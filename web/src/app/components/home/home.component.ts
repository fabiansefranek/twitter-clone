import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { Post, User } from "src/types";
import { PostService } from "src/app/services/post/post.service";
import { ActivatedRoute, createUrlTreeFromSnapshot, Event, Router } from "@angular/router";

@Component({
	selector: "app-home",
	templateUrl: "./home.component.html",
	styleUrls: ["./home.component.css"],
})
export class HomeComponent {
	isAuthenticated: boolean = false;
	user: User = {} as User;
	posts: Post[] = [];

	constructor(
		public router: Router,
		private authService: AuthService,
		private postService: PostService,
		private route: ActivatedRoute
	) {}

	updatePosts(newPost?: Post) {
		this.postService.getPosts().subscribe((posts) => {
			if (newPost) {
				this.posts = [newPost, ...posts];
			} else {
				this.posts = posts;
			}
		});
	}

	ngOnInit() {
		const isAuthenticated = this.authService.isAuthenticated().then((isAuthenticated) => {
			this.isAuthenticated = isAuthenticated;
			if (isAuthenticated) {
				const user = this.authService.getUser().then((user) => {
					if (user) this.user = user;
				});
			}
		});
		this.postService.postCreated.subscribe((post) => {
			this.updatePosts(post);
		});

		this.updatePosts();
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
