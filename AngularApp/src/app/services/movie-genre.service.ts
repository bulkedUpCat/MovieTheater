import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MovieGenre } from '../model/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieGenreService {

  constructor(private http: HttpClient) { }

  getAllGenres(): Observable<MovieGenre[]>{
    return this.http.get<MovieGenre[]>(`${environment.apiUrl}/genres`);
  }
}
