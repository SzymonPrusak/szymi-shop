import { Component, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import * as State from '../../reducers/index';
import * as Actions from '../../actions/auth.actions';
import { LoginModel } from '../../components/login/models/login.model';
import { LoginStatus } from '../../model/auth.model';
import { RegisterModel } from '../../components/register/models/register.model';


@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss']
})
export class UserPanelComponent implements OnDestroy {

  protected LoginStatus = LoginStatus;

  @ViewChild('loginPanel') protected loginPanel!: TemplateRef<any>;
  @ViewChild('registerPanel') protected registerPanel!: TemplateRef<any>;

  protected isLoggedIn$ = this.store.select(State.selectIsLoggedIn);
  protected user$ = this.store.select(State.selectUser);
  protected loginStatus$ = this.store.select(State.selectLoginStatus);
  protected loginError$ = this.store.select(State.selectLoginError);
  protected registerError$ = this.store.select(State.selectRegisterError);

  private sub = new Subscription();
  private dialogRef?: any;

  constructor(
    private store: Store<State.State>,
    private dialog: MatDialog
  ) {
    this.sub.add(
      this.loginStatus$.subscribe(s => {
        if (s === LoginStatus.LoggedIn) {
          this.dialogRef?.close();
        }
      })
    );
  }


  ngOnDestroy(): void {
      this.sub.unsubscribe();
  }

  protected onLogIn(): void {
    this.dialogRef = this.dialog.open(this.loginPanel, { width: '400px', height: '280px' });
  }

  protected onRegister(): void {
    this.dialogRef = this.dialog.open(this.registerPanel, { width: '400px', height: '280px' });
  }

  protected logOut(): void {
    this.store.dispatch(Actions.logOut());
  }

  protected onLoggingIn(model: LoginModel): void {
    this.store.dispatch(Actions.logIn(model));
  }

  protected onRegistering(model: RegisterModel): void {
    this.store.dispatch(Actions.register(model));
  }
}
