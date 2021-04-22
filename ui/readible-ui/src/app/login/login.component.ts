import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppState } from '../app.state';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public hide = true;
  public username = '';
  public password = '';
  public loginFailed = false;

  constructor(private router: Router, private authService: AuthService, private appState: AppState) { }

  ngOnInit(): void {
  }

  public onSubmit(): void {
    this.appState.token = this.generateBasicToken();
    this.authService.login().subscribe((res) => {
      if (res.length > 0) {
        this.authService.isAuthenticated = true;
        this.appState.showToolbar = true;
        this.loginFailed = false;
        this.router.navigate(['/subscription']);
      }
    }, () => {
      this.authService.isAuthenticated = false;
      this.loginFailed = true;
    });
  }

  private generateBasicToken(): string {
    const token = `${this.username}:${this.password}`;
    const hash = btoa(token);
    return `Basic ${hash}`;
  }
}
