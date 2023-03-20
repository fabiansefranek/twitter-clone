import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { User } from "src/types";
import { Router } from "@angular/router";

@Component({
	selector: "app-login",
	templateUrl: "./login.component.html",
	styleUrls: ["./login.component.css"],
})
export class LoginComponent {
	user: User = {} as User;
	constructor(private authService: AuthService, private router: Router) {}

	ngOnInit() {
		this.authService.isAuthenticated().then((isAuthenticated) => {
			if (isAuthenticated) {
				this.router.navigate(["/"]);
			}
		});
	}

	handleLoginButtonClick() {
		if (!this.user.username || !this.user.password) return;
		this.authService.login(this.user);
	}
}
