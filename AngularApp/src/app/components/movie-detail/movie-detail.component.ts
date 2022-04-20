import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/model/movie';
import { AuthService } from 'src/app/services/auth.service';
import { CommentService } from 'src/app/services/comment.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.css']
})
export class MovieDetailComponent implements OnInit {
  comments: any[];
  commentForm: FormGroup;
  loggedUserId: number;
  userInfo;
  movie: Movie;
  imageUrl: string = '';
  returnUrl: string;
  imgToShowAndHide;
  fullSizeImage: boolean;

  constructor(private movieService: MovieService,
    private commentService: CommentService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    this.movieService.getMovieById(parseInt(id)).subscribe(m => {
      this.movie = m;
      this.imageUrl = 'assets/images/' + this.movie?.image;
    },err => console.log(err));

    this.getUserInfo();

    this.authService.getUserByEmail(this.userInfo.email).subscribe(u =>{
      this.loggedUserId = u?.id;
    });

    this.createForm();
    this.imgToShowAndHide = document.getElementById('fullSize');
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

  onShowFullSize(){
    this.fullSizeImage = true;
    let e = document.getElementsByTagName('html');
    for(let i = 0; i < e.length; i++){
      //e[i].style.opacity = '0.5';
    }
    //this.imgToShowAndHide.style.opacity = '2';
  }

  onHideImage(){
    this.fullSizeImage = false;
    let e = document.getElementsByTagName('html');
    for(let i = 0; i < e.length; i++){
      //e[i].style.opacity = '1';
    }
  }
}
