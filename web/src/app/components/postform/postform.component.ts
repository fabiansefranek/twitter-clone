import { Component, EventEmitter, Output } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { PostService } from "src/app/services/post/post.service";
import { UserService } from "src/app/services/user/user.service";
import { Post, User } from "src/types";

@Component({
	selector: "app-postform",
	templateUrl: "./postform.component.html",
	styleUrls: ["./postform.component.css"],
})
export class PostformComponent {
	public text: string = "";
	public user: User = {} as User;
	constructor(private postService: PostService, public authService: AuthService, public userService: UserService) {}

	ngOnInit() {
		if (this.authService.user.username) {
			this.userService.getUser(this.authService.user.username).subscribe((user) => {
				if (user) this.user = user;
			});
		}
	}

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
			this.postService.fetchPosts.emit();
		});
	}
}
