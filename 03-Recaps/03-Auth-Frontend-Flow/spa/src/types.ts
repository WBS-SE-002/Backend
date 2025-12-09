declare global {
	type Post = {
		id: string;
		title: string;
		author: string;
		image: string;
		content: string;
	};
	type User = {
		id: string;
		createdAt: string;
		__v: number;
		email: string;
		firstName?: string;
		lastName?: string;
		roles: string[];
	};

	type AuthContextType = {
		signedIn: boolean;
		user: User | null;
		handleSignIn: () => void;
		handleSignOut: () => void;
	};
}
