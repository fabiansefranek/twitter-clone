import { Component, EventEmitter, Input, Output } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { PopupService } from "src/app/services/popup/popup.service";
import { PostService } from "src/app/services/post/post.service";
import { Post } from "src/types";

@Component({
	selector: "app-editor-popup",
	templateUrl: "./editor-popup.component.html",
	styleUrls: ["./editor-popup.component.css"],
})
export class EditorPopupComponent {
	constructor(private authService: AuthService, private postService: PostService, public popupService: PopupService) {}

	@Output() postCreated = new EventEmitter();

	text: string = this.popupService.updatePost.text ? this.popupService.updatePost.text : "";

	async handleSubmit() {
		const user = await this.authService.getUser();
		if (!user) return;
		if (this.text.length === 0) return;
		const unix_timestamp = Math.floor(Date.now() / 1000);
		if (this.popupService.updatePost.text) {
			// update post
			const updatedPost = {
				id: this.popupService.updatePost.id,
				text: this.text,
				updatedAt: unix_timestamp,
			} as Post;
			this.postService.updatePost(updatedPost).subscribe((post) => {});
		} else {
			// create post
			const post = {
				text: this.text,
				createdAt: unix_timestamp,
				user: { id: user.id, username: user.username, fullname: user.fullname },
			} as Post;
			this.postService.createPost(post).subscribe((post) => {
				this.text = "";
			});
		}
		this.postService.postCreated.emit();
		this.popupService.closeEditorPopup();
	}
}
