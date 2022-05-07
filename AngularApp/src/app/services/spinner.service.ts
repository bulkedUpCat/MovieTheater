import { Injectable } from '@angular/core';
import {Overlay, OverlayRef, OverlayModule} from '@angular/cdk/overlay';
import { SpinnerOverlayComponent } from '../components/spinner-overlay/spinner-overlay.component';
import { ComponentPortal } from '@angular/cdk/portal';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {
  private overlayRef: OverlayRef = undefined;

  constructor(private overlay: Overlay) { }

  public show(){
    Promise.resolve(null).then(() => {
      this.overlayRef = this.overlay.create({
        positionStrategy: this.overlay
          .position()
          .global()
          .centerHorizontally()
          .centerVertically(),
        hasBackdrop: true,
      });
      this.overlayRef.attach(new ComponentPortal(SpinnerOverlayComponent));
    });
  }

  public hide(){
    this.overlayRef.detach();
    this.overlayRef = undefined;
  }
}
