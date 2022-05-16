import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'

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
import { UserService } from './services/user.service';
import { PaginationComponent } from './components/pagination/pagination.component';
import { SpinnerOverlayComponent } from './components/spinner-overlay/spinner-overlay.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './modules/material/material.module';
import { SpinnerService } from './services/spinner.service';
import { OverlayModule } from '@angular/cdk/overlay';
import { NotifierService } from './services/notifier.service';
import { NotifierComponent } from './components/notifier/notifier.component';
import { DialogComponent } from './components/dialog/dialog.component';
import { AddMovieComponent } from './components/add-movie/add-movie.component';
import { UserTableComponent } from './components/user-table/user-table.component';
import { SharedParamsService } from './services/shared-params.service';
import { VideoComponent } from './components/video/video.component';
import { PasswordResetComponent } from './components/password-reset/password-reset.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { WatchLaterMoviesComponent } from './components/watch-later-movies/watch-later-movies.component';
import { CreateReportComponent } from './components/create-report/create-report.component';
import { FavoriteListComponent } from './components/favorite-list/favorite-list.component';

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
    SideMenuComponent,
    PaginationComponent,
    SpinnerOverlayComponent,
    NotifierComponent,
    DialogComponent,
    AddMovieComponent,
    UserTableComponent,
    VideoComponent,
    PasswordResetComponent,
    ForgotPasswordComponent,
    WatchLaterMoviesComponent,
    CreateReportComponent,
    FavoriteListComponent
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
    Ng2SearchPipeModule,
    BrowserAnimationsModule,
    MaterialModule,
    OverlayModule
  ],
  providers: [
    MovieService,
    AuthService,
    UserService,
    CommentService,
    SpinnerService,
    NotifierService,
    SharedParamsService,
    AuthGuard,
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: ErrorCatchingInterceptor,
    //   multi: true,
    // }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
