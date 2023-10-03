import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private reportHubUrl = environment.reportHubUrl;
  private refereeHubUrl = environment.refereeHubUrl;
  public reportHubConnection: signalR.HubConnection;
  public refereeHubConnection: signalR.HubConnection;

  public startReportConnection = () => {
    this.reportHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.reportHubUrl)
      .build();

    this.reportHubConnection
      .start()
      .then(() => {
        console.log(`ReportHub state: ${this.reportHubConnection.state}`);      
      })
      .catch(err => console.log("Error while starting connection: " + err))
  }

  public startRefereeConnection = () => {
    this.refereeHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.refereeHubUrl)
      .build();

    this.refereeHubConnection
      .start()
      .then(() => {
        console.log(`RefereeHub state: ${this.refereeHubConnection.state}`);      
      })
      .catch(err => console.log("Error while starting connection: " + err))
  }

  public addClientMethod(hubConnection: string, methodName: string, method: (arg: any) => void) {
    switch (hubConnection) {
      case "report":
        this.reportHubConnection.on(methodName, (arg) => method(arg));
        break;

      case "referee":
        this.refereeHubConnection.on(methodName, (arg) => method(arg));
        break;

      default:
        console.log(`No connections with the name: ${hubConnection}`);
        break;
    }
  }
}
