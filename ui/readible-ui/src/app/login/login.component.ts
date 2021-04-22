import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
  }

  public onSubmit(): void {
    this.authService.isAuthenticated = true;
    this.router.navigate(['/subscription']);
  }

}
