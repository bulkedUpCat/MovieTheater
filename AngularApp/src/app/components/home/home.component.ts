import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Movie } from 'src/app/model/movie';
import { ChangeUsernameDTO } from 'src/app/model/user';
import { AuthService } from 'src/app/services/auth.service';
import { MovieService } from 'src/app/services/movie.service';
import { UserService } from 'src/app/services/user.service';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  watchLaterMovies: Movie[];
  favoriteMovies: Movie[];
  watchLater: boolean;
  favorite: boolean;
  loggedUserId: string;
  userInfo;
  changeUsername: FormGroup;

  constructor(private movieService: MovieService,
    private authService: AuthService,
    private userService: UserService,
    private fb: FormBuilder,
    private dialog: MatDialog) { }

  ngOnInit(): void {

    this.getUserInfo();

    this.loggedUserId = this.userInfo.jti;

    this.movieService.getWatchLaterMovies(this.loggedUserId).subscribe(m => {
      this.watchLaterMovies = m;
    })

    this.movieService.getFavoriteMovies(this.loggedUserId).subscribe(m => {
      this.favoriteMovies = m;
    })

    this.onShowChangeUserNameWindow();
    this.onCloseChangeUsernameWindow();
    this.onOverlayClick();

    this.createChangeUsername();
  }

  getUserInfo(){
    this.authService.getUserInfo().subscribe(u => {
      this.userInfo = u;
    })
  }

  onWatchLater(){
    this.watchLater = !this.watchLater;
  }

  onAddToFavorite(){
    this.favorite = !this.favorite;
  }

  createChangeUsername(){
    this.changeUsername = this.fb.group({
      newUsername: [null]
    });
  }

  onChangeUsername(){
    const changeUsernameDTO: ChangeUsernameDTO = new ChangeUsernameDTO();
    changeUsernameDTO.userId = this.loggedUserId;
    changeUsernameDTO.newUsername = this.changeUsername.controls['newUsername'].value;
    this.userService.changeUsername(changeUsernameDTO).subscribe(r => {
      alert('Username changed! The changes will be displayed once you log in again');
      document.getElementById('username-popup').classList.remove('active');
      document.getElementById('overlay').classList.remove('active');
    },err => {
      console.log(err);
    })
  }

  onShowChangeUserNameWindow(){
    const openButtons = document.getElementsByClassName('username-open-btn');

    for (let i = 0; i < openButtons.length; i++){
      openButtons[i].addEventListener('click',() => {
        const popup = document.getElementById('username-popup');
        const overlay = document.getElementById('overlay');
        popup.classList.add('active');
        overlay.classList.add('active');
      });
    }
  }

  onCloseChangeUsernameWindow(){
    const closeButtons = document.getElementsByClassName('username-close-btn');

    for (let i = 0; i < closeButtons.length; i++){
      closeButtons[i].addEventListener('click',() => {
        const popup = document.getElementById('username-popup');
        const overlay = document.getElementById('overlay');
        popup.classList.remove('active');
        overlay.classList.remove('active');
      });
    }
  }

  onOverlayClick(){
    const overlay = document.getElementById('overlay');
    const popup = document.getElementById('username-popup');

    overlay.addEventListener('click', () =>{
      popup.classList.remove('active');
      overlay.classList.remove('active');
    })
  }

  openDialog(){
    let dialogRef = this.dialog.open(DialogComponent, {data: {
      title: 'Change Email',
      content: 'Enter your new email:',
      leftBtnText: 'Cancel',
      rightBtnText: 'Submit',
    }});
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    })
  }
}
