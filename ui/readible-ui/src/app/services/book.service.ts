import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AppState } from '../app.state';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class BookService {


  constructor(private httpClient: HttpClient, private errorHandlerService: ErrorHandlerService, private appState: AppState) { }

  public getBooks(): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.get(`${environment.readibleApiUri}/books`)
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }

  public addBookToSubscription(subscriptionId: any, bookId: number): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.post(`${environment.readibleApiUri}/books/${bookId}/subscriptions/${subscriptionId}`,
      null
    ).pipe(
      finalize(() => this.appState.isLoading = false),
      catchError(this.errorHandlerService.handleError)
    );
  }

  public getBooksBySubscriptionId(subscriptionId: number): Observable<any>{
    this.appState.isLoading = true;
    return this.httpClient.get(`${environment.readibleApiUri}/books/subscriptions/${subscriptionId}`
    ).pipe(
      finalize(() => this.appState.isLoading = false),
      catchError(this.errorHandlerService.handleError),
    );
  }

  public deleteBookFromSubscription(bookId: number, subscriptionId: number): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.delete(`${environment.readibleApiUri}/books/${bookId}/subscriptions/${subscriptionId}`)
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }
}
