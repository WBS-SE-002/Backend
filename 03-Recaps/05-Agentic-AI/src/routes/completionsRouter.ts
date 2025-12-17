import { Router } from 'express';
import {
  createSimpleChatCompletion,
  createChatCompletion,
  getChatHistory,
  createAgenticChat
} from '#controllers';
import { validateBodyZod, authenticate } from '#middlewares';
import { promptBodySchema } from '#schemas';

const completionsRouter = Router();
completionsRouter.get('/history/:id', getChatHistory);

completionsRouter.post(
  '/agent',
  authenticate,
  validateBodyZod(promptBodySchema),
  createAgenticChat
);
completionsRouter.use(validateBodyZod(promptBodySchema));
completionsRouter.post('/simple-chat', createSimpleChatCompletion);
completionsRouter.post('/chat', createChatCompletion);

export default completionsRouter;
