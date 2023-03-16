import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
	selector: "app-navigation",
	templateUrl: "./navigation.component.html",
	styleUrls: ["./navigation.component.css"],
})
export class NavigationComponent {
	constructor(private router: Router) {}

	font(path: string): string {
		return this.router.url == path ? "font-semibold" : "font-normal";
	}

	icon(path: string): string {
		let name = path.replace("/", "");
		if (name === "") name = "home";
		return this.router.url == path ? `assets/${name}-solid.svg` : `assets/${name}.svg`;
	}

	ngOnUpdate() {}
}
