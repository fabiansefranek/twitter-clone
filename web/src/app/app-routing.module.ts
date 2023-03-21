import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./components/login/login.component";
import { SignupComponent } from "./components/signup/signup.component";
import { HomeComponent } from "./components/home/home.component";
import { AuthGuard } from "./guards/auth/auth.guard";
import { ProfileComponent } from "./components/profile/profile.component";

const routes: Routes = [
	{ path: "", component: HomeComponent },
	{ path: "profile", component: ProfileComponent, canActivate: [AuthGuard] },
	{ path: "login", component: LoginComponent },
	{ path: "signup", component: SignupComponent },
	{
		path: "user/:username",
		component: ProfileComponent,
	},
];

@NgModule({
	declarations: [],
	imports: [CommonModule, RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
