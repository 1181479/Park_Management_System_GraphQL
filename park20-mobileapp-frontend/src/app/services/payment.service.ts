import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAddPaymentMethodRequest } from '../models/IAddPaymentMethodRequest';
import { IPaymentMethodTokenRequest } from '../models/IPaymentMethodTokenRequest';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  constructor(private http: HttpClient) {}

  addPaymentMethod(data: IAddPaymentMethodRequest): Observable<any> {
    const url = environment.apiURL + '/api/Payment';
    return this.http.post(url, data);
  }

  getListPaymentMethodFromUser(username: string): Observable<any> {
    const url = environment.apiURL + '/api/Payment/GetAllFromUser/' + username;
    return this.http.get(url);
  }

  generateToken(data: IPaymentMethodTokenRequest): Observable<any> {
    const urlPaymentService =
      environment.apiPaymentService + '/api/Payment/GenerateToken';
    return this.http.post(urlPaymentService, data);
  }
}
