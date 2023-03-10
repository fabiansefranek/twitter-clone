import { Observable, throwError } from "rxjs";
import { catchError, retry } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { User } from "src/types";
import { Router } from "@angular/router";

@Injectable({
	providedIn: "root",
})
export class AuthService {
	private authUrl = "http://localhost:5000/auth"; //TODO: use env variable for port
	constructor(private http: HttpClient, private router: Router) {}

	parseTokenAndStore(token: string): any {
		if (token != null) {
			const parsedToken = JSON.parse(atob(token.split(".")[1]));
			window.sessionStorage.setItem("accessToken", parsedToken);
			window.sessionStorage.setItem("accessTokenExpiry", parsedToken.exp);
			window.sessionStorage.setItem("username", parsedToken.unique_name);
			return parsedToken;
		}
		return null;
	}

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
						this.parseTokenAndStore(data.token);
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
						const token = this.parseTokenAndStore(data.token);
						console.log(token);
					}
				},
				error: (err) => console.error(err),
			});
	}

	isLoggedIn(): boolean {
		const accessToken = window.sessionStorage.getItem("accessToken");
		const tokenExpiry = window.sessionStorage.getItem("accessTokenExpiry");
		const now = Math.floor(Date.now() / 1000);
		if (accessToken && tokenExpiry && parseInt(tokenExpiry) > now) {
			return true;
		}
		return false;
	}
}
