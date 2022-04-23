import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Movie } from 'src/app/model/movie';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-list-buttons',
  templateUrl: './list-buttons.component.html',
  styleUrls: ['./list-buttons.component.css']
})
export class ListButtonsComponent implements OnInit,OnChanges {
  @Input() movie: Movie;
  @Input() loggedUserId: string;
  watchLater: boolean;
  favorite: boolean;
  constructor(private movieService: MovieService) { }

  ngOnChanges(): void {
    this.movieService.getWatchLaterMovies(this.loggedUserId).subscribe(m =>{
      for (var i = 0; i < m.length; i++){
        if (m[i].title == this.movie.title &&
            m[i].description == this.movie.description)
        {
          this.watchLater = true;
        }
      }
    })

    this.movieService.getFavoriteMovies(this.loggedUserId).subscribe(m =>{
      for (var i = 0; i < m.length; i++){
        if (m[i].title == this.movie.title &&
            m[i].description == this.movie.description)
        {
          this.favorite = true;
        }
      }
    })
  }

  ngOnInit(): void {
  }

  onWatchLater(){
    this.movieService.addToWatchLaterList(this.loggedUserId, this.movie.id).subscribe(m =>{
      this.watchLater = true;
    }, err => {
      console.log(err);
    })
  }

  onRemoveFromWatchLater(){
    this.movieService.removeFromWatchLaterList(this.loggedUserId, this.movie.id).subscribe(m =>{
      this.watchLater = false;
    }, err => {
      console.log(err);
    })
  }

  onAddToFavorite(){
    this.movieService.addToFavoriteList(this.loggedUserId, this.movie.id).subscribe(m =>{
      this.favorite = true;
    }, err => {
      console.log(err);
    })
  }

  onRemoveFromFavorite(){
    this.movieService.removeFromFavoriteList(this.loggedUserId, this.movie.id).subscribe(m =>{
      this.favorite = false;
    }, err => {
      console.log(err);
    })
  }
}
