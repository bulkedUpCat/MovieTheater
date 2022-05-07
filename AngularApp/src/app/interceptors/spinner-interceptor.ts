import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { finalize, Observable } from 'rxjs';import { SpinnerService } from '../services/spinner.service';
;

@Injectable()
export class SpinnerInterceptor implements HttpInterceptor {
  constructor(private readonly spinnerOverlayService: SpinnerService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.spinnerOverlayService.show();
    console.log('here');
    return next.handle(req).pipe(finalize(() => this.spinnerOverlayService.hide()));
  }
}
