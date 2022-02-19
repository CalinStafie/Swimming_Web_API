import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientPageProfileComponent } from './client-page-profile.component';

describe('ClientPageProfileComponent', () => {
  let component: ClientPageProfileComponent;
  let fixture: ComponentFixture<ClientPageProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientPageProfileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientPageProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
