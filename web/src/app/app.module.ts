import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./components/app/app.component";
import { LoginComponent } from "./components/login/login.component";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { SignupComponent } from "./components/signup/signup.component";
import { HomeComponent } from "./components/home/home.component";

@NgModule({
	declarations: [AppComponent, LoginComponent, LoginComponent, SignupComponent, HomeComponent],
	imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
	providers: [],
	bootstrap: [AppComponent],
})
export class AppModule {}
