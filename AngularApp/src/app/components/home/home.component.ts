import { Component, OnInit } from '@angular/core';
import { Movie } from 'src/app/model/movie';
import { AuthService } from 'src/app/services/auth.service';
import { MovieService } from 'src/app/services/movie.service';

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

  constructor(private movieService: MovieService,
    private authService: AuthService) { }

  ngOnInit(): void {

    this.getUserInfo();

    this.authService.getUserByEmail(this.userInfo?.email).subscribe(u =>{
      this.loggedUserId = u?.id;

      this.movieService.getWatchLaterMovies(this.loggedUserId).subscribe(m => {
        this.watchLaterMovies = m;
      })

      this.movieService.getFavoriteMovies(this.loggedUserId).subscribe(m => {
        this.favoriteMovies = m;
      })
    });
    console.log(this.userInfo);
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
}
