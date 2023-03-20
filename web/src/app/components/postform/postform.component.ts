import { Component, EventEmitter, Output } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { PostService } from "src/app/services/post/post.service";
import { Post } from "src/types";

@Component({
	selector: "app-postform",
	templateUrl: "./postform.component.html",
	styleUrls: ["./postform.component.css"],
})
export class PostformComponent {
	@Output() postCreated = new EventEmitter();
	public text: string = "";
	constructor(private postService: PostService, private authService: AuthService) {}

	async handleSubmit() {
		const user = await this.authService.getUser();
		if (!user) return;
		const post = { text: this.text, user: { id: user.id } } as Post;
		this.postService.createPost(post).subscribe((post) => {
			this.text = "";
		});
		this.postCreated.emit(post);
	}
}
