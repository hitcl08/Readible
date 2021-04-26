import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AppState } from '../app.state';
import { UserService } from '../services/user.service';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { PasswordChangeDialogComponent } from './password-change-dialog/password-change-dialog.component';
import { PasswordDialogModel } from '../models/password-dialog';
import { UpdatePasswordRequest } from '../models/update-password.request';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  public panelOpenState = false;
  public deleteFailed = false;
  public confirmDelete: boolean;
  public passwordCreds: PasswordDialogModel;
  public showPopup = false;
  constructor(
    private userService: UserService,
    private router: Router,
    private appState: AppState,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
  }


  public openConfirmDialog(): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      maxWidth: '800px'
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.confirmDelete = dialogResult;
      if (this.confirmDelete) {
        this.userService.deleteAccount(this.appState.userId).subscribe(isDeleted => {
          isDeleted ? this.router.navigate(['login']) : this.deleteFailed = true;
        });
      }
    });
  }



  public openPasswordDialog(): void {
    const dialogRef = this.dialog.open(PasswordChangeDialogComponent, {
      width: '250px',
      height: '310px',
      data: { passwordCreds: this.passwordCreds }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.passwordCreds = result;
      const request = new UpdatePasswordRequest(this.appState.userId, this.passwordCreds.newPassword);
      this.userService.changePassword(request).subscribe(isChanged => {
        if (isChanged) {
          this.openPopup(`Your password has been updated succesfully`);
        } else {
          this.openPopup(`Your password has not been updated due to an error`);
        }
      });
    });
  }

  private openPopup(message: string): void {
    this.snackBar.open(message, 'X', {
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: ['popup']
    }).afterDismissed().subscribe(() => this.showPopup = false);
  }
}
