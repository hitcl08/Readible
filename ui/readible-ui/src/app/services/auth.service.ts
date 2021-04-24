import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map, tap, timeout } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ErrorHandlerService } from './error-handler.service';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public isAuthenticated = false;

  constructor(
    private httpClient: HttpClient,
    private errorHandlerService: ErrorHandlerService
  ) { }
  public canActivate(): boolean {
    return this.isAuthenticated;
  }

  public login(): Observable<any> {
    return this.httpClient.get(`${environment.readibleApiUri}/users`,
      {
        responseType: 'text'
      })
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }

  public getUserId(username: string): Observable<any>  {
    return this.httpClient.get(`${environment.readibleApiUri}/users/${username}`)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }

  public generateBasicToken(username: string, password: string): string {
    const token = `${username}:${password}`;
    const hash = btoa(token);
    return `Basic ${hash}`;
  }

}
