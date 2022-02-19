import { TestBed } from '@angular/core/testing';

import { InfoForUsersService } from './info-for-users.service';

describe('InfoForUsersService', () => {
  let service: InfoForUsersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InfoForUsersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
