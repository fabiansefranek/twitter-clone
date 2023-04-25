import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth/auth.service";
import { PopupService } from "src/app/services/popup/popup.service";

@Component({
	selector: "app-navigation",
	templateUrl: "./navigation.component.html",
	styleUrls: ["./navigation.component.css"],
})
export class NavigationComponent {
	constructor(private router: Router, public authService: AuthService, public popupService: PopupService) {}

	isAuthenticated: boolean = false;

	font(path: string): string {
		return this.router.url == path ? "font-semibold" : "font-normal";
	}

	icon(path: string): string {
		let name = path.replace("/", "");
		if (name === "") name = "home";
		return this.router.url == path ? `assets/${name}-solid.svg` : `assets/${name}.svg`;
	}
}
