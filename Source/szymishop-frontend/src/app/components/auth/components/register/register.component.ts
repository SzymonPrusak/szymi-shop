import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { RegisterModel } from './models/register.model';
import * as RegisterConst from '../../const/register.const';
import { noWhitespaceValidator } from '../../../../validators/no-whitespace.validators';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  protected RegisterConst = RegisterConst;

  @Input() set login(val: string) {
    this.loginControl.setValue(val);
  }
  @Input() set password(val: string) {
    this.passwordControl.setValue(val);
  }
  @Input() error?: string | null;
  @Input() loggingIn!: boolean;

  @Output() submit = new EventEmitter<RegisterModel>();

  protected form: FormGroup;
  protected loginControl: AbstractControl;
  protected passwordControl: AbstractControl;

  constructor(
    fb: FormBuilder
  ) {
    this.form = fb.group({
      login: [ '', [
        noWhitespaceValidator,
        Validators.minLength(RegisterConst.MIN_LOGIN_LENGTH),
        Validators.maxLength(RegisterConst.MAX_LOGIN_LENGTH)
      ] ],
      password: [ '', [
        Validators.minLength(RegisterConst.MIN_PASSWORD_LENGTH),
        Validators.maxLength(RegisterConst.MAX_PASSWORD_LENGTH)
      ] ]
    });
    this.loginControl = this.form.controls['login'];
    this.passwordControl = this.form.controls['password'];
  }


  protected onSubmit(): void {
    const model = this.form.value;
    this.submit.emit(model);
  }
}
