import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { iUser } from '../../Models/i-user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  user!: iUser;

  constructor(private authSvc: AuthService) {}

  ngOnInit() {
    this.authSvc.user$.subscribe(user => {
      if (user) {
        this.user = user;
      } else {
        // Se l'utente non è definito, reindirizzare o gestire l'errore
        // Questo è un esempio, puoi personalizzare questa logica come necessario
        console.error('User not defined');
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
