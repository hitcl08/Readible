import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { BooksComponent } from './books/books.component';
import { SubscriptionComponent } from './subscription/subscription.component';
import { MaterialModule } from './material-module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { AuthService } from './services/auth.service';
import { BookCardComponent } from './books/book-card/book-card.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SubscriptionBookComponent } from './subscription/subscription-book/subscription-book.component';
import { ErrorHandlerService } from './services/error-handler.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppState } from './app.state';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { UserService } from './services/user.service';
import { SubscriptionService } from './services/subscription.service';
import { BookService } from './services/book.service';
import { CanAccessAuthGuard } from './auth.guard';
import { SettingsComponent } from './settings/settings.component';
import { ConfirmationDialogComponent } from './settings/confirmation-dialog/confirmation-dialog.component';
import { PasswordChangeDialogComponent } from './settings/password-change-dialog/password-change-dialog.component';
import { MonitoringService } from './services/monitoring.service';
@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    LoginComponent,
    BooksComponent,
    SubscriptionComponent,
    ToolbarComponent,
    BookCardComponent,
    SubscriptionBookComponent,
    SettingsComponent,
    ConfirmationDialogComponent,
    PasswordChangeDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    HttpClientModule,
  ],
  providers: [
    AuthService,
    {
      provide: ErrorHandler,
      useClass: ErrorHandlerService
    },
    UserService,
    SubscriptionService,
    BookService,
    MonitoringService,
    AppState,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    CanAccessAuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
