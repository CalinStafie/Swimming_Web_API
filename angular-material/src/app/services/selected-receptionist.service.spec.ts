import { TestBed } from '@angular/core/testing';

import { SelectedReceptionistService } from './selected-receptionist.service';

describe('SelectedReceptionistService', () => {
  let service: SelectedReceptionistService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SelectedReceptionistService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
