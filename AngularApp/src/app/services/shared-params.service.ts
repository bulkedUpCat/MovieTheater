import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedParamsService {
  userEmail: string;
  watchLater: boolean = false;
  favoriteList: boolean = false;
  pageNumber: number = 1;
  pageSize: number = 12;
  genres: Array<string> = new Array<string>();
  years: Array<number> = new Array<number>();
  runtime: Array<string> = new Array<string>();
  showSortingOptions: boolean;
  showGenres: boolean;
  showYears: boolean;

  constructor() { }
}
