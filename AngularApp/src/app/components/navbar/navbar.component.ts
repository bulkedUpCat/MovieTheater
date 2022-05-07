import { ChangeDetectionStrategy } from '@angular/compiler/src/compiler_facade_interface';
import { AfterViewChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { AddMovieComponent } from '../add-movie/add-movie.component';
import { UserTableComponent } from '../user-table/user-table.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit,AfterViewChecked {
  userLoggedIn: boolean;
  isAdmin: boolean = true;
  constructor(private authService: AuthService,
    private changeDetectorRef: ChangeDetectorRef,
    private dialog: MatDialog) { }

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

  openAddMovie(){
    this.dialog.open(AddMovieComponent);
  }
}
