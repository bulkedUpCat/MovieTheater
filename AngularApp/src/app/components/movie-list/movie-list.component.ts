import { Component, OnInit } from '@angular/core';
import { FilterModel } from 'src/app/model/filters';
import { Movie } from 'src/app/model/movie';
import { MovieParameters } from 'src/app/model/movieParameters';
import { AuthService } from 'src/app/services/auth.service';
import { MovieService } from 'src/app/services/movie.service';
import { SharedParamsService } from 'src/app/services/shared-params.service';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {

  movies: Array<Movie> = [];
  filteredMovies: Array<Movie> = [];
  movieParameters: MovieParameters = new MovieParameters();
  searchText: string;

  constructor(private movieService: MovieService,
    private authService: AuthService,
    public sharedParamsService: SharedParamsService) { }

  ngOnInit(): void {
    this.configureMovieParameters();
    console.log(this.sharedParamsService.pageNumber);

    this.movieService.getMovies(this.movieParameters).subscribe(
      movies => {
        this.movies = movies;
        this.filteredMovies = movies;
      },
      err => console.log(err),
      () => console.log('all movies displayed'));
      this.authService.getUserInfo();
  }

  configureMovieParameters(){
    this.movieParameters.pageSize = 12;
    this.movieParameters.pageNumber = this.sharedParamsService.pageNumber;
    this.movieParameters.genres = this.sharedParamsService.genres;
    this.movieParameters.years = this.sharedParamsService.years;
  }

  filterMovies(filterModel: FilterModel){
    this.movieParameters.genres = this.sharedParamsService.genres;
    this.movieParameters.years = this.sharedParamsService.years;

    this.movieService.getMovies(this.movieParameters).subscribe(m => {
      this.filteredMovies = m;
    }, err =>{
      console.log(err);
    })
  }

  onNextPage(page: number){
    this.movieParameters.pageNumber = page;
    this.sharedParamsService.pageNumber = page;

    this.movieService.getMovies(this.movieParameters).subscribe(m => {
      this.filteredMovies = m;
    }, err => {
      console.log(err);
    })
  }

  onPreviousPage(page: number){
    this.movieParameters.pageNumber = page;
    this.sharedParamsService.pageNumber = page;

    this.movieService.getMovies(this.movieParameters).subscribe(m => {
      this.filteredMovies = m;
    }, err => {
      console.log(err);
    })
  }
}


