import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceptionistInfoComponent } from './receptionist-info.component';

describe('ReceptionistInfoComponent', () => {
  let component: ReceptionistInfoComponent;
  let fixture: ComponentFixture<ReceptionistInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReceptionistInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReceptionistInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
