import {Component} from '@angular/core';

@Component({
  selector: 'my-component',
  standalone: true,
  template: `
    <svg:use [attr.xlink:href]="value"/>
    <svg:use id="foo" xlink:href="/foo" name="foo"/>
  `
})
export class MyComponent {
  value: any;
}
