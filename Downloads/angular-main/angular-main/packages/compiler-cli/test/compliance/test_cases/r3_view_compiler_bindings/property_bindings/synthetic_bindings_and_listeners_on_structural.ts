import {Component} from '@angular/core';

@Component({
  selector: 'my-cmp',
  standalone: true,
  template: `
    <button
      *ngIf="true"
      [@anim]="field"
      (@anim.start)="fn($event)">
    </button>
  `
})
export class MyComponent {
  field!: any;
  fn!: any;
}
