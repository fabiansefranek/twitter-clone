import { Injectable } from "@angular/core";
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor,
	HttpErrorResponse,
	HttpResponse,
} from "@angular/common/http";
import { catchError, filter, map, Observable, throwError } from "rxjs";
import { AuthService } from "src/app/services/auth/auth.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
	constructor(private authService: AuthService) {}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		return next.handle(request).pipe(
			catchError((error: HttpErrorResponse) => {
				if (error.status === 401) this.authService.logout();
				let errorMessage = "";
				if (error.error instanceof ErrorEvent) {
					// Client-side error
					errorMessage = `Client-side Error: ${error.error.message}`;
				} else {
					// Server-side error
					errorMessage = `Server-side Error: ${error.status}-${error.statusText}, Message: ${error.message}`;
				}
				return throwError(() => new Error(errorMessage));
			})
		);
	}
}
