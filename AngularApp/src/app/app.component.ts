import { AfterViewChecked, Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'AngularApp';
  loggedUser;
  constructor(private authService: AuthService){}

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe(u =>{
      this.loggedUser = u;
    })
  }
}
