import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppState } from './app.state';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService: AuthService){}

}
