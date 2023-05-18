import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { PopupService } from "src/app/services/popup/popup.service";
import { PostService } from "src/app/services/post/post.service";
import { UserService } from "src/app/services/user/user.service";
import { Post, User } from "src/types";

@Component({
	selector: "app-profile-editor-popup",
	templateUrl: "./profile-editor-popup.component.html",
	styleUrls: ["./profile-editor-popup.component.css"],
})
export class ProfileEditorComponent {
	constructor(public authService: AuthService, public popupService: PopupService, private userService: UserService) {}

	user: User = {} as User;

	ngOnInit() {
		this.userService.getUser(this.authService.user.username).subscribe((user) => {
			if (user) this.user = user;
		});
	}

	openUrlPrompt(profile: boolean) {
		const url = prompt("Enter an image URL", this.user.profilePicture) || "";
		if (!url) return;
		try {
			new URL(url);
			if (profile) this.user.profilePicture = url || "";
		} catch (_) {
			alert("Invalid URL");
		}
	}

	async handleSubmit() {
		this.userService.updateUser(this.user).subscribe((user) => {
			this.userService.fetchUser.emit();
			this.popupService.closePopup("profile-editor");
		});
	}
}
