import { ChangeDetectorRef, Component, ElementRef, HostListener, ViewChild } from "@angular/core";
import { Router } from "@angular/router";

@Component({
	selector: "app-root",
	templateUrl: "./app.component.html",
	styleUrls: ["./app.component.css"],
})
export class AppComponent {
	title = "twitter-clone";

	isEditorPopupShown: boolean = false;

	constructor(public router: Router, private cdRef: ChangeDetectorRef) {}

	openEditorPopup = () => {
		// arrow function, otherwise cdRef is undefined, because of "this" context
		this.cdRef.detectChanges(); // detect ui changes
		this.isEditorPopupShown = true;
	};

	closeEditorPopup = () => {
		this.isEditorPopupShown = false;
		this.cdRef.detectChanges();
	};
}
