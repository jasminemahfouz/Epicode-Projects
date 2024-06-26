import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { iUser } from '../../Models/i-user';
import { iMovie } from '../../Models/i-movie';
import { MovieService } from '../../movie.service';
import { iMoviePreferiti } from '../../Models/i-movie-preferiti';
import { MoviePreferitoService } from '../../movie-preferito.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild('highlightModal') highlightModal!: TemplateRef<any>;

  user!: iUser;
  users: iUser[] = [];
  movies: iMovie[] = [];
  favorites: iMoviePreferiti[] = [];
  highlightedMovie: iMovie | null = null;
  isModalOpen: boolean = false;

  constructor(
    private authSvc: AuthService,
    private movieSvc: MovieService,
    private moviePreferitoSvc: MoviePreferitoService,
    private modalService: NgbModal
  ) {}

  ngOnInit() {
    this.authSvc.user$.subscribe({
      next: user => {
        if (user) {
          this.user = user;
          this.loadUsers();
          this.loadFavorites();
        }
      },
      error: err => {
        console.error('Error fetching user', err);
      }
    });

    this.movieSvc.getAll().subscribe({
      next: movies => {
        this.movies = movies;
      },
      error: err => {
        console.error('Error fetching movies', err);
      }
    });
  }

  loadUsers() {
    this.authSvc.getAllUsers().subscribe({
      next: users => {
        this.users = users.filter(u => u.id !== this.user.id);
      },
      error: err => {
        console.error('Error fetching users', err);
      }
    });
  }

  loadFavorites() {
    this.moviePreferitoSvc.getFavoriteByUserId(this.user.id).subscribe({
      next: favorites => {
        this.favorites = favorites;
      },
      error: err => {
        console.error('Error fetching favorites', err);
      }
    });
  }

  addFavorite(movie: iMovie) {
    const isAlreadyFavorite = this.favorites.some(fav => fav.movie.id === movie.id);
    if (isAlreadyFavorite) {
      console.log('Il film è già nei preferiti');
      return;
    }
    const favorite: Partial<iMoviePreferiti> = {
      userId: this.user.id,
      movie: movie
    };
    this.moviePreferitoSvc.addFavorite(favorite).subscribe({
      next: newFavorite => {
        this.favorites.push(newFavorite);
      },
      error: err => {
        console.error('Error adding favorite', err);
      }
    });
  }

  removeFavorite(movieId: number) {
    const favoriteToRemove = this.favorites.find(fav => fav.movie.id === movieId);
    if (favoriteToRemove && favoriteToRemove.id) {
      this.moviePreferitoSvc.removeFavorite(favoriteToRemove.id).subscribe({
        next: () => {
          this.favorites = this.favorites.filter(fav => fav.movie.id !== movieId);
        },
        error: err => {
          console.error('Error removing favorite', err);
        }
      });
    }
  }

  usersVisible: boolean = false;
  moviesVisible: boolean = false;

  toggleUsers() {
    this.usersVisible = !this.usersVisible;
  }

  toggleMovies() {
    this.moviesVisible = !this.moviesVisible;
  }

  isFavorite(movie: iMovie): boolean {
    return this.favorites.some(fav => fav.movie.id === movie.id);
  }

  openModal(movie: iMovie) {
    this.highlightedMovie = movie;
    this.isModalOpen = true;
    this.modalService.open(this.highlightModal).result.finally(() => {
      this.isModalOpen = false; 
    });
  }

  closeModal(modal: any) {
    modal.dismiss();
    this.highlightedMovie = null;
    this.isModalOpen = false;
  }
}
