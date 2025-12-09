import { authServiceURL } from '@/utils';
type LoginInput = { email: string; password: string };

type SuccessRes = { message: string };

type TokenRes = SuccessRes & { accessToken: string; refreshToken: string };

const login = async (formData: LoginInput): Promise<TokenRes> => {
	const res = await fetch(`${authServiceURL}/login`, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(formData)
	});
	if (!res.ok) throw new Error(`${res.status}. Something went wrong!`);

	const data = (await res.json()) as TokenRes;
	// console.log(data);

	return data;
};

export { login };
