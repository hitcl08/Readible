import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

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

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  public onSubmit(): void {
    this.router.navigate(['/subscription']);
  }

}
