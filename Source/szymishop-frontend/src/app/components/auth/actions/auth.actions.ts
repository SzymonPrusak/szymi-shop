import { createAction, props, union } from '@ngrx/store';

import { AuthTokens } from '../model/auth.model';
import { User } from '../model/user.model';


export enum AuthActions {
    LogIn = 'app/auth/log-in',
    LogInSuccess = 'app/auth/log-in-success',
    LogInError = 'app/auth/log-in-error',
    Register = 'app/auth/register',
    RegisterError = 'app/auth/register-error',
    LogOut = 'app/auth/log-out'
};


export const logIn = createAction(
    AuthActions.LogIn,
    props<{ login: string, password: string }>()
);

export const logInSuccess = createAction(
    AuthActions.LogInSuccess,
    props<{ user: User, authTokens: AuthTokens }>()
);

export const logInError = createAction(
    AuthActions.LogInError,
    props<{ message: string }>()
);

export const register = createAction(
    AuthActions.Register,
    props<{ login: string, password: string }>()
);

export const registerError = createAction(
    AuthActions.RegisterError,
    props<{ message: string }>()
);

export const logOut = createAction(
    AuthActions.LogOut
);


const all = union({
    logIn,
    logInSuccess,
    logInError,
    register,
    registerError,
    logOut
});
export type AuthActionTypes = typeof all;
