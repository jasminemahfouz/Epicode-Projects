import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cutText'
})
export class CutTextPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    return value.slice(0,40) + "..."
  }
}
