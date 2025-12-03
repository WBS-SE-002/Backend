import type { RequestHandler } from 'express';
import jwt from 'jsonwebtoken';

const secret = process.env.ACCESS_JWT_SECRET;

if (!secret) {
  console.log('Missing access token secret');
  process.exit(1);
}

const authenticate: RequestHandler = (req, _res, next) => {
  const authHeader = req.header('Authorization');
  console.log(authHeader?.split(' ')[1]);

  const accessToken = authHeader && authHeader.split(' ')[1];

  if (!accessToken) throw new Error('Access token is required.', { cause: { status: 401 } });

  try {
    const decoded = jwt.verify(accessToken, secret) as jwt.JwtPayload;

    console.log(decoded);

    if (!decoded.sub)
      throw new Error('Invalid or expired access token.', {
        cause: { status: 403 }
      });

    const user = { id: decoded.sub, roles: decoded.roles };

    req.user = user;

    next();
  } catch (error) {
    if (error instanceof jwt.TokenExpiredError) {
      next(new Error('Expired access token', { cause: { status: 401, code: 'ACCESS_TOKEN_EXPIRED' } }));
    } else {
      next(new Error('Invalid access token', { cause: { status: 401 } }));
    }
  }
};

export default authenticate;
