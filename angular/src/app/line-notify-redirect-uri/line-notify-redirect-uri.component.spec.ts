import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LineNotifyRedirectUriComponent } from './line-notify-redirect-uri.component';

describe('LineNotifyRedirectUriComponent', () => {
  let component: LineNotifyRedirectUriComponent;
  let fixture: ComponentFixture<LineNotifyRedirectUriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LineNotifyRedirectUriComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LineNotifyRedirectUriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
