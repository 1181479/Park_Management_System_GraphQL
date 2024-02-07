import { TestBed } from '@angular/core/testing';

import { ParkyWalletService } from './parky-wallet.service';

describe('ParkyWalletService', () => {
  let service: ParkyWalletService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParkyWalletService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
