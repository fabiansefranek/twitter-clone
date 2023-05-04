import { Component, Input } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { PostService } from "src/app/services/post/post.service";
import { Post } from "src/types";

@Component({
	selector: "app-post-detail",
	templateUrl: "./post-detail.component.html",
	styleUrls: ["./post-detail.component.css"],
})
export class PostDetailComponent {
	@Input() post: Post = {} as Post;
	constructor(public router: Router, private route: ActivatedRoute, private postService: PostService) {}

	ngOnInit() {
		const postId = this.route.snapshot.paramMap.get("id");
		if (!postId) throw new Error("Post Id is null");
		this.postService.getPost(postId).subscribe((data: any) => {
			this.post = data;
		});
	}
}
