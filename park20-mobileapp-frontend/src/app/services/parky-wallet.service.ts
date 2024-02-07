import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ParkyWalletService {
  constructor(private http: HttpClient) {}

  getListTransactionsFromUser(username: string): Observable<any> {
    const url =
      environment.apiURL +
      '/api/ParkyWallet/ParkyWalletUsername/?username=' +
      username;
    return this.http.get(url);
  }
}
