import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppState {

  public showToolbar = false;
  public token = '';
}
