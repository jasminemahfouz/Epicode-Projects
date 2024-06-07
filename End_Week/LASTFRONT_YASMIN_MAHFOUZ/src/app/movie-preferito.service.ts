import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { iMoviePreferiti } from './Models/i-movie-preferiti';

@Injectable({
  providedIn: 'root'
})
export class MoviePreferitoService {
  private apiUrl: string = 'http://localhost:3000/favorites';

  constructor(private http: HttpClient) { }

  getFavorites(): Observable<iMoviePreferiti[]> {
    return this.http.get<iMoviePreferiti[]>(this.apiUrl);
  }

  getFavoriteByUserId(userId: number): Observable<iMoviePreferiti[]> {
    return this.http.get<iMoviePreferiti[]>(`${this.apiUrl}?userId=${userId}`);
  }

  addFavorite(movie: Partial<iMoviePreferiti>): Observable<iMoviePreferiti> {
    return this.http.post<iMoviePreferiti>(this.apiUrl, movie);
  }

  removeFavorite(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
