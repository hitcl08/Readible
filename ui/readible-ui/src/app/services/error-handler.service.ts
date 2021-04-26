import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { AppState } from '../app.state';
import { MonitoringService } from './monitoring.service';


@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  constructor(private appState: AppState, private monitoringService: MonitoringService){}
  public handleError(error: HttpErrorResponse): any {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    this.monitoringService.logException(error);
    return throwError(
      'Something bad happened; please try again later.');
  }
}
