import { type RequestHandler } from 'express';
import type { z } from 'zod/v4';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import { User, RefreshToken } from '#models';
import { ACCESS_JWT_SECRET, SALT_ROUNDS } from '#config';
import { createTokens } from '#utils';
import type {
  registerSchema,
  loginSchema,
  userSchema,
  userProfileSchema,
  refreshTokenSchema
} from '#schemas';

type RegisterDTO = z.infer<typeof registerSchema>;
type LoginDTO = z.infer<typeof loginSchema>;
type UserDTO = z.infer<typeof userSchema>;
type RefreshTokenDTO = z.infer<typeof refreshTokenSchema>;
type UserProfileDTO = { user: z.infer<typeof userProfileSchema> };

type SuccessResBody = { message: string };
type TokenResBody = SuccessResBody & { accessToken: string; refreshToken: string };
type MeResBody = SuccessResBody & UserProfileDTO;

export const register: RequestHandler<{}, TokenResBody, RegisterDTO> = async (req, res) => {
  const { firstName, lastName, email, password } = req.body;
  // Check if the user exists in the database
  const userExists = await User.exists({ email });
  if (userExists) throw new Error('Email already registered', { cause: { status: 409 } });

  const salt = await bcrypt.genSalt(SALT_ROUNDS);
  const hashedPW = await bcrypt.hash(password, salt);

  const user = await User.create<UserDTO>({ firstName, lastName, email, password: hashedPW });

  const [refreshToken, accessToken] = await createTokens(user);

  res.status(201).json({ message: 'Registered', accessToken, refreshToken });
};

export const login: RequestHandler<{}, TokenResBody, LoginDTO> = async (req, res) => {
  const { email, password } = req.body;
  // Check if the user exists in the database
  const user = await User.findOne({ email });
  if (!user) throw new Error('Invalid credentials', { cause: { status: 401 } });

  const match = await bcrypt.compare(password, user.password);

  if (!match) throw new Error('Invalid credentials', { cause: { status: 401 } });

  await RefreshToken.deleteMany({ userId: user._id });

  const [refreshToken, accessToken] = await createTokens(user);

  res.json({ message: 'Logged in', accessToken, refreshToken });
};

export const refresh: RequestHandler<{}, TokenResBody, RefreshTokenDTO> = async (req, res) => {
  const { refreshToken } = req.body;

  // query the DB for a RefreshToken that has a token property that matches the refreshToken
  const storedToken = await RefreshToken.findOne({
    token: refreshToken
  }).lean();

  // if no storedToken is found, throw a 403 error with an appropriate message
  if (!storedToken) {
    throw new Error('Refresh token not found.', { cause: { status: 403 } });
  }

  // delete the storedToken from the DB
  await RefreshToken.findByIdAndDelete(storedToken._id);

  // query the DB for the user with _id that matches the userId of the storedToken
  const user = await User.findById(storedToken.userId).lean();

  // if not user is found, throw a 403 error
  if (!user) {
    throw new Error('User not found.', { cause: { status: 403 } });
  }

  // create new tokens with util function
  const [newRefreshToken, newAccessToken] = await createTokens(user);

  // send generic success response in body of response
  res.status(201).json({
    message: 'Refreshed',
    refreshToken: newRefreshToken,
    accessToken: newAccessToken
  });
};

export const logout: RequestHandler<{}, SuccessResBody, RefreshTokenDTO> = async (req, res) => {
  const { refreshToken } = req.body;

  await RefreshToken.deleteOne({ token: refreshToken });

  // send generic success message in response body
  res.json({ message: 'Successfully logged out' });
};

export const me: RequestHandler<{}, MeResBody> = async (req, res, next) => {
  const authHeader = req.header('Authorization');
  console.log(authHeader?.split(' ')[1]);

  const accessToken = authHeader && authHeader.split(' ')[1];

  if (!accessToken) throw new Error('Access token is required.', { cause: { status: 401 } });

  try {
    const decoded = jwt.verify(accessToken, ACCESS_JWT_SECRET) as jwt.JwtPayload;

    console.log(decoded);

    if (!decoded.sub)
      throw new Error('Invalid or expired access token.', {
        cause: { status: 403 }
      });

    const user = await User.findById(decoded.sub).select('-password').lean();

    if (!user) throw new Error('User not found', { cause: { status: 404 } });

    res.json({ message: 'Valid token', user });
  } catch (error) {
    if (error instanceof jwt.TokenExpiredError) {
      next(
        new Error('Expired access token', { cause: { status: 401, code: 'ACCESS_TOKEN_EXPIRED' } })
      );
    } else {
      next(new Error('Invalid access token', { cause: { status: 401 } }));
    }
  }
};
