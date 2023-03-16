import { TestBed } from '@angular/core/testing';

import { DataInterceptor } from './data.interceptor';

describe('DataInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      DataInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: DataInterceptor = TestBed.inject(DataInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
