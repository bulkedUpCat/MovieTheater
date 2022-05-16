import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NotifierService } from '../services/notifier.service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorCatchingInterceptor implements HttpInterceptor {

  constructor(private notifier: NotifierService,
    private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    console.log("it works!");
    return next.handle(request)
      .pipe(catchError((err: HttpErrorResponse) => {
        let errorMsg = '';

        if (err.error instanceof ErrorEvent){
          errorMsg = `Error: ${err.error}`;
        }
        else{
          errorMsg = `Error code: ${err.status}, Message: ${err.error}`;
        }

        this.notifier.showNotification(errorMsg, 'Ok', 'error');
        this.router.navigateByUrl('/error');

        return throwError(() => new Error(errorMsg));
      }));
  }
}
