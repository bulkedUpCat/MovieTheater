import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, ReplaySubject } from 'rxjs';
import { Movie, MovieDTO } from '../model/movie';
import { environment } from 'src/environments/environment';
import { MovieUser } from '../model/movieUser';
import { MovieParameters } from '../model/movieParameters';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private movieUser: MovieUser = new MovieUser();

  constructor(private http : HttpClient) { }

  getMovies(movieParameters: MovieParameters) : Observable<Movie[]>{

    return this.http.get<Movie[]>(`${environment.apiUrl}/movies`,{
      params: {
        pageNumber: movieParameters.pageNumber,
        pageSize: movieParameters.pageSize,
        genres: movieParameters.genres,
        years: movieParameters.years,
        userEmail: movieParameters.userEmail,
        watchLater: movieParameters.watchLater,
        favoriteList: movieParameters.favoriteList,
        searchString: movieParameters.searchString
      }
    });
  }

  getMovieById(id: number) : Observable<Movie>{
    return this.http.get<Movie>(`${environment.apiUrl}/movies/` + id);
  }

  addMovie(movie: MovieDTO){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    return this.http.post<Movie>(`${environment.apiUrl}/movies`,movie,headers)
  }

  deleteMovie(id: number){
    return this.http.delete(`${environment.apiUrl}/movies/` + id);
  }

  // Watch Later List

  getWatchLaterMovies(userId: string) : Observable<Movie[]>{
    return this.http.get<Movie[]>(`${environment.apiUrl}/WatchLater/` + userId);
  }

  addToWatchLaterList(userId: string, movieId: number){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    this.movieUser.userId = userId;
    this.movieUser.movieId = movieId;

    return this.http.post(`${environment.apiUrl}/WatchLater`, this.movieUser, headers);
  }

  removeFromWatchLaterList(userId: string, movieId: number){
    this.movieUser.userId = userId;
    this.movieUser.movieId = movieId;

    return this.http.delete(`${environment.apiUrl}/WatchLater`, {body: this.movieUser});
  }

  // Favorite List

  getFavoriteMovies(userId: string) : Observable<Movie[]>{
    return this.http.get<Movie[]>(`${environment.apiUrl}/FavoriteList/` + userId);
  }

  addToFavoriteList(userId: string, movieId: number){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    this.movieUser.userId = userId;
    this.movieUser.movieId = movieId;

    return this.http.post(`${environment.apiUrl}/FavoriteList`, this.movieUser, headers);
  }

  removeFromFavoriteList(userId: string, movieId: number){
    this.movieUser.userId = userId;
    this.movieUser.movieId = movieId;

    return this.http.delete(`${environment.apiUrl}/FavoriteList`, {body: this.movieUser});
  }

  // Emails

  createReport(movieParameters: MovieParameters){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    return this.http.post(`${environment.apiUrl}/movies/report`,movieParameters,headers);
  }

}
