import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, Subject, throwError } from 'rxjs';

import { User } from '../model/user.model';
import { AuthTokens, RefreshToken } from '../model/auth.model';
import { handleError } from '../../../utils/service-utils';


export interface LoginResponse {
    user: User;
    authTokens: AuthTokens;
};

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private _tokenRefreshFailure$ = new Subject();

    constructor(
        private http: HttpClient
    ) {}


    public login(login: string, password: string): Observable<LoginResponse> {
        const body = {
            login,
            password
        };
        return this.http.post<LoginResponse>('/Auth/Login', body).pipe(
            catchError(handleError(e => {
                if (e.status === 409) {
                    return throwError(() => 'Incorrect login or password');
                }
                return null;
            }))
        );
    }

    public register(login: string, password: string): Observable<LoginResponse> {
        const body = {
            login,
            password
        };
        return this.http.post<LoginResponse>('/Auth/Register', body).pipe(
            catchError(handleError(e => {
                if (e.status === 409) {
                    return throwError(() => 'Login already in use')
                }
                return null;
            }))
        )
    }

    public refresh(payload: AuthTokens): Observable<AuthTokens> {
        return this.http.post<AuthTokens>('/Auth/Refresh', payload);
    }
}
