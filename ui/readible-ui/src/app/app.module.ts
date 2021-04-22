import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

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
import {AuthService} from './services/auth.service';
import { BookCardComponent } from './books/book-card/book-card.component';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    LoginComponent,
    BooksComponent,
    SubscriptionComponent,
    ToolbarComponent,
    BookCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule
  ],
  providers: [
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
