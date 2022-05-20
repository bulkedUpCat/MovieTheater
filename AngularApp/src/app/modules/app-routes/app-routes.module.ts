import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FavoriteListComponent } from 'src/app/components/favorite-list/favorite-list.component';
import { ForgotPasswordComponent } from 'src/app/components/dialogs/forgot-password/forgot-password.component';
import { HomeComponent } from 'src/app/components/home/home.component';
import { MovieDetailComponent } from 'src/app/components/movie-detail/movie-detail.component';
import { MovieListComponent } from 'src/app/components/movie-list/movie-list.component';
import { PasswordResetComponent } from 'src/app/components/password-reset/password-reset.component';
import { UserLoginComponent } from 'src/app/components/user-login/user-login.component';
import { UserLogoutComponent } from 'src/app/components/user-logout/user-logout.component';
import { UserSignupComponent } from 'src/app/components/user-signup/user-signup.component';
import { UserTableComponent } from 'src/app/components/user-table/user-table.component';
import { WatchLaterMoviesComponent } from 'src/app/components/watch-later-movies/watch-later-movies.component';
import { AuthGuard } from 'src/app/guards/auth.guard';


const appRoutes : Routes = [
  {path: '', redirectTo: '/movies', pathMatch: 'full'},
  {path: 'login', component: UserLoginComponent},
  {path: 'logout', component: UserLogoutComponent, canActivate: [AuthGuard]},
  {path: 'signup', component: UserSignupComponent},
  {path: 'forgot-password', component: ForgotPasswordComponent},
  {path: 'reset-password', component: PasswordResetComponent},
  {path: 'movies', component: MovieListComponent},
  {path:'movie-detail/:id', component: MovieDetailComponent},
  {path: 'user-table', component: UserTableComponent, canActivate: [AuthGuard]},
  {path:'home', component: HomeComponent, canActivate: [AuthGuard]},
  {path: 'home/watch-later', component: WatchLaterMoviesComponent, canActivate: [AuthGuard]},
  {path: 'home/favorite-list',component: FavoriteListComponent, canActivate: [AuthGuard]},
  {path:'**', redirectTo: '/signup'}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(appRoutes)
  ]
})
export class AppRoutesModule { }
