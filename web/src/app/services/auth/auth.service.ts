import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AccessToken, User } from "src/types";
import { Router } from "@angular/router";

@Injectable({
	providedIn: "root",
})
export class AuthService {
	private authUrl = "http://localhost:5000/auth"; //TODO: use env variable for port
	constructor(private http: HttpClient, private router: Router) {}

	login(user: User) {
		const request = this.http
			.post(`${this.authUrl}/login`, user, {
				headers: new HttpHeaders({
					"Content-Type": "application/json",
				}),
			})
			.subscribe({
				next: (data: any) => {
					if (data.token != null) {
						this.router.navigate(["/"]);
						this.storeToken(data.token);
					}
				},
				error: (err) => {
					console.error(err);
				},
			});
	}

	signup(user: User) {
		this.http
			.post(`${this.authUrl}/signup`, user, {
				headers: new HttpHeaders({
					"Content-Type": "application/json",
				}),
			})
			.subscribe({
				next: (data: any) => {
					if (data.token != null) {
						this.router.navigate(["/"]);
						this.storeToken(data.token);
					}
				},
				error: (err) => console.error(err),
			});
	}

	logout() {
		window.sessionStorage.removeItem("token");
		window.sessionStorage.removeItem("tokenExpiry");
		window.sessionStorage.removeItem("username");
		this.router.navigate(["/"]);
	}

	parseToken(token: string): AccessToken | null {
		if (token == null) throw "Access token is null";
		try {
			atob(token.split(".")[1]);
		} catch (e) {
			return null;
		}
		const parsedToken = JSON.parse(atob(token.split(".")[1]));
		return parsedToken;
	}

	storeToken(token: string) {
		const parsedToken = this.parseToken(token);
		if (parsedToken == null) throw "Invalid token";
		window.sessionStorage.setItem("token", token);
		window.sessionStorage.setItem("tokenExpiry", parsedToken.exp.toString());
		window.sessionStorage.setItem("username", parsedToken.unique_name);
	}

	hasToken(): boolean {
		return window.sessionStorage.getItem("token") != null;
	}

	isAuthenticated(): Promise<boolean> {
		return new Promise((resolve, reject) => {
			const token = window.sessionStorage.getItem("token");
			if (!token) return resolve(false);

			this.http
				.post(`${this.authUrl}/validate`, JSON.stringify(token), {
					headers: new HttpHeaders({
						"Content-Type": "application/json",
					}),
					observe: "response",
				})
				.subscribe({
					next: (response: any) => {
						if (response.status === 200) {
							return resolve(true);
						} else {
							return resolve(false);
						}
					},
					error: (err) => {
						console.error(err);
						return resolve(false);
					},
				});
		});
	}

	getUser(): User | undefined {
		const isAuthenticated = this.isAuthenticated().then((isAuthenticated) => isAuthenticated);
		if (!isAuthenticated) return undefined;
		const token = this.parseToken(window.sessionStorage.getItem("token") as string);
		if (token == null) return undefined;
		return { username: token.unique_name } as User;
	}
}
