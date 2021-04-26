import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { CanAccessAuthGuard } from './auth.guard';
import { BooksComponent } from './books/books.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { SettingsComponent } from './settings/settings.component';
import { SubscriptionComponent } from './subscription/subscription.component';


const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent},
  { path: 'registration', component: RegistrationComponent },
  { path: 'subscription', component: SubscriptionComponent, canActivate: [CanAccessAuthGuard] },
  { path: 'books', component: BooksComponent, canActivate: [CanAccessAuthGuard] },
  { path: 'settings', component: SettingsComponent, canActivate: [CanAccessAuthGuard] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
