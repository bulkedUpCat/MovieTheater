import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ChangeUsernameDTO, User } from '../model/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUsers() : Observable<User[]>{
    return this.http.get<User[]>(`${environment.apiUrl}/User`);
  }

  changeUsername(changeDTO: ChangeUsernameDTO){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    return this.http.post<ChangeUsernameDTO>(`${environment.apiUrl}/User/changeUsername`,changeDTO,headers);
  }
}
