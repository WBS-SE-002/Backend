import type { RequestHandler } from 'express';
import type { ChatCompletionMessageParam } from 'openai/resources';
import { z } from 'zod';
import OpenAI from 'openai';
import {
  Agent,
  OpenAIChatCompletionsModel,
  run,
  setDefaultOpenAIClient,
  tool,
  user,
  assistant,
  InputGuardrailTripwireTriggered,
  type InputGuardrail
} from '@openai/agents';

import type { promptBodySchema } from '#schemas';
import { isValidObjectId } from 'mongoose';
import { Chat, Post } from '#models';

type IncomingPrompt = z.infer<typeof promptBodySchema>;
type ResponseCompletion = { completion: string };
type ResponseWithId = ResponseCompletion & { chatId: string };

export const createAgenticChat: RequestHandler<unknown, ResponseWithId, IncomingPrompt> = async (
  req,
  res
) => {
  const { prompt, chatId } = req.body;
  const { user: userInfo } = req;
  const signedIn = userInfo?.id ? true : false;

  // find chat in database
  let currentChat = await Chat.findById(chatId);
  // if no chat is found, create a chat with system prompt
  if (!currentChat) {
    currentChat = await Chat.create({});
  }

  // add user message to database history
  currentChat.history.push({
    role: 'user',
    content: prompt
  });

  const formattedHistory = currentChat.history.map(msg =>
    msg.role === 'user' ? user(msg.content) : assistant(msg.content)
  );

  const client = new OpenAI({
    apiKey: process.env.AI_API_KEY,
    baseURL: process.env?.AI_URL
  });

  setDefaultOpenAIClient(client);

  const model = new OpenAIChatCompletionsModel(client, process.env.AI_MODEL || 'gemini-2.5-flash');

  // console.log(msgHistory);

  const getPostsTool = tool({
    name: 'get_post',
    description:
      'Get the travel blog posts from a user to offer personalized travel recommendations',
    parameters: z.object({ userId: z.string() }),
    async execute({ userId }) {
      console.log(`\x1b[35mFunction get_posts called with userId: ${userId}\x1b[0m`);
      const userPosts = await Post.find({ author: userId }).select('title content -_id').lean();
      console.log(userPosts);
      return JSON.stringify(userPosts);
    }
  });

  const guardrailAgent = new Agent({
    name: 'Guardrail Check',
    instructions: `We give travel recommendations. If the input is remotely about travel and travel destinations
       return isNotAboutTravel: false, otherwise return true.`,
    model,
    outputType: z.object({
      isNotAboutTravel: z.boolean(),
      reasoning: z.string()
    })
  });

  const travelGuardrails: InputGuardrail = {
    name: 'Travel Assistant Guardrail',
    execute: async ({ input, context }) => {
      const result = await run(guardrailAgent, input, { context });
      return {
        outputInfo: result.finalOutput,
        tripwireTriggered: result.finalOutput?.isNotAboutTravel ?? false
      };
    }
  };

  const generalizedAgent = new Agent({
    name: 'Generalized Agent',
    instructions: `You offer general travel advice and recommendations. You suggest that the user log in to 
    get more personalized results. If the user asks for follow up questions about a travel destination, use your general knowledge.`,
    model
  });

  const personalizedAgent = new Agent({
    name: 'Personalized Agent',
    instructions: `You are a helpful assistant with memory of past conversations for user with userId: ${userInfo?.id}.
       You can access user posts if needed using the get_posts tool.
       When asked to use the get_posts tool, please call the tool and and use the information to offer personalized travel
       recommendations. If the user asks for more information about a travel destination, use your general knowledge.`,
    model,
    tools: [getPostsTool]
  });

  const triageAgent = new Agent({
    name: 'Triage Agent',
    instructions: `${signedIn ? 'The user is logged in.' : 'The user is not logged in'}
      If the user is logged in, handoff to the personalized agent.
      If the user is not logged in, handoff to the generalized agent.
        `,
    model,
    inputGuardrails: [travelGuardrails],
    handoffs: [generalizedAgent, personalizedAgent]
  });

  try {
    const result = await run(triageAgent, formattedHistory);

    if (result.finalOutput) {
      currentChat.history.push({
        role: 'assistant',
        content: result.finalOutput
      });
    }

    await currentChat.save();

    res.json({
      completion: result.finalOutput || 'Something went wrong, please ask again',
      chatId: currentChat._id.toString()
    });
  } catch (error) {
    if (error instanceof InputGuardrailTripwireTriggered) {
      res.json({
        completion: 'That is outside of my abilities, I only answer questions about travel',
        chatId: currentChat._id.toString()
      });
    } else {
      console.error('An error occurred:', error);
      res.json({
        completion: 'Sorry, something went wrong, please ask again.',
        chatId: currentChat._id.toString()
      });
    }
  }
};

export const getChatHistory: RequestHandler = async (req, res) => {
  const { id } = req.params;

  if (!isValidObjectId(id)) throw new Error('Invalid id', { cause: { status: 400 } });

  const chat = await Chat.findById(id);

  if (!chat) throw new Error('Chat not found', { cause: { status: 404 } });

  res.json(chat);
};

// declared outside of function, to persist across API calls.
// Will reset if server stops/restarts
const messages: ChatCompletionMessageParam[] = [
  { role: 'developer', content: 'You are Batman after becoming a Senior Software Engineer.' }
];

export const createSimpleChatCompletion: RequestHandler<
  unknown,
  ResponseCompletion,
  IncomingPrompt
> = async (req, res) => {
  const { prompt } = req.body;

  const client = new OpenAI({
    apiKey: process.env.AI_API_KEY,
    baseURL: process.env?.AI_URL
  });

  messages.push({ role: 'user', content: prompt });

  const completion = await client.chat.completions.create({
    model: process.env.AI_MODEL || 'gemini-2.0-flash',
    messages
  });

  const completionText = completion.choices[0]?.message.content || 'No completion generated';

  messages.push({ role: 'assistant', content: completionText });

  res.json({ completion: completionText });
};
export const createChatCompletion: RequestHandler<unknown, ResponseWithId, IncomingPrompt> = async (
  req,
  res
) => {
  const { prompt, chatId } = req.body;

  // find chat in database
  let currentChat = await Chat.findById(chatId);
  // if no chat is found, create a chat with system prompt
  if (!currentChat) {
    const systemPrompt = {
      role: 'developer',
      content: 'You are Gollum after becoming a Senior Software Engineer'
    };
    currentChat = await Chat.create({ history: [systemPrompt] });
  }

  // add user message to database history
  currentChat.history.push({
    role: 'user',
    content: prompt
  });

  const client = new OpenAI({
    apiKey: process.env.AI_API_KEY,
    baseURL: process.env?.AI_URL
  });

  const msgHistory = currentChat.history.map(msg => {
    const { role, content } = msg;
    return { role, content };
  });

  // console.log(msgHistory);

  const completion = await client.chat.completions.create({
    model: process.env.AI_MODEL || 'gemini-2.0-flash',
    // stringifying and then parsing is like using .lean(). It will turn currentChat into a plain JavaScript Object
    // We don't use .lean(), because we later need to .save()
    messages: JSON.parse(JSON.stringify(msgHistory))
  });

  const completionText = completion.choices[0]?.message.content || 'No completion generated';

  currentChat.history.push({ role: 'assistant', content: completionText });

  await currentChat.save();

  res.json({ completion: completionText, chatId: currentChat._id.toString() });
};
