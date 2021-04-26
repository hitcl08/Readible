import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppState } from '../app.state';
import { AuthService } from '../services/auth.service';
import { SubscriptionService } from '../services/subscription.service';

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

  constructor(
    private router: Router,
    private authService: AuthService,
    private appState: AppState,
    private subscriptionService: SubscriptionService
  ) { }

  ngOnInit(): void {
  }

  public onSubmit(): void {
    this.appState.token = this.authService.generateBasicToken(this.username, this.password);
    this.tryLogin();
  }

  private tryLogin(): void {
    this.authService.login().subscribe((res) => {
      if (res.length > 0) {
        this.authService.isAuthenticated = true;
        this.loginFailed = false;
        this.authService.getUserId(this.username).subscribe((user: any) => {
          this.appState.userId = user.id;
          this.appState.subscriptionId = user.subscription?.id;

          this.subscriptionService.getUserSubscription(this.appState.userId).subscribe(subscription => {

            this.appState.subscriptionId = subscription.id;
            this.router.navigate(['/subscription']);
            this.appState.showToolbar = true;
          });
        });
      }
    }, () => {
      this.authService.isAuthenticated = false;
      this.loginFailed = true;
    });
  }
}
