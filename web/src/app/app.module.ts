import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./components/app/app.component";
import { LoginComponent } from "./components/login/login.component";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { SignupComponent } from "./components/signup/signup.component";
import { HomeComponent } from "./components/home/home.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { PostComponent } from "./components/post/post.component";
import { ErrorInterceptor } from "./interceptors/error/error.interceptor";
import { NavigationComponent } from "./components/navigation/navigation.component";
import { DataInterceptor } from "./interceptors/data/data.interceptor";
import { PostformComponent } from "./components/postform/postform.component";
import { AuthInterceptor } from "./interceptors/auth/auth.interceptor";
import { PostDetailComponent } from "./components/post-detail/post-detail.component";
import { PostDropdownComponent } from "./components/post-dropdown/post-dropdown.component";
import { RouterModule } from "@angular/router";
import { EditorPopupComponent } from "./components/editor-popup/editor-popup.component";

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		LoginComponent,
		SignupComponent,
		HomeComponent,
		ProfileComponent,
		PostComponent,
		NavigationComponent,
		PostformComponent,
		PostDetailComponent,
		PostDropdownComponent,
		EditorPopupComponent,
	],
	imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule, RouterModule],
	providers: [
		{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: DataInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
	],
	bootstrap: [AppComponent],
})
export class AppModule {}
