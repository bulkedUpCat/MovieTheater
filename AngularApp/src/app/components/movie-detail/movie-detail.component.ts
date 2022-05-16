import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from 'src/app/model/movie';
import { AuthService } from 'src/app/services/auth.service';
import { CommentService } from 'src/app/services/comment.service';
import { MovieService } from 'src/app/services/movie.service';
import { NavigationService } from 'src/app/services/navigation.service';
import { NotifierService } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.css']
})
export class MovieDetailComponent implements OnInit {
  comments: any[];
  commentForm: FormGroup;
  loggedUserId: string;
  isLoggedIn: boolean;
  userInfo;
  movie: Movie;
  imageUrl: string = '';
  returnUrl: string;
  videoSource: string = '';

  constructor(private movieService: MovieService,
    private notifier: NotifierService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private navigation: Location) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    this.movieService.getMovieById(parseInt(id)).subscribe(m => {
      this.movie = m;
      this.imageUrl = 'assets/images/' + this.movie?.image;
      this.videoSource = m.trailerUrl;
      console.log(this.videoSource);
    },err => {
      this.notifier.showNotification(err.error, 'Ok', 'error');
      this.router.navigateByUrl('/movies');
    });

    this.getUserInfo();

    this.authService.isLoggedIn.subscribe( u => {
      this.isLoggedIn = u;
    });

    this.loggedUserId = this.userInfo.jti;

    this.createForm();

    this.createImgPopup();
  }

  createForm(){
    this.commentForm = this.fb.group({
      movieId: [this.movie?.id],
      text: [null]
    });
  }

  getUserInfo(){
    this.authService.getUserInfo().subscribe(u => {
      this.userInfo = u;
    })
  }

  createImgPopup(){
    const imgPopup = document.getElementById('grid-item-image');
    const popup = document.getElementById('img-popup');
      const overlay = document.getElementById('overlay');

    imgPopup.firstChild.addEventListener('click',() => {
      popup.classList.add('active');
      overlay.classList.add('active');
    })

    overlay.addEventListener('click', () => {
      popup.classList.remove('active');
      overlay.classList.remove('active');
    })

  }

  goBack(){
    this.navigation.back();
  }
}
