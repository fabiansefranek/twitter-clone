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
		if (this.text.length === 0) return;
		const unix_timestamp = Math.floor(Date.now() / 1000);
		const post = {
			text: this.text,
			createdAt: unix_timestamp,
			user: { id: user.id, username: user.username, fullname: user.fullname },
		} as Post;
		this.postService.createPost(post).subscribe((post) => {
			this.text = "";
		});
		this.postCreated.emit(post);
	}
}
