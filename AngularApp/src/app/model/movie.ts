export interface Movie {
  id: number;
  title: string;
  genres: Array<MovieGenre>;
  image: string;
  releaseDate: number;
  description: string;
  director: string;
  runtimeHours: number;
  trailerUrl: string;
}

export interface MovieDTO{
  title: number;
  genres: Array<string>;
  releaseDate: number;
  description: string;
  director: string;
  runtimeHours: number;
  image: string;
  trailerUrl: string;
}

export class MovieGenre{
  id: number;
  name: string;
}
