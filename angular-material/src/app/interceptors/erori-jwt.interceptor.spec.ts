import { TestBed } from '@angular/core/testing';

import { EroriJWTInterceptor } from './erori-jwt.interceptor';

describe('EroriJWTInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      EroriJWTInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: EroriJWTInterceptor = TestBed.inject(EroriJWTInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
