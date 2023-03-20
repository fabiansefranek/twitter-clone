import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Post } from "src/types";

@Injectable({
	providedIn: "root",
})
export class PostService {
	private postUrl = "http://localhost:5000/posts";
	constructor(private http: HttpClient) {}

	getPosts(): Observable<Post[]> {
		return this.http.get<Post[]>(this.postUrl);
	}

	createPost(post: Post): Observable<Post> {
		return this.http.post<Post>(this.postUrl, post);
	}
}
