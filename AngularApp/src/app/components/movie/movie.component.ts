import { Component, Input, OnInit } from '@angular/core';
import { Movie } from 'src/app/model/movie';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  @Input() movie: Movie;
  imageUrl: string;
  constructor() { }

  ngOnInit(): void {
    this.imageUrl = 'assets/images/' + this.movie.image;
  }

}
