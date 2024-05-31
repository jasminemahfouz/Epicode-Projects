import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { Todo } from '../../../modulo/todo';

@Component({
  selector: 'app-completed',
  templateUrl: './completed.component.html',
  styleUrl: './completed.component.scss'
})
export class CompletedComponent implements OnInit {
  completedTodos: { id: number; todo: string; completed: boolean; userId: number; }[] = [];
  filteredCompletedTodos: Todo[] = [];
  searchQuery: string = '';

  constructor(private todoService: TodoService) {}

  ngOnInit() {
    this.completedTodos = this.todoService.getCompletedTodos();
    this.filteredCompletedTodos = this.completedTodos;
  }
  toggleCompletion(todoId: number, isChecked: boolean) {
    this.todoService.updateTodoStatus(todoId, isChecked);
  }
  filterCompletedTodos() {
    if (!this.searchQuery) {
      this.filteredCompletedTodos = this.completedTodos;
    } else {
      this.filteredCompletedTodos = this.completedTodos.filter(todo =>
        todo.todo.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
}}
