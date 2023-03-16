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

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
	constructor() {}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		return next.handle(request).pipe(
			catchError((error: HttpErrorResponse) => {
				let errorMessage = "";
				if (error.error instanceof ErrorEvent) {
					// Client-side error
					errorMessage = `Client-side Error: ${error.error.message}`;
				} else {
					// Server-side error
					errorMessage = `Server-side Error: ${error.status}-${error.statusText}, Message: ${error.message}`;
				}
				console.error(errorMessage);
				return throwError(() => new Error(errorMessage));
			})
		);
	}
}
