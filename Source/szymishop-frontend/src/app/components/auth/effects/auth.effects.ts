import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, exhaustMap, map, of, tap } from 'rxjs';

import { AuthService } from '../services/auth.service';
import * as AuthActions from '../actions/auth.actions';
import { AuthTokenService } from '../services/auth-token.service';


@Injectable()
export class AuthEffects {
    readonly logIn$ = createEffect(() => this.actions$.pipe(
        ofType(AuthActions.logIn),
        exhaustMap(({ login, password }) => this.authService.login(login, password).pipe(
            map(({ user, authTokens }) => AuthActions.logInSuccess({ user, authTokens })),
            catchError(err => of(AuthActions.logInError({ message: err })))
        ))
    ));

    readonly logInSuccess$ = createEffect(() => this.actions$.pipe(
        ofType(AuthActions.logInSuccess),
        tap((a) => this.authTokenService.setTokens(a.authTokens))
    ), { dispatch: false });

    readonly register$ = createEffect(() => this.actions$.pipe(
        ofType(AuthActions.register),
        exhaustMap(({ login, password}) => this.authService.register(login, password).pipe(
            map(({ user, authTokens}) => AuthActions.logInSuccess({ user, authTokens })),
            catchError(err => of(AuthActions.registerError({ message: err })))
        ))
    ));

    readonly logOut$ = createEffect(() => this.actions$.pipe(
        ofType(AuthActions.logOut),
        tap(() => this.authTokenService.setTokens(null))
    ), { dispatch: false });


    constructor(
        private actions$: Actions,
        private authService: AuthService,
        private authTokenService: AuthTokenService
    ) {}
};
