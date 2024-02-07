import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IAddVehicleRequest } from '../models/IAddVehicleRequest';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  constructor(private http: HttpClient) {}

  addVehicle(data: IAddVehicleRequest): Observable<any> {
    const url = environment.apiURL + '/api/Vehicle';
    return this.http.post(url, data);
  }

  getListVehicleFromUser(username: string): Observable<any> {
    const url = environment.apiURL + '/api/Vehicle/GetAllFromUser/' + username;
    return this.http.get(url);
  }
}
