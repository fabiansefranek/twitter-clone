export type User = {
	username: string;
	password: string;
};

export type Post = {
	id: number;
	text: string;
	createdAt: number;
	updatedAt: number;
};

export type AccessToken = {
	unique_name: string;
	role: string;
	exp: number;
	nbf: number;
	iat: number;
};
