import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MovieService } from 'src/app/services/movie.service';
import { NotifierService } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-delete-movie',
  templateUrl: './delete-movie.component.html',
  styleUrls: ['./delete-movie.component.css']
})
export class DeleteMovieComponent implements OnInit {
  loaded: boolean = true;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public dialogRef: MatDialogRef<DeleteMovieComponent>,
  private movieService: MovieService,
  private notifier: NotifierService) { }

  ngOnInit(): void {
  }

  deleteMovie(){
    this.loaded = false;
    this.movieService.deleteMovie(this.data.movieId).subscribe(
    m => {
      this.movieService.deletedMovieId.next(this.data.movieId);
      this.notifier
        .showNotification(`You've just deleted the movie with id: ${this.data.movieId}`,'Ok','success');
    },
    err => {
      console.log(err);
    },
    () => {
      this.loaded = true;
      this.dialogRef.close();
    });

  }

}
