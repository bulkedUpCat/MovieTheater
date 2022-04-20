import { ChangeDetectionStrategy } from '@angular/compiler/src/compiler_facade_interface';
import { AfterViewChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit,AfterViewChecked {
  userLoggedIn: boolean;
  constructor(private authService: AuthService,
    private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe(u => {
      this.userLoggedIn = u;
    })
  }

  ngAfterViewChecked(): void {
    this.changeDetectorRef.detectChanges();
  }

  onLogOut(){
    this.authService.logOut();
  }
}
