import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth/auth.service";
import { User } from "src/types";

@Component({
	selector: "app-signup",
	templateUrl: "./signup.component.html",
	styleUrls: ["./signup.component.css"],
})
export class SignupComponent {
	user: User = {} as User;
	constructor(private authService: AuthService, private router: Router) {}

	ngOnInit() {
		this.authService.isAuthenticated().then((isAuthenticated) => {
			if (isAuthenticated) {
				this.router.navigate(["/"]);
			}
		});
	}

	handleSignupButtonClick() {
		if (!this.user.username || !this.user.password || !this.user.fullname) return;
		this.authService.signup(this.user);
	}
}
