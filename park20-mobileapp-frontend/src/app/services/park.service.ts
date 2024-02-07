import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ParkService {
  constructor(private http: HttpClient) {}

  getNearbyParksByLocation(
    latitude: number,
    longitude: number
  ): Observable<any> {
    const url =
      environment.apiURL +
      '/api/Park/GetAllParksByDistance?latitude=' +
      latitude +
      '&longitude=' +
      longitude;
    return this.http.get(url);
  }

  getParkingSpotsCount(parkName: string): Observable<any> {
    const url =
      environment.apiURL +
      '/api/Park/GetParkingSpotsCount?parkName=' +
      parkName;
    return this.http.get(url);
  }

  checkAvailableSpaceOnPark(
    parkName: string,
    licensePlate: string
  ): Observable<any> {
    const url =
      environment.apiURL +
      '/api/Park/GetAvailableSpace?LicencePlate=' +
      licensePlate +
      '&ParkName=' +
      parkName;
    return this.http.get(url);
  }
}
