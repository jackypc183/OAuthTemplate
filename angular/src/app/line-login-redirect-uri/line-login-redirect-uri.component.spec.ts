import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LineLoginRedirectUriComponent } from './line-login-redirect-uri.component';

describe('LineLoginRedirectUriComponent', () => {
  let component: LineLoginRedirectUriComponent;
  let fixture: ComponentFixture<LineLoginRedirectUriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LineLoginRedirectUriComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LineLoginRedirectUriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
