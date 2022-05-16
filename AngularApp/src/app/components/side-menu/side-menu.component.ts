import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FilterModel } from 'src/app/model/filters';
import { MovieGenre } from 'src/app/model/movie';
import { MovieGenreService } from 'src/app/services/movie-genre.service';
import { SharedParamsService } from 'src/app/services/shared-params.service';


@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent implements OnInit {
  @Output() filters = new EventEmitter<FilterModel>();
  filterModel: FilterModel;
  showOptions: boolean;
  genres: MovieGenre[];
  chosenGenres: Array<string> = [];
  showGenres: boolean;
  years: Array<number> = [2022,2021,2020,2019,2018];
  chosenYears: Array<number> = [];
  showYears: boolean;
  runtime: Array<string> = ['10m', '30m', '1h', '1.5h', '2h'];
  chosenRuntime: Array<string> = [];
  showRuntime: boolean;

  constructor(public sharedParamsService: SharedParamsService,
    private genreService: MovieGenreService) { }

  ngOnInit(): void {
    this.genreService.getAllGenres().subscribe(g => {
      this.genres = g;
    })

    this.filterModel = new FilterModel();
    this.filterModel.genres = new Array<string>();
    this.filterModel.years = new Array<number>();
    this.chosenGenres = this.sharedParamsService.genres;
    this.chosenYears = this.sharedParamsService.years;
    this.showOptions = this.sharedParamsService.showSortingOptions;
    this.chosenGenres = this.sharedParamsService.genres;
    this.showYears = this.sharedParamsService.showYears;
  }

  showSortingOptions(){
    this.showOptions = !this.showOptions;
    this.sharedParamsService.showSortingOptions = !this.sharedParamsService.showSortingOptions;
  }

  closeMenu(){
    this.showOptions = false;
    this.sharedParamsService.showSortingOptions = false;
  }

  onSortByGenre(genre: MovieGenre){
    let alreadyChosen = false;
    let index = -1;

    for (let i = 0; i < this.chosenGenres.length; i++){
      if (this.chosenGenres[i] == genre.name){
        alreadyChosen = true;
        index = i;
        break;
      }
    }

    if (alreadyChosen){
      this.chosenGenres.splice(index, 1);
    }
    else
    {
      this.chosenGenres.push(genre.name);
    }

    this.filterModel.genres = this.chosenGenres;
    this.sharedParamsService.genres = this.chosenGenres;
    this.filters.emit(this.filterModel);
  }

  checkIfGenreIsChosen(genre: MovieGenre){
    for (let i = 0; i < this.chosenGenres.length; i++){
      if (this.chosenGenres[i] == genre.name){
        return true;
      }
    }

    return false;
  }

  onSortByYear(year){
    let alreadyChosen = false;
    let index = -1;

    for (let i = 0; i < this.chosenYears.length; i++){
      if (this.chosenYears[i] == year){
        alreadyChosen = true;
        index = i;
      }
    }

    if (alreadyChosen){
      this.chosenYears.splice(index,1);
    }
    else
    {
      this.chosenYears.push(year);
    }

    this.filterModel.years = this.chosenYears;
    this.sharedParamsService.years = this.chosenYears;
    this.filters.emit(this.filterModel);
  }

  checkIfYearIsChosen(year){
    for (let i = 0; i < this.chosenYears.length; i++){
      if (this.chosenYears[i] == year){
        return true;
      }
    }
    return false;
  }

  onSortByRuntime(runtime){
    let alreadyChosen = false;
    let index = -1;

    for (let i = 0; i < this.chosenRuntime.length; i++){
      if (this.chosenRuntime[i] == runtime){
        alreadyChosen = true;
        index = i;
        break;
      }
    }

    if (alreadyChosen){
      this.chosenRuntime.splice(index, 1);
    }
    else
    {
      this.chosenRuntime.push(runtime);
    }

    this.filterModel.runtime = this.chosenRuntime;
    this.sharedParamsService.runtime = this.chosenRuntime;
    this.filters.emit(this.filterModel);
  }

  checkIfRuntimeIsChosen(runtime){
    for (let i = 0; i < this.chosenRuntime.length; i++){
      if (this.chosenRuntime[i] == runtime){
        return true;
      }
    }

    return false;
  }
}
