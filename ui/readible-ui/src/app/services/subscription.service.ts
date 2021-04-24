import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { Observable } from 'rxjs';
import { SubscriptionRequest } from '../models/subscription.request';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private errorHandlerService: ErrorHandlerService, private httpClient: HttpClient) { }
  public registerNewUserSubscription(request: SubscriptionRequest): Observable<any> {
    return this.httpClient.post(`${environment.readibleApiUri}/subscriptions/users`,
      request)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }

  public getUserSubscription(userId: any): Observable<any> {
    return this.httpClient.get(`${environment.readibleApiUri}/subscriptions/users/${userId}`)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }

}
