import { Component, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import * as State from '../../reducers/index';
import * as Actions from '../../actions/auth.actions';
import { LoginModel } from '../../components/login/models/login.model';
import { LoginStatus } from '../../model/auth.model';


@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss']
})
export class UserPanelComponent implements OnDestroy {

  protected LoginStatus = LoginStatus;

  @ViewChild('loginPanel') protected loginPanel!: TemplateRef<any>;

  protected isLoggedIn$ = this.store.select(State.selectIsLoggedIn);
  protected user$ = this.store.select(State.selectUser);
  protected loginStatus$ = this.store.select(State.selectLoginStatus);
  protected loginError$ = this.store.select(State.selectLoginError);

  private sub = new Subscription();
  private dialogRef: any;

  constructor(
    private store: Store<State.State>,
    private dialog: MatDialog
  ) {
    this.sub.add(
      this.loginStatus$.subscribe(s => {
        if (s === LoginStatus.LoggedIn) {
          this.dialogRef.close();
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

  }

  protected logOut(): void {
    this.store.dispatch(Actions.logOut());
  }

  protected onSubmit(model: LoginModel): void {
    this.store.dispatch(Actions.logIn(model));
  }
}
