import { HttpClient } from "@angular/common/http";
import { EventEmitter, Injectable, Output } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Post } from "src/types";

@Injectable({
	providedIn: "root",
})
export class PostService {
	private postUrl = `${environment.BASE_URL}/posts`;
	constructor(private http: HttpClient) {}

	@Output() postCreated = new EventEmitter();

	getPosts(): Observable<Post[]> {
		return this.http.get<Post[]>(this.postUrl);
	}

	getPost(id: string) {
		return this.http.get<Post>(`${this.postUrl}/${id}`);
	}

	createPost(post: Post): Observable<Post> {
		return this.http.post<Post>(this.postUrl, post);
	}

	updatePost(post: Post): Observable<Post> {
		return this.http.put<Post>(this.postUrl, post);
	}
}
