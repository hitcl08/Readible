import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { NewUserRequest } from '../models/new-user.request';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppState } from '../app.state';
import { UpdatePasswordRequest } from '../models/update-password.request';

@Injectable({
  providedIn: 'root'
})
export class UserService {


  constructor(private errorHandlerService: ErrorHandlerService, private httpClient: HttpClient, private appState: AppState) { }

  public registerNewUser(request: NewUserRequest): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.post(`${environment.readibleApiUri}/users`,
      request)
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }

  public deleteAccount(userId: number): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.delete(`${environment.readibleApiUri}/users/${userId}`)
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }

  public changePassword(updatePasswordReq: UpdatePasswordRequest): Observable<any> {
    this.appState.isLoading = true;
    return this.httpClient.put(`${environment.readibleApiUri}/users`,
      updatePasswordReq
    )
      .pipe(
        finalize(() => this.appState.isLoading = false),
        catchError(this.errorHandlerService.handleError)
      );
  }
}
