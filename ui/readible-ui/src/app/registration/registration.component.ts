import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppState } from '../app.state';
import { NewUserRequest } from '../models/new-user.request';
import { AuthService } from '../services/auth.service';
import { SubscriptionRequest } from '../models/subscription.request'
import { UserService } from '../services/user.service';
import { SubscriptionService } from '../services/subscription.service';
import { flatMap } from 'rxjs/operators';
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public hide = true;
  public username = '';
  public password = '';
  public registrationFailed = false;
  public isDisabled = false;
  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService,
    private subscriptionService: SubscriptionService,
    private appState: AppState) { }

  ngOnInit(): void {
  }

  public onSubmit(): void {
    this.isDisabled = true;
    this.appState.isLoading = true;

    const request = new NewUserRequest(this.username, this.password);

    this.userService.registerNewUser(request)
      .subscribe(isNewUserRegistered => {
;
        if (!isNewUserRegistered) {
          this.appState.isLoading = false;
          this.isDisabled = false;
          this.registrationFailed = true;
        } else {
          this.registerNewUser();
        }
      });
  }

  private registerNewUser(): void {
    this.appState.token = this.authService.generateBasicToken(this.username, this.password);
    this.authService.getUserId(this.username)

      .subscribe(res => {

        this.appState.userId = res.id;
        const subscriptionRequest = new SubscriptionRequest(this.appState.userId);
        this.subscriptionService.registerNewUserSubscription(subscriptionRequest)

          .subscribe(() => {
            this.subscriptionService.getUserSubscription(this.appState.userId).subscribe(res => {
              this.appState.subscriptionId = res.id;

              this.authService.isAuthenticated = true;
              this.router.navigate(['/books']);

              this.appState.showToolbar = true;
              this.appState.isLoading = false;
            })
          });
      });
  }
}
