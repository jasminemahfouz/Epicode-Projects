import { iMovie } from "./i-movie"

export interface iMoviePreferiti {
  id: number;
  userId: number;
  movie: {
    id: number;
    title: string;
  };
}
