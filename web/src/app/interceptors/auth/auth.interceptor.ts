import { Injectable } from "@angular/core";
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from "@angular/common/http";
import { Observable } from "rxjs";
import { AuthService } from "src/app/services/auth/auth.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	constructor(private authService: AuthService) {}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		const token = this.authService.getToken();
		if (token == null) return next.handle(request);
		return next.handle(request.clone({ setHeaders: { Authorization: `Bearer ${token}` } }));
	}
}
