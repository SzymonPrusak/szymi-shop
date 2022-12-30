import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';

import { UserPanelComponent } from './containers/user-panel/user-panel.component';
import { authReducers, featureKey } from './reducers';
import { SharedModule } from '../../shared/shared.module';
import { EffectsModule } from '@ngrx/effects';
import { AuthEffects } from './effects/auth.effects';



@NgModule({
  declarations: [
    UserPanelComponent
  ],
  exports: [
    UserPanelComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    StoreModule.forFeature(featureKey, authReducers),
    EffectsModule.forFeature([ AuthEffects ])
  ]
})
export class AuthModule { }
