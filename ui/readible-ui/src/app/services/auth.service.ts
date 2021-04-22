import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public isAuthenticated = false;

  constructor() { }
  public canActivate(): boolean {
    return this.isAuthenticated;
  }
}
