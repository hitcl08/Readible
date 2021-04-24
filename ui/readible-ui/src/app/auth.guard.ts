import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthService } from './services/auth.service';

@Injectable()
export class CanAccessAuthGuard implements CanActivate {

  constructor(private authService: AuthService) { }

  public canActivate(): boolean {
    return this.authService.isAuthenticated;
  }
}
