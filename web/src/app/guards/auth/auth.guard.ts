import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../../services/auth/auth.service";

@Injectable({
	providedIn: "root",
})
export class AuthGuard {
	constructor(private router: Router, private authService: AuthService) {}
	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		return new Promise((resolve, reject) => {
			this.authService.isAuthenticated().then((isAuthenticated) => {
				if (!isAuthenticated) {
					this.router.navigate(["/login"]);
					resolve(false);
				} else {
					resolve(true);
				}
			});
		});
	}
}
