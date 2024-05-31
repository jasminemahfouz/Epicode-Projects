import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { UserService } from '../../services/users.service';
import { Todo } from '../../../modulo/todo';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent implements OnInit {
  users: { id: number; firstName: string; lastName: string; email: string; image: string; title: string; }[] = [];
  todos: Todo[] = [];
  searchQuery = '';
  filteredTodos: Todo[] = [];

  constructor(private todoService: TodoService, private userService: UserService) {}

  ngOnInit(): void {
    this.todos = this.todoService.getTodos();
    this.filteredTodos = this.todos;
  }

  getUserName(userId: number): string {
    const user = this.userService.getUserById(userId);
    if (user) {
      return `${user.firstName} ${user.lastName}`;
    } else {
      console.error(`User with ID ${userId} not found`);
      return 'Unknown';
    }
  }
  
  filterTodos() {
    if (!this.searchQuery) {
      this.filteredTodos = this.todos;
      return;
    }
    const searchQueryLower = this.searchQuery.toLowerCase();
    this.filteredTodos = this.todos.filter(todo => {
      const user = this.getUserName(todo.userId).toLowerCase();
      return user.includes(searchQueryLower);
    });
  }  

  toggleCompletion(todoId: number, isChecked: boolean) {
    this.todoService.updateTodoStatus(todoId, isChecked);
  }

  onTodoChange(todo: Todo, event: any) {
    const target = event.target as HTMLInputElement;
    const checked = target.checked || false;
    this.toggleCompletion(todo.id, checked);
  }
  
}
