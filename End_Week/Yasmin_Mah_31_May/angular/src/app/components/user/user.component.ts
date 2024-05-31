import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { UserService } from '../../services/users.service';
import { User } from '../../../modulo/user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit {
  users: { id: number; firstName: string; lastName: string; email: string; image: string; title: string; }[] = [];
  todos: { id: number; todo: string; completed: boolean; userId: number; }[] = [];
  searchQuery: string = '';
  filteredUsers: User[] = [];

  constructor(private userService: UserService, private todoService: TodoService) {}

  ngOnInit() {
    this.users = this.userService.getUsers();
    this.todos = this.todoService.getTodos();
    this.filteredUsers = this.users;
  }
  filterUsers() {
    if (!this.searchQuery) {
      this.filteredUsers = this.users;
      return;
    }
    this.filteredUsers = this.users.filter(user => {
      return (user.firstName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
              user.lastName.toLowerCase().includes(this.searchQuery.toLowerCase()));
    });
  }

  getUserTodos(userId: number) {
    return this.todos.filter(todo => todo.userId === userId);
  }
}