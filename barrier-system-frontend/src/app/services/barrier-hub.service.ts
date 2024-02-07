// Seu serviço Angular
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BarrierHub {
  private hubConnection: signalR.HubConnection;
  private barrierRaisedSubject = new BehaviorSubject<boolean>(false);
  public barrierRaised$: Observable<boolean> =
    this.barrierRaisedSubject.asObservable();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.signalRBaseUrl}/barrierHub`)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Conectado ao hub SignalR');
      })
      .catch((err) => console.error(err));

    this.inicializarEventosDoHub();
  }

  private inicializarEventosDoHub() {
    this.hubConnection.on('OpenBarrier', () => {
      console.log('Barreira levantada');
      this.barrierRaisedSubject.next(true);
    });
    this.hubConnection.on('CloseBarrier', () => {
      console.log('Barreira em baixo');
      this.barrierRaisedSubject.next(false);
    });
  }
}
