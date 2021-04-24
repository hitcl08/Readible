import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { NewUserRequest } from '../models/new-user.request';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private errorHandlerService: ErrorHandlerService, private httpClient: HttpClient) { }


  public registerNewUser(request: NewUserRequest): Observable<any> {
    return this.httpClient.post(`${environment.readibleApiUri}/users`,
      request)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }

}
