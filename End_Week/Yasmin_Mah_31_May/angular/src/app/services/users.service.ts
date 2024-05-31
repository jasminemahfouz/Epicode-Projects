import { Injectable } from '@angular/core';
import { users } from '../../data/users';
import { User } from '../../modulo/user'; // Importa il tipo User

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private users: User[] = users;

  getUsers(): User[] {
    return this.users;
  }
  getUserById(id: number): User | undefined {
    return this.users.find(user => user.id === id);
  }
}
