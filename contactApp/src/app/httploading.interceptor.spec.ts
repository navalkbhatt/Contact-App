import { TestBed } from '@angular/core/testing';

import { HttploadingInterceptor } from './httploading.interceptor';

describe('HttploadingInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      HttploadingInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: HttploadingInterceptor = TestBed.inject(HttploadingInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
