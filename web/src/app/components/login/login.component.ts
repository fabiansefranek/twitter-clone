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
		const accessToken = window.sessionStorage.getItem("accessToken");
		const tokenExpiry = window.sessionStorage.getItem("accessTokenExpiry");
		const now = Math.floor(Date.now() / 1000);
		if (accessToken && tokenExpiry && parseInt(tokenExpiry) > now) {
			this.router.navigate(["/"]);
		}
	}

	handleLoginButtonClick() {
		if (!this.user.username || !this.user.password) return;
		this.authService.login(this.user);
	}
}
