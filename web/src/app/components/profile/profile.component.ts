import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthService } from "src/app/services/auth/auth.service";
import { PopupService } from "src/app/services/popup/popup.service";
import { UserService } from "src/app/services/user/user.service";
import { Post, User } from "src/types";

@Component({
	selector: "app-profile",
	templateUrl: "./profile.component.html",
	styleUrls: ["./profile.component.css"],
})
export class ProfileComponent {
	user: User = {} as User;
	posts: Post[] = [];
	constructor(
		public router: Router,
		private route: ActivatedRoute,
		private userService: UserService,
		public authService: AuthService,
		public popupService: PopupService
	) {}

	ngOnInit() {
		const username = this.route.snapshot.paramMap.get("username");
		if (!username) throw new Error("Username is null");
		this.userService.getUser(username).subscribe((user) => {
			if (user) this.user = user;
			this.userService.getPostsFromUser(this.user.id).subscribe((posts) => {
				if (posts) this.posts = posts;
			});
		});

		this.userService.fetchUser.subscribe(() => {
			this.fetchUser();
		});
	}

	fetchUser() {
		this.userService.getUser(this.user.username).subscribe((user) => {
			if (user) this.user = user;
		});
	}

	handlePostClick(id: number, event: any) {
		const idBlacklist = ["popup-button", "post_popup"];
		const classBlacklist = ["post_popup_item"];
		const tagBlacklist = ["BUTTON", "A", "IMG", "P"];
		if (
			!tagBlacklist.includes(event.target.tagName) &&
			!idBlacklist.includes(event.target.id) &&
			![...event.target.classList].some((className) => classBlacklist.includes(className))
		) {
			this.router.navigate([`/post/${id}`]);
		}
	}
}
