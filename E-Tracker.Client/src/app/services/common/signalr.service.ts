import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { error } from 'jquery';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private _connection: HubConnection;
  get connection(): HubConnection{
    return this._connection;
  }
  start(hubIrl:string){
    if (!this.connection|| this._connection?.state==HubConnectionState.Disconnected) {
      const builder: HubConnectionBuilder = new HubConnectionBuilder();
      const hubConnection: HubConnection = builder.withUrl(hubIrl)
          .withAutomaticReconnect()
          .build();
          hubConnection.start()
          .then(()=> console.log("Connected"))
          .catch(error=>setTimeout(()=>this.start(hubIrl),2000));
           this._connection = hubConnection;
    }
    this._connection.onreconnected(connectionId=>console.log("Reconnected"));
    this._connection.onreconnecting(error=>console.log("Reconnecting"));
    this._connection.onclose(error=>console.log("Close reconnection"));

}
invoke(procedureName: string, message:any,successCallBack?:(value)=>void, errorCallBack?:
(error)=>void)  {
  this.connection.invoke(procedureName,message)
  .then(successCallBack)
  .catch(errorCallBack);
}
on(procedureName:string,callBack:(...message:any[])=>void){
  this.connection.on(procedureName,callBack);
}
}
