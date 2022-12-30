import { ActionReducerMap, createFeatureSelector, createSelector } from '@ngrx/store';

import * as Auth from './auth.reducer';


export const featureKey = 'auth';

export interface State {
    auth: Auth.State;
}

export const authReducers: ActionReducerMap<State> = {
    auth: Auth.reducer
}


const selectState = createFeatureSelector<State>(featureKey);
const selectAuthState = createSelector(
    selectState,
    s => s.auth
);

export const selectIsLoggedIn = createSelector(
    selectAuthState,
    s => !!s.user
);
