import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegisterUserRequest } from '../models/IRegisterUserRequest';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  registerUser(data: IRegisterUserRequest): Observable<any> {
    const url = environment.apiURL + '/api/User';
    return this.http.post(url, data);
  }

  checkIfUserIsRegistered(username: string, password: string): Observable<any> {
    const url =
      environment.apiURL +
      '/api/User?username=' +
      username +
      '&password=' +
      password;
    return this.http.get(url);
  }

  getUserByUsername(username: string): Observable<any> {
    const url = environment.apiURL + '/api/User/GetUser/' + username;
    return this.http.get(url);
  }
}
