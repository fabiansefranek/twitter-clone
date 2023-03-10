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
	private authUrl = "http://localhost:5000/auth/login"; //TODO: use env variable for port
	constructor(private http: HttpClient, private router: Router) {}

	login(user: User) {
		const request = this.http
			.post(this.authUrl, user, {
				headers: new HttpHeaders({
					"Content-Type": "application/json",
				}),
			})
			.subscribe({
				next: (data: any) => {
					if (data.token != null) {
						this.router.navigate(["/"]);
						const token = JSON.parse(atob(data.token.split(".")[1]));
						window.sessionStorage.setItem("accessToken", data.token);
						window.sessionStorage.setItem("accessTokenExpiry", token.exp);
						window.sessionStorage.setItem("username", token.username);
					}
				},
				error: (err) => {
					console.error("ERR");
				},
			});
	}
}
