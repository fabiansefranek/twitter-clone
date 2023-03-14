import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { User } from "src/types";

@Component({
	selector: "app-profile",
	templateUrl: "./profile.component.html",
	styleUrls: ["./profile.component.css"],
})
export class ProfileComponent {
	user: User = {} as User;
	constructor(private authService: AuthService) {}

	ngOnInit() {
		const user = this.authService.getUser();
		if (user) this.user = user;
	}

	logout() {
		this.authService.logout();
	}
}
