import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { Todo } from '../../../modulo/todo';

@Component({
  selector: 'app-uncompleted',
  templateUrl: './uncompleted.component.html',
  styleUrls: ['./uncompleted.component.scss']
})
export class UncompletedComponent implements OnInit {
  uncompletedTodos: Todo[] = [];
  searchQuery: string = '';
  filteredUncompletedTodos: Todo[] = [];

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.uncompletedTodos = this.todoService.getUncompletedTodos();
    this.filteredUncompletedTodos = this.uncompletedTodos;
  }
  filterUncompletedTodos() {
    if (!this.searchQuery) {
      this.filteredUncompletedTodos = this.uncompletedTodos;
      return;
    }
    this.filteredUncompletedTodos = this.uncompletedTodos.filter(todo => {
      return todo.todo.toLowerCase().includes(this.searchQuery.toLowerCase());
    });
  }

  toggleCompletion(todoId: number, isChecked: boolean) {
    this.todoService.updateTodoStatus(todoId, isChecked);
  }
  onTodoChange(todo: Todo, event: Event) {
    const target = event.target as HTMLInputElement;
    if (target instanceof HTMLInputElement) {
      const checked = target.checked;
      this.toggleCompletion(todo.id, checked);
    }
  }
}