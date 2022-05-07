import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedParamsService {
  pageNumber: number = 1;
  genres: Array<string> = new Array<string>();
  years: Array<number> = new Array<number>();
  showSortingOptions: boolean;
  showGenres: boolean;
  showYears: boolean;

  constructor() { }
}
