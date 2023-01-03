import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { LoginModel } from './models/login.model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  @Input() set login(val: string) {
    this.form.controls['login'].setValue(val);
  }
  @Input() set password(val: string) {
    this.form.controls['password'].setValue(val);
  }
  @Input() loggingIn!: boolean;
  @Input() error?: string | null;

  @Output() submit = new EventEmitter<LoginModel>();

  protected form: FormGroup

  constructor(
    fb: FormBuilder
  ) {
    this.form = fb.group({
      login: [ '', Validators.required ],
      password: [ '', Validators.required ]
    });
  }


  protected onSubmit(): void {
    const model = this.form.value;
    this.submit.emit(model);
  }
}
