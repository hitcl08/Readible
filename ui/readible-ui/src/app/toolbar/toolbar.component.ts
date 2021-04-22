import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppState } from '../app.state';
import { NavEnum } from '../enums/nav.enum';
import { AuthService } from '../services/auth.service';
@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {
  public navEnum = NavEnum;

  constructor(public appState: AppState, private router: Router, private authService: AuthService) { }

  public ngOnInit(): void {
  }

  public onNavClick(page: NavEnum): void {
    switch (page) {
      case this.navEnum.Books:
        this.router.navigate(['/books']);
        break;
      case this.navEnum.Subscription:
        this.router.navigate(['/subscription']);
        break;

      default:
        break;
    }
  }

  public logout(): void {
    this.authService.isAuthenticated = false;
    this.appState.token = '';
    this.appState.showToolbar = false;
    this.router.navigate(['login']);
  }
}
