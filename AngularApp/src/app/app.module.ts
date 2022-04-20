import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MovieListComponent } from './components/movie-list/movie-list.component';
import { MovieService } from './services/movie.service';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { UserSignupComponent } from './components/user-signup/user-signup.component';
import { AppRoutesModule } from './modules/app-routes/app-routes.module';
import { MovieComponent } from './components/movie/movie.component';
import { MovieDetailComponent } from './components/movie-detail/movie-detail.component';
import { AuthGuard } from './guards/auth.guard';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UserLogoutComponent } from './components/user-logout/user-logout.component';
import { JwtModule } from '@auth0/angular-jwt';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { HomeComponent } from './components/home/home.component';
import { ListButtonsComponent } from './components/list-buttons/list-buttons.component';
import { CommentComponent } from './components/comment/comment.component';
import { CommentService } from './services/comment.service';
import { SideMenuComponent } from './components/side-menu/side-menu.component';

export function tokenGetter() {
  return localStorage.getItem("TokenInfo");
}

@NgModule({
  declarations: [
    AppComponent,
    MovieListComponent,
    UserLoginComponent,
    UserSignupComponent,
    MovieComponent,
    MovieDetailComponent,
    NavbarComponent,
    UserLogoutComponent,
    HomeComponent,
    ListButtonsComponent,
    CommentComponent,
    SideMenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutesModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:7065'],
        disallowedRoutes: []
      }
    }),
    Ng2SearchPipeModule
  ],
  providers: [
    MovieService,
    AuthService,
    CommentService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
