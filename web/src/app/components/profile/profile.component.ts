import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthService } from "src/app/services/auth/auth.service";
import { UserService } from "src/app/services/user/user.service";
import { User } from "src/types";

@Component({
	selector: "app-profile",
	templateUrl: "./profile.component.html",
	styleUrls: ["./profile.component.css"],
})
export class ProfileComponent {
	user: User = {} as User;
	constructor(private router: Router, private route: ActivatedRoute, private userService: UserService) {}

	ngOnInit() {
		const username = this.route.snapshot.paramMap.get("username");
		if (!username) throw new Error("Username is null");
		this.userService.getUser(username).subscribe((user) => {
			if (user) this.user = user;
		});
	}
}
