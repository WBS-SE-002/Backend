import Markdown from 'react-markdown';
import type { Message } from '../types';

type ChatBubbleProps = {
	message: Message;
};
const ChatBubble = ({ message }: ChatBubbleProps) => {
	const { role, content } = message;
	return (
		<div className={`chat ${role === 'user' ? 'chat-end' : 'chat-start'}`}>
			<div className='chat-image avatar'>
				<div className='w-10 rounded-full p-2 bg-slate-800'>
					{role === 'user' ? 'You' : 'Bot'}
				</div>
			</div>
			<div
				className={`chat-bubble  ${
					role === 'user' ? 'chat-bubble-primary' : 'chat-bubble-secondary'
				}`}
			>
				<Markdown>{content}</Markdown>
			</div>
		</div>
	);
};

export default ChatBubble;
