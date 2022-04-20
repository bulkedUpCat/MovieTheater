import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User, UserLogin, UserSignUp } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public loggedIn = new BehaviorSubject<boolean>(!!localStorage.getItem('TokenInfo'));
  //public userId = new BehaviorSubject<number>(parseInt(localStorage.getItem('id')));
  constructor(private http: HttpClient,
    private router: Router) { }

  get isLoggedIn(){
    return this.loggedIn.asObservable();
  }

  logIn(user: UserLogin) : Observable<any>{
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    return this.http.post<UserLogin>(`${environment.apiUrl}/User/login`,user,headers)
      .pipe(tap(user => {
        if (user && user.token){
          localStorage.setItem("TokenInfo",user.token);
          this.loggedIn.next(true);
        }
      }))
  }

  logOut(){
    this.loggedIn.next(false);
    localStorage.removeItem("TokenInfo");
    localStorage.removeItem("id");
    this.router.navigate(['/login']);
  }

  signUp(user: UserSignUp){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type' : 'application/json'
      })
    };

    return this.http.post<UserSignUp>(`${environment.apiUrl}/User/signup`,user,headers);
  }

  getUserInfo(){
    const token = localStorage.getItem('TokenInfo');
    let payload;
    if (token){
      payload = token.split('.')[1];
      payload = window.atob(payload);
      payload = JSON.parse(payload);
      return of(payload);
    }
    return of(null);
  }

  getUserByEmail(email: string) : Observable<User>{
    return this.http.get<User>(`${environment.apiUrl}/User/` + email);
  }
}
