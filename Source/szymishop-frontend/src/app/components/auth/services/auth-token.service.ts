import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';

import * as State from '../reducers/index';
import * as Actions from '../actions/auth.actions';
import { AuthTokens, RefreshToken } from '../model/auth.model';


export const accessTokenKey = 'accessToken';
export const refreshTokenKey = 'refreshToken';

@Injectable({
    providedIn: 'root'
})
export class AuthTokenService {
    
    public get accessToken(): string | null {
        return localStorage.getItem(accessTokenKey);
    }
    public get refreshToken(): RefreshToken | null {
        const t = localStorage.getItem(refreshTokenKey);
        return t ? JSON.parse(t) : null;
    }

    constructor(
      private authStore: Store<State.State>
    ) {}

    
    public setTokens(tokens: AuthTokens | null): void {
        if (tokens) {
            localStorage.setItem(accessTokenKey, tokens.accessToken);
            localStorage.setItem(refreshTokenKey, JSON.stringify(tokens.refreshToken));
        }
        else {
            localStorage.removeItem(accessTokenKey);
            localStorage.removeItem(refreshTokenKey);
        }
    }

    public onTokenRefreshFailure(): void {
        this.authStore.dispatch(Actions.logOut());
    }
}
