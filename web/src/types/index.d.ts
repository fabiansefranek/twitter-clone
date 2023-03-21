export type User = {
	id: string;
	fullname: string;
	username: string;
	password: string;
};

export type Post = {
	id: number;
	text: string;
	createdAt: number;
	updatedAt: number;
	user: User;
};

export type AccessToken = {
	nameid: string;
	unique_name: string;
	role: string;
	exp: number;
	nbf: number;
	iat: number;
	family_name: string; // claim type for full name does not exist, so family_name is used for full name
};
