import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { User } from "src/types";

@Component({
	selector: "app-login",
	templateUrl: "./login.component.html",
	styleUrls: ["./login.component.css"],
})
export class LoginComponent {
	user: User = {} as User;
	constructor(private authService: AuthService) {}

	handleLoginButtonClick() {
		console.log(this.user);
		this.authService.login(this.user);
	}
}
