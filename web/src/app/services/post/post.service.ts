import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Post } from "src/types";

@Injectable({
	providedIn: "root",
})
export class PostService {
	private postUrl = "http://localhost:5000/posts";
	constructor(private http: HttpClient) {}

	getPosts(): Post[] | undefined {
		let respose: Post[] | undefined = [];
		this.http.get(this.postUrl).subscribe((response: any) => {
			console.log(response.data);
		});
		return respose;
	}
}
