const AI_SERVER_URL = import.meta.env.VITE_AI_SERVER_URL;
if (!AI_SERVER_URL)
	throw new Error('AI_SERVER_URL is required, are you missing a .env file?');
const baseURL = `${AI_SERVER_URL}/ai`;

const createChat = async (body) => {};

const getChatHistory = async (chatId) => {};

export { createChat, getChatHistory };
