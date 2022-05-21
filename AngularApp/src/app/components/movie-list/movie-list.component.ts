import { Location } from '@angular/common';
import { ThisReceiver } from '@angular/compiler';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FilterModel } from 'src/app/model/filters';
import { Movie } from 'src/app/model/movie';
import { MovieParameters } from 'src/app/model/movieParameters';
import { AuthService } from 'src/app/services/auth.service';
import { MovieService } from 'src/app/services/movie.service';
import { SharedParamsService } from 'src/app/services/shared-params.service';
import { CreateReportComponent } from '../dialogs/create-report/create-report.component';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {
  @Input() watchLater: boolean = false;
  @Input() favoriteList: boolean = false;
  userInfo;
  filteredMovies: Array<Movie> = [];
  movieParameters: MovieParameters = new MovieParameters();
  searchString: string;

  constructor(private movieService: MovieService,
    private authService: AuthService,
    private dialog: MatDialog,
    public sharedParamsService: SharedParamsService,
    private location: Location) { }

  ngOnInit(): void {
    this.getUserInfo();
    this.configureMovieParameters();
    this.searchString = this.sharedParamsService.searchString;

    this.movieService.getMovies(this.movieParameters).subscribe(
      movies => {
        this.filteredMovies = movies;
      },
      err => console.log(err),
      () => console.log('all movies displayed'));


     // when movie is deleted
      this.movieService.deletedMovieId.subscribe( id => {
        if (id){
          this.movieService.getMovies(this.movieParameters).subscribe( m => {
            this.filteredMovies = m;
          })
        }
      })

      // when new movie is added
      this.movieService.addedMovie.subscribe( m => {
        if (m){
          this.movieService.getMovies(this.movieParameters).subscribe( m => {
            this.filteredMovies = m;
          })
        }
      })
  }

  getUserInfo(){
    this.authService.getUserInfo().subscribe(u => {
      if (u){
        this.userInfo = u;
        this.sharedParamsService.userEmail = u.email;
      }
    })
  }

  configureMovieParameters(){
    this.sharedParamsService.watchLater = this.watchLater;
    this.sharedParamsService.favoriteList = this.favoriteList;

    this.movieParameters = {
      pageSize: 12,
      pageNumber: this.sharedParamsService.pageNumber,
      genres: this.sharedParamsService.genres,
      years: this.sharedParamsService.years,
      runtime: this.sharedParamsService.runtime,
      userEmail: this.sharedParamsService.userEmail,
      watchLater: this.sharedParamsService.watchLater,
      favoriteList: this.sharedParamsService.favoriteList,
      searchString: this.sharedParamsService.searchString
    }
  }

  filterMovies(filterModel: FilterModel){
    this.movieParameters.genres = this.sharedParamsService.genres;
    this.movieParameters.years = this.sharedParamsService.years;
    this.movieParameters.runtime = this.sharedParamsService.runtime;

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

  createReport(){
    this.movieParameters.pageSize = 0;
    this.dialog.open(CreateReportComponent, {
      data: {
        params: this.movieParameters
      }
    });

    this.movieParameters.pageSize = this.sharedParamsService.pageSize;
  }

  onSearch(){
    this.movieParameters.searchString = this.searchString;
    this.sharedParamsService.searchString = this.searchString;

    this.movieService.getMovies(this.movieParameters).subscribe( m => {
      this.filteredMovies = m;
    })
  }

  goHome(){
    this.location.back();
  }
}
