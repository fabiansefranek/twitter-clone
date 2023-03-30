import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { User } from "src/types";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
	providedIn: "root",
})
export class UserService {
	private userUrl = `${environment.BASE_URL}/users`;
	constructor(private http: HttpClient) {}

	getUser(username: string): Observable<User> {
		return this.http.get<User>(`${this.userUrl}/${username}`);
	}
}