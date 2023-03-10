import { Observable, throwError } from "rxjs";
import { catchError, retry } from "rxjs/operators";
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

	parseToken(token: string): AccessToken | null {
		if (token == null) throw "Access token is null";
		const parsedToken = JSON.parse(atob(token.split(".")[1]));
		return parsedToken;
	}

	storeToken(token: string) {
		const parsedToken = this.parseToken(token);
		if (parsedToken == null) throw "Invalid token";
		window.sessionStorage.setItem("accessToken", token);
		window.sessionStorage.setItem("accessTokenExpiry", parsedToken.exp.toString());
		window.sessionStorage.setItem("username", parsedToken.unique_name);
	}

	isLoggedIn(): boolean {
		//TODO: verify token
		const accessToken = window.sessionStorage.getItem("accessToken");
		const tokenExpiry = window.sessionStorage.getItem("accessTokenExpiry");
		const now = Math.floor(Date.now() / 1000);
		if (accessToken && tokenExpiry && parseInt(tokenExpiry) > now) {
			return true;
		}
		return false;
	}

	getUser(): User | undefined {
		if (!this.isLoggedIn()) return undefined;
		const token = this.parseToken(window.sessionStorage.getItem("accessToken") as string);
		if (token == null) return undefined;
		return { username: token.unique_name } as User;
	}
}
