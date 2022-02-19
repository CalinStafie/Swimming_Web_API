import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceptionistsProfileComponent } from './receptionists-profile.component';

describe('ReceptionistsProfileComponent', () => {
  let component: ReceptionistsProfileComponent;
  let fixture: ComponentFixture<ReceptionistsProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReceptionistsProfileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReceptionistsProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
