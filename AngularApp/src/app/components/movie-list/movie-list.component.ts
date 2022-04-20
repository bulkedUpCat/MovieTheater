import { Component, OnInit } from '@angular/core';
import { FilterModel } from 'src/app/model/filters';
import { Movie } from 'src/app/model/movie';
import { AuthService } from 'src/app/services/auth.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {

  movies: Array<Movie> = [];
  filteredMovies: Array<Movie> = [];
  searchText: string;
  constructor(private movieService: MovieService,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.movieService.getMovies().subscribe(
      movies => {
        this.movies = movies;
        this.filteredMovies = movies;
      },
      err => console.log(err),
      () => console.log('all movies displayed'));
      this.authService.getUserInfo();
  }

  filterMovies(filterModel: FilterModel){
    this.filteredMovies = this.movies;
    let genres = filterModel.genres;
    let years = filterModel.years;

    // sorting by genre
    for (let i = 0; i < genres.length; i++){
      this.filteredMovies = this.filteredMovies.filter(m => m.genre.includes(genres[i]));
    }

    // sorting by year (only if at least one option specified)
    if (years.length > 0){
      this.filteredMovies = this.filteredMovies.filter(m => years.includes(m.releaseDate));
    }

  }
}


