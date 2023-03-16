import { Component, Input } from "@angular/core";

@Component({
	selector: "app-post",
	templateUrl: "./post.component.html",
	styleUrls: ["./post.component.css"],
})
export class PostComponent {
	@Input() text: string = "";
	@Input() username: string = "";
	@Input() createdAt: string = "";
	@Input() fullname: string = "";

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
}
