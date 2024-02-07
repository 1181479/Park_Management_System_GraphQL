import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ParkingSpotsCount } from '../models/ParkingSpotsCount';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private hubConnection: signalR.HubConnection;

  constructor(private http: HttpClient) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.signalRBaseUrl}/messagingHub`)
      .build();
    this.startConnection();
  }

  private startConnection() {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));
  }
  getParkingSpotNotifications(): Observable<any> {
    return new Observable<any>((observer) => {
      this.hubConnection.on('GeneralMessage', () => {
        console.log('General Message');
        observer.next({ message: 'General Message' });
      });

      this.hubConnection.on('WelcomeMessage', () => {
        console.log('Welcome Message');
        observer.next({ message: 'Welcome Message' });
      });

      this.hubConnection.on('GoodbyeMessage', (parkyCoinSpent, otherAmount, totalCost) => {
        console.log('Goodbye Message with Total Cost:', parkyCoinSpent, otherAmount, totalCost);
        observer.next({ message: 'Goodbye Message', parkyCoinSpent,  otherAmount, totalCost});
      });
    });
  }

  getParkingSpotsCount(parkName: string): Observable<ParkingSpotsCount> {
    const url = environment.apiURL + '/api/Park/GetParkingSpotsCount';
    const params = new HttpParams().set('parkName', parkName);
    return this.http.get<ParkingSpotsCount>(url, { params });
  }
}
