import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Post } from "src/types";

@Injectable({
	providedIn: "root",
})
export class PostService {
	private postUrl = `${environment.BASE_URL}/posts`;
	constructor(private http: HttpClient) {}

	getPosts(): Observable<Post[]> {
		return this.http.get<Post[]>(this.postUrl);
	}

	createPost(post: Post): Observable<Post> {
		return this.http.post<Post>(this.postUrl, post);
	}
}
