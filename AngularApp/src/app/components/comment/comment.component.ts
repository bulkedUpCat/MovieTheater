import { ThisReceiver } from '@angular/compiler';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/model/movie';
import { AuthService } from 'src/app/services/auth.service';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() movie: Movie;
  @Input() loggedUserId: string;
  isLoggedIn: boolean;
  commentForm: FormGroup;
  comments: any[];
  userId: number;

  constructor(private fb: FormBuilder,
    private commentService: CommentService,
    private authService: AuthService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id'));

    this.commentService.getCommentsByMovieId(id).subscribe(c => {
      this.comments = c;
      this.comments.reverse();
      console.log(this.comments);
    },err => console.log(err));

    this.authService.isLoggedIn.subscribe( u => {
      this.isLoggedIn = u;
    });

    this.createForm();
  }

  createForm(){
    this.commentForm = this.fb.group({
      movieId: [this.movie?.id],
      text: [null]
    });
  }

  addComment(){
    if (!this.commentForm.valid){
      return;
    }

    var comment = this.commentForm.value;
    comment.movieId = this.movie.id;
    comment.userId = this.loggedUserId;
    this.commentForm.reset();

    this.commentService.addComment(comment).subscribe(c =>
    {
      this.comments.unshift(c);
    },err => console.log(err));
  }
}
