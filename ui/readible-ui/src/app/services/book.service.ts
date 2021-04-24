import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class BookService {


  constructor(private httpClient: HttpClient, private errorHandlerService: ErrorHandlerService) { }

  public getBooks(): Observable<any> {
    return this.httpClient.get(`${environment.readibleApiUri}/books`)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }

  public addBookToSubscription(subscriptionId: any, bookId: number): Observable<any> {
    return this.httpClient.post(`${environment.readibleApiUri}/books/${bookId}/subscriptions/${subscriptionId}`,
      null
    ).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }

  public getBooksBySubscriptionId(subscriptionId: number): Observable<any>{
    return this.httpClient.get(`${environment.readibleApiUri}/books/subscriptions/${subscriptionId}`
    ).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }
}
