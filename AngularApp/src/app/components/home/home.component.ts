import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Movie } from 'src/app/model/movie';
import { ChangeUsernameDTO } from 'src/app/model/user';
import { AuthService } from 'src/app/services/auth.service';
import { MovieService } from 'src/app/services/movie.service';
import { SharedParamsService } from 'src/app/services/shared-params.service';
import { UserService } from 'src/app/services/user.service';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  watchLaterMovies: Movie[];
  favoriteMovies: Movie[];
  watchLater: boolean;
  favorite: boolean;
  loggedUserId: string;
  userInfo;
  changeUsername: FormGroup;

  constructor(private movieService: MovieService,
    private authService: AuthService,
    private userService: UserService,
    private sharedParamsService: SharedParamsService,
    private fb: FormBuilder,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.resetMovieParams();
    this.getUserInfo();

    this.loggedUserId = this.userInfo.jti;

    this.movieService.getWatchLaterMovies(this.loggedUserId).subscribe(m => {
      this.watchLaterMovies = m;
    })

    this.movieService.getFavoriteMovies(this.loggedUserId).subscribe(m => {
      this.favoriteMovies = m;
    })
  }

  getUserInfo(){
    this.authService.getUserInfo().subscribe(u => {
      this.userInfo = u;
    })
  }

  onWatchLater(){
    this.watchLater = !this.watchLater;
  }

  onAddToFavorite(){
    this.favorite = !this.favorite;
  }

  resetMovieParams(){
    this.sharedParamsService.genres = [];
    this.sharedParamsService.showGenres = false;
    this.sharedParamsService.runtime = [];
    this.sharedParamsService.years = [];
    this.sharedParamsService.showYears = false;
    this.sharedParamsService.showSortingOptions = false;
  }
}
