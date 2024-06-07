import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { iUser } from '../../Models/i-user';
import { iMoviePreferiti } from '../../Models/i-movie-preferiti';
import { MoviePreferitoService } from '../../movie-preferito.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  user!: iUser;
  favorites: iMoviePreferiti[] = [];

  constructor(private authSvc: AuthService, private moviePreferitoSvc: MoviePreferitoService) {}

  ngOnInit() {
    this.authSvc.user$.subscribe(user => {
      if (user) {
        this.user = user;
        this.loadFavorites();
      } else {
        console.error('User not defined');
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

  removeFavorite(favoriteId: number) {
    this.moviePreferitoSvc.removeFavorite(favoriteId).subscribe({
      next: () => {
        this.favorites = this.favorites.filter(fav => fav.id !== favoriteId);
      },
      error: err => {
        console.error('Error removing favorite', err);
      }
    });
  }

  confirmDelete() {
    const confirmation = confirm('Sei sicuro? Questa azione è irreversibile!');
    if (confirmation) {
      this.deleteProfile();
    }
  }

  deleteProfile() {
    this.authSvc.deleteUser(this.user.id).subscribe(() => {
      this.authSvc.logoutAfterDeletion();
    });
  }
}
