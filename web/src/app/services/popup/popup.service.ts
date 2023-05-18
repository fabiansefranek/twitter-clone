import { ChangeDetectorRef, Injectable } from "@angular/core";
import { Post, User } from "src/types";

@Injectable({
	providedIn: "root",
})
export class PopupService {
	constructor() {}
	updatePost: Post = {} as Post;
	updateUser: User = {} as User;

	public popups: { [key: string]: { isShown: boolean } } = {
		editor: {
			isShown: false,
		},
		"profile-editor": {
			isShown: false,
		},
	};

	togglePopup = (popupName: string) => {
		this.popups[popupName].isShown = !this.popups[popupName].isShown;
	};

	closePopup = (popupName: string) => {
		this.popups[popupName].isShown = false;
	};

	openPopup = (popupName: string, args?: any) => {
		this.popups[popupName].isShown = true;
		if (args && args.post) {
			this.updatePost = args.post;
		}
	};
}
