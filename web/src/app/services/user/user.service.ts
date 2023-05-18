import { EventEmitter, Injectable, Output } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Post, User } from "src/types";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { AuthService } from "../auth/auth.service";

@Injectable({
	providedIn: "root",
})
export class UserService {
	private userUrl = `${environment.BASE_URL}/users`;
	constructor(private http: HttpClient, private authService: AuthService) {}
	@Output() fetchUser = new EventEmitter();

	getUser(username: string): Observable<User> {
		return this.http.get<User>(`${this.userUrl}/${username}`);
	}

	getPostsFromUser(userId: string): Observable<Post[]> {
		return this.http.get<Post[]>(`${this.userUrl}/${userId}/posts`);
	}

	updateUser(user: Partial<User>): Observable<User> {
		return this.http.put<User>(`${this.userUrl}`, user);
	}
}
