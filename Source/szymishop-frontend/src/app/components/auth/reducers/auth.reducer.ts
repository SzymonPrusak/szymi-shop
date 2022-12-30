import { createReducer, on } from '@ngrx/store';

import { User } from '../model/user.model';
import * as Actions from '../actions/auth.actions';


export interface State {
    user: User | null
}

const initialState: State = {
    user: null
};


export const reducer = createReducer(initialState,
    on(Actions.logInSuccess, (s, a) => ({ ...s, user: a.user })),
    on(Actions.logOut, (s) => ({ ...s, user: null }))
);
