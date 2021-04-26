import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { AppState } from './app.state';
import { AuthService } from './services/auth.service';
import { MonitoringService } from './services/monitoring.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService: AuthService, public appState: AppState, private monitoringService: MonitoringService){}

  private appInsights = new ApplicationInsights({
    config: {
      instrumentationKey: 'a8aca698-844c-4515-9875-fb6b480e4fec'
    }
  });
}
