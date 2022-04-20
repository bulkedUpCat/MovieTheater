export interface Movie {
  id: number;
  title: string;
  genre: Array<string>;
  image: string;
  releaseDate: number;
  description: string;
  director: string;
  runtimeHours: number;
}
