import { Keys, LocalStorageConfig, localStorageSync } from 'ngrx-store-localstorage';
import * as deepmerge from 'deepmerge';
import { MetaReducer } from '@ngrx/store';


const INIT_ACTION = '@ngrx/store/init';
const UPDATE_ACTION = '@ngrx/store/update-reducers';

const USED_KEY = '__$USED';

const mergeReducer = (state: any, rehydratedState: any, action: any) => {
    if ((action.type === INIT_ACTION || action.type === UPDATE_ACTION) && rehydratedState) {
        // Fix rehydration when using dynamic module loading, by ensuring rehydrated state is rehydrated only once.
        if (rehydratedState[USED_KEY]) {
            return state;
        }

        const overwriteMerge = (destinationArray: any, sourceArray: any, options: any) => sourceArray;
        const options: deepmerge.Options = {
            arrayMerge: overwriteMerge,
        };

        state = deepmerge(state, rehydratedState, options);
        
        rehydratedState[USED_KEY] = true;
    }

    return state;
};

export function createRehydrationConfig(keys: Keys, featureName: string) : LocalStorageConfig {
    return {
        keys,
        rehydrate: true,
        storageKeySerializer: (k) => `${featureName}_${k}`,
        mergeReducer
    };
}

export function createRehydrationSyncReducer(keys: Keys, featureName: string): MetaReducer<any, any> {
    const config = createRehydrationConfig(keys, featureName);
    return (r) => localStorageSync(config)(r);
}
