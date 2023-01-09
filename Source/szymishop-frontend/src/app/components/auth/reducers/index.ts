import { ActionReducerMap, createFeatureSelector, createSelector } from '@ngrx/store';
import { createRehydrationSyncReducer } from '../../../utils/rehydration-utils';

import * as Auth from './auth.reducer';


export const featureKey = 'auth';

export interface State {
    auth: Auth.State;
}

export const authReducers: ActionReducerMap<State> = {
    auth: Auth.reducer
}

export const authMetaReducers = [
    createRehydrationSyncReducer([
            { auth: Auth.rehydratedProps }
        ],
        featureKey
    )
]


const selectState = createFeatureSelector<State>(featureKey);
const selectAuthState = createSelector(
    selectState,
    s => s.auth
);

export const selectIsLoggedIn = createSelector(
    selectAuthState,
    s => !!s.user
);

export const selectUser = createSelector(
    selectAuthState,
    s => s.user
);

export const selectLoginStatus = createSelector(
    selectAuthState,
    s => s.loginStatus
);

export const selectLoginError = createSelector(
    selectAuthState,
    s => s.loginError
);

export const selectRegisterError = createSelector(
    selectAuthState,
    s => s.registerError
);
