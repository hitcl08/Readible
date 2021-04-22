import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {NavEnum} from '../enums/nav.enum';
@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {
  public navEnum = NavEnum;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onNavClick(page:NavEnum): void {
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
}
