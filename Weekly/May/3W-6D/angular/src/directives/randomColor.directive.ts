import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[postRandomColor]',
  standalone: true,
})
export class RandomColorDirective {
  constructor(private ref: ElementRef) {
    this.ref.nativeElement.style.backgroundColor = `rgb(
      ${Math.round(Math.random() * 255)},
      ${Math.round(Math.random() * 255)},
      ${Math.round(Math.random() * 255)}
      )`
  }
}
