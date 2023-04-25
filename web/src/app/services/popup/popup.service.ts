import { ChangeDetectorRef, Injectable } from "@angular/core";
import { Post } from "src/types";

@Injectable({
	providedIn: "root",
})
export class PopupService {
	constructor() {}
	isEditorPopupShown: boolean = false;
	updatePost: Post = {} as Post;

	toggleEditorPopup = () => {
		this.isEditorPopupShown = !this.isEditorPopupShown;
	};

	closeEditorPopup = () => {
		this.isEditorPopupShown = false;
		this.updatePost = {} as Post;
	};

	openEditorPopup = (updatePost?: Post) => {
		this.isEditorPopupShown = true;
		if (updatePost) {
			this.updatePost = updatePost;
		}
	};
}
