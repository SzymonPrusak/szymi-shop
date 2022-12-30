import { Component } from '@angular/core';
import { Store } from '@ngrx/store';

import * as State from '../../reducers/index';
import * as Actions from '../../actions/auth.actions';


@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss']
})
export class UserPanelComponent {

  protected isLoggedIn$ = this.store.select(State.selectIsLoggedIn);

  constructor(
    private store: Store<State.State>
  ) {}


  protected logIn(): void {
    this.store.dispatch(Actions.logIn());
  }

  protected register(): void {
    
  }

  protected logOut(): void {
    this.store.dispatch(Actions.logOut());
  }
}
