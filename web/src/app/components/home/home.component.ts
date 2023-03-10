import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth/auth.service";
import { User } from "src/types";

@Component({
	selector: "app-home",
	templateUrl: "./home.component.html",
	styleUrls: ["./home.component.css"],
})
export class HomeComponent {
	user: User = {} as User;
	constructor(private authService: AuthService) {}

	ngOnInit() {
		const user = this.authService.getUser();
		if (user) this.user = user;
	}
}
