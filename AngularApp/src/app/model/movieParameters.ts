export class MovieParameters{
  pageNumber:number;
  pageSize:number;
  genres: Array<string>;
  years: Array<number>;
  runtime: Array<number>;
  userEmail: string;
  watchLater: boolean;
  favoriteList: boolean;
  searchString: string;
}
