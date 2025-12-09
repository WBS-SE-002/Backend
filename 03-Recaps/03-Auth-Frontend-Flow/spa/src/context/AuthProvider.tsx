import { useState, type ReactNode } from 'react';
import { AuthContext } from '.';

const AuthProvider = ({ children }: { children: ReactNode }) => {
	const [signedIn, setSignedIn] = useState(false);
	const [user, setUser] = useState<User | null>(null);

	const handleSignIn = () => {
		setSignedIn(true);
	};

	const handleSignOut = () => {
		setSignedIn(false);
		setUser(null);
	};
	const value: AuthContextType = {
		signedIn,
		user,
		handleSignIn,
		handleSignOut
	};
	return <AuthContext value={value}>{children}</AuthContext>;
};

export default AuthProvider;
