import { Component, ElementRef, HostListener, Input, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth/auth.service";
import { PopupService } from "src/app/services/popup/popup.service";
import { Post, User } from "src/types";

@Component({
	selector: "app-post",
	templateUrl: "./post.component.html",
	styleUrls: ["./post.component.css"],
})
export class PostComponent {
	constructor(private router: Router, private authService: AuthService, public popupService: PopupService) {}
	user: User = {} as User;

	@Input() post: Post = {} as Post;

	@ViewChild("popup_button") popup_button: ElementRef<HTMLDivElement> = {} as ElementRef<HTMLDivElement>;
	isPopupShown: boolean = false;

	@HostListener("document:click", ["$event"])
	clickout(event: any) {
		if (this.popup_button && !this.popup_button.nativeElement.contains(event.target)) {
			this.hidePopup();
		}
	}

	ngOnInit() {
		const user = this.authService.getStoredTokenUser();
		if (user) this.user = user;
	}

	timeSince(timestamp: string): string {
		const intervals = [
			{ label: "year", seconds: 31536000 },
			{ label: "month", seconds: 2592000 },
			{ label: "day", seconds: 86400 },
			{ label: "hour", seconds: 3600 },
			{ label: "minute", seconds: 60 },
			{ label: "second", seconds: 1 },
		];
		const now = new Date();
		const date = new Date(parseInt(timestamp) * 1000);
		const seconds = Math.floor((now.getTime() - date.getTime()) / 1000);
		const interval = intervals.find((interval) => interval.seconds < seconds);
		if (interval != null) {
			const count = Math.floor(seconds / interval.seconds);
			const rtf = new Intl.RelativeTimeFormat("en", { style: "narrow" });
			return rtf.format(-count, interval.label as Intl.RelativeTimeFormatUnit);
		}
		return "just now";
	}

	togglePopup() {
		this.isPopupShown = !this.isPopupShown;
	}

	hidePopup() {
		this.isPopupShown = false;
	}

	reportPost() {}

	editPost() {
		this.popupService.openEditorPopup(this.post);
	}

	deletePost() {}
}
