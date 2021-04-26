import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { environment } from 'src/environments/environment';

@Injectable()
export class MonitoringService {

  appInsights: ApplicationInsights;
  constructor() {
    this.appInsights = new ApplicationInsights({
      config: {
        instrumentationKey: environment.appInsights.instrumentationKey,
        enableAutoRouteTracking: true // option to log all route changes
      }
    });
    this.appInsights.loadAppInsights();
    this.appInsights.trackPageView();
  }

  public logPageView(name?: string, url?: string): void { // option to call manually
    this.appInsights.trackPageView({
      name,
      uri: url
    });
  }

  public logEvent(name: string, properties?: { [key: string]: any }): void {
    this.appInsights.trackEvent({ name }, properties);
  }

  public logMetric(name: string, average: number, properties?: { [key: string]: any }): void {
    this.appInsights.trackMetric({ name, average }, properties);
  }

  public logException(exception: Error, severityLevel?: number): void {
    this.appInsights.trackException({ exception, severityLevel });
  }

  public logHttpException(exception: HttpErrorResponse, severityLevel?: number): void {
    this.appInsights.trackException({ exception, severityLevel });
  }

  public logTrace(message: string, properties?: { [key: string]: any }): void {
    this.appInsights.trackTrace({ message }, properties);
  }
}
