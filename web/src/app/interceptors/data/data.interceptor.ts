import { Injectable } from "@angular/core";
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor,
	HttpResponse,
	HttpResponseBase,
} from "@angular/common/http";
import { filter, map, Observable } from "rxjs";

@Injectable()
export class DataInterceptor implements HttpInterceptor {
	constructor() {}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		return next.handle(request).pipe(
			filter((event: any) => event instanceof HttpResponse),
			map((event: HttpResponse<any>) => {
				return event.body.data ? event.clone({ body: event.body.data }) : event;
			})
		);
	}
}
