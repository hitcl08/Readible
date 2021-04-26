import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, finalize, map, tap, timeout } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AppState } from '../app.state';
import { ErrorHandlerService } from './error-handler.service';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public isAuthenticated = false;

  constructor(
    private httpClient: HttpClient,
    private errorHandlerService: ErrorHandlerService,
    private appState: AppState
  ) { }
  public canActivate(): boolean {
    return this.isAuthenticated;
  }

  public login(): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.get(`${environment.readibleApiUri}/users`,
      {
        responseType: 'text'
      })
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }

  public getUserId(username: string): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.get(`${environment.readibleApiUri}/users/${username}`)
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }

  public generateBasicToken(username: string, password: string): string {
    const token = `${username}:${password}`;
    const hash = btoa(token);
    return `Basic ${hash}`;
  }

}
