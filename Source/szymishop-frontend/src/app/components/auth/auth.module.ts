import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';

import { UserPanelComponent } from './containers/user-panel/user-panel.component';
import { authMetaReducers, authReducers, featureKey } from './reducers';
import { SharedModule } from '../../shared/shared.module';
import { EffectsModule } from '@ngrx/effects';
import { AuthEffects } from './effects/auth.effects';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';



@NgModule({
  declarations: [
    UserPanelComponent,
    LoginComponent,
    RegisterComponent
  ],
  exports: [
    UserPanelComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    StoreModule.forFeature(featureKey, authReducers, { metaReducers: authMetaReducers }),
    EffectsModule.forFeature([ AuthEffects ])
  ]
})
export class AuthModule { }
