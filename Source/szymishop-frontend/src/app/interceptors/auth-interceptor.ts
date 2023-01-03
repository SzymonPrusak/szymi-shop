import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, filter, Observable, Subject, switchMap, take, takeUntil, throwError } from 'rxjs';

import { AuthTokens } from '../components/auth/model/auth.model';
import { AuthTokenService } from '../components/auth/services/auth-token.service';
import { AuthService } from '../components/auth/services/auth.service';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  private token$ = new BehaviorSubject<AuthTokens | null>(null);
  private refreshFailure$ = new Subject();
  private refreshing = false;

  constructor(
    private authService: AuthService,
    private authTokenService: AuthTokenService
  ) {}


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const accessToken = this.authTokenService.accessToken;
    const nReq = this.addAuthorizationHeader(req, accessToken);
    return next.handle(nReq).pipe(
      catchError(err => {
        if (err instanceof HttpErrorResponse && err.status === 401) {
          return this.refreshToken(req, next);
        }
        return throwError(err);
      })
    );
  }

  private addAuthorizationHeader(request: HttpRequest<any>, token: string | null): HttpRequest<any> {
    if (token) {
      return request.clone({ setHeaders: { Authorization: `bearer ${token}` }});
    }
    return request;
  }

  private refreshToken(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.refreshing) {
      return this.beginRefresh(request, next);
    }
    else {
      return this.waitForRefresh(request, next);
    }
  }

  private beginRefresh(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.refreshing = true;
    this.token$.next(null);

    const accessToken = this.authTokenService.accessToken;
    const refreshToken = this.authTokenService.refreshToken;
    if (!accessToken || !refreshToken) {
      return this.refreshFailure('No auth tokens');
    }
    const body = {
      accessToken,
      refreshToken
    };
    return this.authService.refresh(body).pipe(
      switchMap((t) => {
        this.refreshing = false;
        this.authTokenService.setTokens(t);
        this.token$.next(t);
        console.log('Token refresh successful');
        return next.handle(this.addAuthorizationHeader(request, t.accessToken));
      }),
      catchError(err => this.refreshFailure(err))
    );
  }

  private waitForRefresh(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return this.token$.pipe(
      takeUntil(this.refreshFailure$),
      filter(t => !!t),
      take(1),
      switchMap(t => next.handle(this.addAuthorizationHeader(request, t!.accessToken)))
    );
  }

  private refreshFailure(err: any): Observable<any> {
    this.refreshing = false;
    this.refreshFailure$.next(null);
    
    this.authTokenService.onTokenRefreshFailure();
    return throwError(err);
  }
}
