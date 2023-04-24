import { Component, EventEmitter, Input, Output } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { PostService } from "src/app/services/post/post.service";
import { Post } from "src/types";

@Component({
	selector: "app-editor-popup",
	templateUrl: "./editor-popup.component.html",
	styleUrls: ["./editor-popup.component.css"],
})
export class EditorPopupComponent {
	constructor(private authService: AuthService, private postService: PostService) {}

	@Input() closeEditorPopup: Function = () => {};
	@Output() postCreated = new EventEmitter();

	text: string = "";

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
		this.postService.postCreated.emit(post);
		this.closeEditorPopup();
	}
}
