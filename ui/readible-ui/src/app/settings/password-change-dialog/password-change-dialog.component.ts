import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PasswordDialogModel } from 'src/app/models/password-dialog';

@Component({
  selector: 'app-password-change-dialog',
  templateUrl: './password-change-dialog.component.html',
  styleUrls: ['./password-change-dialog.component.scss']
})
export class PasswordChangeDialogComponent implements OnInit{
  public minPw = 8;
  public formGroup: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<PasswordChangeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PasswordDialogModel,
    private formBuilder: FormBuilder
  ) { }

  onNoClick(): void {
    this.dialogRef.close();
  }


  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(this.minPw)]],
      password2: ['', [Validators.required]]
    }, { validator: passwordMatchValidator });
  }

  get password() { return this.formGroup.get('password'); }
  get password2() { return this.formGroup.get('password2'); }

  onPasswordInput() {
    if (this.formGroup.hasError('passwordMismatch')) {
      this.password2.setErrors([{ passwordMismatch: true }]);
    }
    else {
      this.password2.setErrors(null);
    }
  }
}

export const passwordMatchValidator: ValidatorFn = (formGroup: FormGroup): ValidationErrors | null => {
  if (formGroup.get('password').value === formGroup.get('password2').value) {
    return null;
  } else {
    return { passwordMismatch: true };
  }
};
