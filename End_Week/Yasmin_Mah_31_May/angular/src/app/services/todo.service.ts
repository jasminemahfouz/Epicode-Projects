import { Injectable } from '@angular/core';
import { todos } from '../../data/todos';
import { Todo } from '../../modulo/todo';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private todos: Todo[] = todos;

  getTodos(): Todo[] {
    return this.todos;
  }
  getCompletedTodos(): Todo[] {
    return this.todos.filter(todo => todo.completed);
  }
  getUncompletedTodos(): Todo[] {
    return this.todos.filter(todo => !todo.completed);
  }

  updateTodoStatus(id: number, completed: boolean) {
    const todo = this.todos.find(todo => todo.id === id);
    if (todo) {
      todo.completed = completed;
    }
  }
}
