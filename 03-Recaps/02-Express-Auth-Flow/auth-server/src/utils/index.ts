import { randomUUID } from 'node:crypto';
import jwt from 'jsonwebtoken';
import type { Types } from 'mongoose';
import { ACCESS_JWT_SECRET, ACCESS_TOKEN_TTL } from '#config';
import { RefreshToken } from '#models';

const createTokens = async (userData: {
  roles: string[];
  _id: Types.ObjectId;
}): Promise<[string, string]> => {
  const payload = { roles: userData.roles };
  const secret = ACCESS_JWT_SECRET;
  const tokenOptions = {
    expiresIn: ACCESS_TOKEN_TTL,
    subject: userData._id.toString()
  };

  const accessToken = jwt.sign(payload, secret, tokenOptions);
  const refreshToken = randomUUID();

  await RefreshToken.create({
    token: refreshToken,
    userId: userData._id
  });

  return [refreshToken, accessToken];
};

export { createTokens };
