import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { PostComponent } from "../post/post.component";
import { Post, User } from "src/types";
import { PostService } from "src/app/services/post/post.service";

@Component({
	selector: "app-home",
	templateUrl: "./home.component.html",
	styleUrls: ["./home.component.css"],
})
export class HomeComponent {
	isAuthenticated: boolean = false;
	user: User = {} as User;
	posts: Post[] = [];
	constructor(private authService: AuthService, private postService: PostService) {}

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
		this.updatePosts();
	}
}
