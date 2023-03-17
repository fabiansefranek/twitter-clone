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
	isLoggedIn: boolean = false;
	user: User = {} as User;
	posts: Post[] = [];
	constructor(private authService: AuthService, private postService: PostService) {}

	ngOnInit() {
		const isLoggedIn = this.authService.isLoggedIn();
		if (isLoggedIn) {
			this.isLoggedIn = isLoggedIn;
			const user = this.authService.getUser();
			if (user) this.user = user;
		}
		this.postService.getPosts().subscribe((posts) => {
			console.log(posts);
			this.posts = posts;
		});
	}
}
