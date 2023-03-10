export type User = {
	username: string;
	password: string;
};

export type AccessToken = {
	unique_name: string;
	role: string;
	exp: number;
	nbf: number;
	iat: number;
};
