import { createReducer, on } from '@ngrx/store';

import { User } from '../model/user.model';
import * as Actions from '../actions/auth.actions';
import { LoginStatus } from '../model/auth.model';


export interface State {
    user: User | null;
    loginStatus: LoginStatus;
    loginError: string | null;
}

const initialState: State = {
    user: null,
    loginStatus: LoginStatus.LoggedOut,
    loginError: null
};


export const reducer = createReducer(initialState,
    on(Actions.logIn, (s) => ({ ...s, loginStatus: LoginStatus.LoggingIn })),
    on(Actions.logInSuccess, (s, a) => ({
        ...s,
        user: a.user,
        loginStatus: LoginStatus.LoggedIn,
        loginError: null
    })),
    on(Actions.logInError, (s, { message }) => ({
        ...s,
        loginStatus: LoginStatus.LoggedOut,
        loginError: message
    })),
    on(Actions.logOut, (s) => ({
        ...s,
        user: null,
        loginStatus: LoginStatus.LoggedOut
    }))
);
