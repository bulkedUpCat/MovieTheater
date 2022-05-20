import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private http: HttpClient) { }

  getAllComments() : Observable<Comment[]>{
    return this.http.get<Comment[]>(`${environment.apiUrl}/comments`);
  }

  getCommentsByMovieId(id: number): Observable<Comment[]>{
    return this.http.get<Comment[]>(`${environment.apiUrl}/comments/movies/` + id);
  }

  addComment(comment: Comment){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type' : 'application/json'
      })
    };

    return this.http.post<Comment>(`${environment.apiUrl}/comments`,comment, headers);
  }

  deleteComment(id: number){
    return this.http.delete(`${environment.apiUrl}/comments/` + id);
  }
}
