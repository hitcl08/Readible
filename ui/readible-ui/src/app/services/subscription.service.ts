import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { Observable } from 'rxjs';
import { SubscriptionRequest } from '../models/subscription.request';
import { environment } from 'src/environments/environment';
import { AppState } from '../app.state';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private errorHandlerService: ErrorHandlerService, private httpClient: HttpClient, private appState: AppState) { }
  public registerNewUserSubscription(request: SubscriptionRequest): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.post(`${environment.readibleApiUri}/subscriptions/users`,
      request)
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }

  public getUserSubscription(userId: any): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.get(`${environment.readibleApiUri}/subscriptions/users/${userId}`)
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }

}
