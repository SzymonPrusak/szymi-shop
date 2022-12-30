import { createAction, union } from '@ngrx/store';


export enum AuthActions {
    LogIn = 'app/auth/log-in',
    LogOut = 'app/auth/log-out'
};


export const logIn = createAction(
    AuthActions.LogIn
);

export const logOut = createAction(
    AuthActions.LogOut
);


const all = union({
    logIn,
    logOut
});
export type AuthActionTypes = typeof all;
