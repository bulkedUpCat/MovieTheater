import { CDK_CONNECTED_OVERLAY_SCROLL_STRATEGY_PROVIDER_FACTORY } from '@angular/cdk/overlay/overlay-directives';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ForgotPasswordDTO, ResetPasswordDTO, User, UserLogin, UserSignUp } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public loggedIn = new BehaviorSubject<boolean>(!!localStorage.getItem('TokenInfo'));
  public claims = new BehaviorSubject<string[]>(null);

  constructor(private http: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService) { }

  get isLoggedIn(){
    const token = localStorage.getItem('TokenInfo');

    if (token && this.jwtHelper.isTokenExpired(token)){
      this.loggedIn.next(false);
      this.claims.next([]);
    }

    return this.loggedIn.asObservable();
  }

  logIn(user: UserLogin) : Observable<any>{
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    return this.http.post<UserLogin>(`${environment.apiUrl}/Auth/login`,user,headers)
      .pipe(tap(user => {
        if (user.token){
          localStorage.setItem("TokenInfo",user.token);
          this.loggedIn.next(true);
        }
      }))
  }

  logOut(){
    this.loggedIn.next(false);
    localStorage.removeItem("TokenInfo");
    this.claims.next([]);
    this.router.navigate(['/login']);
  }

  signUp(user: UserSignUp){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type' : 'application/json'
      })
    };

    return this.http.post<UserSignUp>(`${environment.apiUrl}/Auth/signup`,user,headers);
  }

  getUserInfo(){
    const token = localStorage.getItem('TokenInfo');
    let payload;

    if (token && !this.jwtHelper.isTokenExpired(token)){
      payload = token.split('.')[1];
      payload = window.atob(payload);
      payload = JSON.parse(payload);
      console.log(payload);
      this.claims.next(payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
      return of(payload);
    }
    return of(null);
  }

  forgotPassword(model: ForgotPasswordDTO){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    return this.http.post(`${environment.apiUrl}/Auth/forgotPassword`, model, headers)
  }

  resetPassword(model: ResetPasswordDTO){
    const headers = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    return this.http.post(`${environment.apiUrl}/Auth/passwordReset`, model, headers)
  }
}
