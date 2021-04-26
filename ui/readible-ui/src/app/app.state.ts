import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppState {

  public showToolbar: boolean;
  public isLoading: boolean;
  public token = '';
  public userId: number;
  public subscriptionId: number;
}
