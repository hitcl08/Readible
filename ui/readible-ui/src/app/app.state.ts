import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppState {

  public showToolbar = false;
  public isLoading = false;
  public token = '';
  userId: any;
  subscriptionId: any;
}
