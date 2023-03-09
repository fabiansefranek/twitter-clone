import { Observable, throwError } from "rxjs";
import { catchError, retry } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { User } from "src/types";

@Injectable({
	providedIn: "root",
})
export class AuthService {
	private authUrl = "http://localhost:5000/auth/login"; //TODO: use env variable for port
	constructor(private http: HttpClient) {}

	login(user: User) {
		const request = this.http
			.post(this.authUrl, user, {
				headers: new HttpHeaders({
					"Content-Type": "application/json",
				}),
			})
			.subscribe({
				next: (data: any) => {
					if (data.token != null || data.token != undefined) {
						const parsedToken = JSON.parse(atob(data.token.split(".")[1]));
						console.log(new Date(parsedToken.exp * 1000));
						console.log(new Date());
						window.sessionStorage.setItem("bearer", data.token);
					}
				},
				error: (err) => {
					console.error("ERR");
				},
			});
	}
}
