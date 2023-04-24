import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorPopupComponent } from './editor-popup.component';

describe('EditorPopupComponent', () => {
  let component: EditorPopupComponent;
  let fixture: ComponentFixture<EditorPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditorPopupComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditorPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
