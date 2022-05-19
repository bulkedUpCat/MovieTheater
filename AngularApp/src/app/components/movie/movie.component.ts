import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Movie } from 'src/app/model/movie';
import { AuthService } from 'src/app/services/auth.service';
import { MovieService } from 'src/app/services/movie.service';
import { DeleteMovieComponent } from '../dialogs/delete-movie/delete-movie.component';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  @Input() movie: Movie;
  imageUrl: string;
  isAdmin: boolean = false;

  constructor(private movieService: MovieService,
    private authService: AuthService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.imageUrl = 'assets/images/' + this.movie.image;

    this.authService.claims.subscribe(c => {
      if (c){
        this.isAdmin = c.includes('Admin');
      }
    })
  }

  onDeleteMovie(){
    this.dialog.open(DeleteMovieComponent, {
      data: {
        movieId: this.movie.id
      }
    });
  }
}
