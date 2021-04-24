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
  isLoading: boolean;

  constructor(
    private router: Router,
    private authService: AuthService,
    private appState: AppState,
    private subscriptionService: SubscriptionService) { }

  ngOnInit(): void {
  }

  public onSubmit(): void {
    this.appState.isLoading = true;
    this.appState.token = this.authService.generateBasicToken(this.username, this.password);

    this.authService.login().subscribe((res) => {
      if (res.length > 0) {
        this.isLoading = true;
        this.authService.isAuthenticated = true;
        this.loginFailed = false;
        this.authService.getUserId(this.username).subscribe((res: any) => {
          this.appState.userId = res.id;
          this.appState.subscriptionId = res.subscription?.id
          console.log(res)

          this.subscriptionService.getUserSubscription(this.appState.userId).subscribe(res => {

            this.appState.subscriptionId = res.id;
            this.router.navigate(['/subscription']);
            this.isLoading = false;
            this.appState.showToolbar = true;

          })
        })
      }
    }, () => {
      this.authService.isAuthenticated = false;
      this.loginFailed = true;
    });
  }
}
