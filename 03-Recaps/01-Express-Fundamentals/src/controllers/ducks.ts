import type { RequestHandler } from 'express';
import { isValidObjectId } from 'mongoose';
import type { z } from 'zod/v4';
import { Duck } from '#models';
import type {
	duckInputSchema,
	duckUpdateInputSchema,
	duckSchema
} from '#schemas';

type DuckInputDTO = z.infer<typeof duckInputSchema>;
type UpdateDuckDTO = z.infer<typeof duckUpdateInputSchema>;
type DuckDTO = z.infer<typeof duckSchema>;

const getAllDucks: RequestHandler<{}, DuckDTO[]> = async (req, res) => {
	const ducks = await Duck.find();

	res.json(ducks);
};
const createDuck: RequestHandler<{}, DuckDTO, DuckInputDTO> = async (
	req,
	res
) => {
	// const { name, imgUrl, quote, owner } = req.body;

	const newDuck = await Duck.create(req.body);

	res.json(newDuck);
};
const getDuckById: RequestHandler<{ id: string }, DuckDTO> = async (
	req,
	res
) => {
	const { id } = req.params;

	if (!isValidObjectId(id)) throw new Error('Invalid ID', { cause: 400 });

	const duck = await Duck.findById(id);

	if (!duck) throw new Error('Duck Not Found', { cause: 404 });

	res.json(duck);
};
const updateDuck: RequestHandler<
	{ id: string },
	DuckDTO,
	UpdateDuckDTO
> = async (req, res) => {
	const { name, imgUrl, quote } = req.body;
	const { id } = req.params;
	const { userId } = req;

	const duck = await Duck.findById(id);

	if (!duck) throw new Error('Duck Not Found', { cause: 404 });

	// console.log(duck.owner);
	// if (userId !== duck.owner.toString())
	// 	throw new Error('You are not authorized to update this duck', {
	// 		cause: 403
	// 	});

	duck.name = name;
	duck.imgUrl = imgUrl;
	duck.quote = quote;

	await duck.save();
	res.json(duck);
};
const deleteDuck: RequestHandler<{ id: string }, { message: string }> = async (
	req,
	res
) => {
	const { id } = req.params;
	if (!isValidObjectId(id)) throw new Error('Invalid ID', { cause: 400 });

	const found = await Duck.findByIdAndDelete(id);

	if (!found) throw new Error('Duck Not Found', { cause: 404 });

	res.json({ message: 'Duck deleted' });
};

export { getAllDucks, createDuck, getDuckById, updateDuck, deleteDuck };
