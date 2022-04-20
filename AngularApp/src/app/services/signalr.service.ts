import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private _hubConnection : HubConnection;
  constructor() { }

  start(message: string){
    this._hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.apiUrl}/hub`).build();

    this._hubConnection.start().then(() => console.log('Connection started'))
    .catch(err => console.log(err));

    this._hubConnection.on('send',(message) => {
      const text = `${message}`;
    })
  }

  sendMessage(message: string){
    this._hubConnection.invoke('SendAsync',message)
    .catch(err => console.log(err));
  }
}
