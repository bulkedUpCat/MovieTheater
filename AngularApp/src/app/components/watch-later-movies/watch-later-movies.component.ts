import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { SharedParamsService } from 'src/app/services/shared-params.service';

@Component({
  selector: 'app-watch-later-movies',
  templateUrl: './watch-later-movies.component.html',
  styleUrls: ['./watch-later-movies.component.css']
})
export class WatchLaterMoviesComponent implements OnInit {
  constructor(private location: Location) { }

  ngOnInit(): void {

  }

  goHome(){
    this.location.back();
  }
}
