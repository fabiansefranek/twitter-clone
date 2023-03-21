import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { User } from "src/types";
import { Observable } from "rxjs";

@Injectable({
	providedIn: "root",
})
export class UserService {
	private userUrl = "http://localhost:5000/users";
	constructor(private http: HttpClient) {}

	getUser(username: string): Observable<User> {
		return this.http.get<User>(`${this.userUrl}/${username}`);
	}
}
