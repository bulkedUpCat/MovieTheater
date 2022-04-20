import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, ReplaySubject } from 'rxjs';
import { Movie } from '../model/movie';
import { environment } from 'src/environments/environment';
import { MovieUser } from '../model/movieUser';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private movieUser: MovieUser = new MovieUser();

  constructor(private http : HttpClient) { }

  getMovies() : Observable<Movie[]>{
    return this.http.get<Movie[]>(`${environment.apiUrl}/Movie`);
  }

  getMovieById(id: number) : Observable<Movie>{
    return this.http.get<Movie>(`${environment.apiUrl}/Movie/` + id);
  }

  // Watch Later List

  getWatchLaterMovies(userId: number) : Observable<Movie[]>{
    return this.http.get<Movie[]>(`${environment.apiUrl}/WatchLater/` + userId);
  }

  addToWatchLaterList(userId: number, movieId: number){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    this.movieUser.userId = userId;
    this.movieUser.movieId = movieId;

    return this.http.post(`${environment.apiUrl}/WatchLater`, this.movieUser, headers);
  }

  removeFromWatchLaterList(userId: number, movieId: number){
    this.movieUser.userId = userId;
    this.movieUser.movieId = movieId;

    return this.http.delete(`${environment.apiUrl}/WatchLater`, {body: this.movieUser});
  }

  // Favorite List

  getFavoriteMovies(userId: number) : Observable<Movie[]>{
    return this.http.get<Movie[]>(`${environment.apiUrl}/FavoriteList/` + userId);
  }

  addToFavoriteList(userId: number, movieId: number){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    this.movieUser.userId = userId;
    this.movieUser.movieId = movieId;

    return this.http.post(`${environment.apiUrl}/FavoriteList`, this.movieUser, headers);
  }

  removeFromFavoriteList(userId: number, movieId: number){
    this.movieUser.userId = userId;
    this.movieUser.movieId = movieId;

    return this.http.delete(`${environment.apiUrl}/FavoriteList`, {body: this.movieUser});
  }

}
