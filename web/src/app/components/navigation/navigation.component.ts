import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth/auth.service";
import { PopupService } from "src/app/services/popup/popup.service";
import { UserService } from "src/app/services/user/user.service";
import { User } from "src/types";

@Component({
	selector: "app-navigation",
	templateUrl: "./navigation.component.html",
	styleUrls: ["./navigation.component.css"],
})
export class NavigationComponent {
	constructor(
		private router: Router,
		public authService: AuthService,
		public popupService: PopupService,
		private userService: UserService
	) {}

	user: User = {} as User;

	isAuthenticated: boolean = false;

	ngOnInit() {
		if (this.authService.user.username) {
			this.userService.getUser(this.authService.user.username).subscribe((user) => {
				if (user) this.user = user;
			});
		}
	}

	font(path: string): string {
		return this.router.url == path ? "font-semibold" : "font-normal";
	}

	icon(path: string, icon: string): string {
		return this.router.url == path ? `assets/${icon}-solid.svg` : `assets/${icon}.svg`;
	}
}
