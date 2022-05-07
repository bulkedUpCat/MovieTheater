import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddMovieComponent } from 'src/app/components/add-movie/add-movie.component';
import { HomeComponent } from 'src/app/components/home/home.component';
import { MovieDetailComponent } from 'src/app/components/movie-detail/movie-detail.component';
import { MovieListComponent } from 'src/app/components/movie-list/movie-list.component';
import { UserLoginComponent } from 'src/app/components/user-login/user-login.component';
import { UserLogoutComponent } from 'src/app/components/user-logout/user-logout.component';
import { UserSignupComponent } from 'src/app/components/user-signup/user-signup.component';
import { UserTableComponent } from 'src/app/components/user-table/user-table.component';
import { AuthGuard } from 'src/app/guards/auth.guard';


const appRoutes : Routes = [
  {path: '', redirectTo: '/login', pathMatch: 'full'},
  {path: 'login', component: UserLoginComponent},
  {path: 'logout', component: UserLogoutComponent, canActivate: [AuthGuard]},
  {path: 'signup', component: UserSignupComponent},
  {path: 'movies', component: MovieListComponent},
  {path:'movie-detail/:id', component: MovieDetailComponent},
  {path: 'add-movie', component: AddMovieComponent, canActivate: [AuthGuard]},
  {path: 'user-table', component: UserTableComponent, canActivate: [AuthGuard]},
  {path:'home', component: HomeComponent, canActivate: [AuthGuard]},
  {path:'**', redirectTo: '/signup'}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(appRoutes)
  ]
})
export class AppRoutesModule { }
