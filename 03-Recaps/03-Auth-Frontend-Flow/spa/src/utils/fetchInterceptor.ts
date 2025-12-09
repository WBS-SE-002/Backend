const authServiceURL = import.meta.env.VITE_APP_AUTH_SERVER_URL;
if (!authServiceURL) {
	console.error('No Auth service set');
}
export { authServiceURL };
