/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */

import {Component} from '@angular/core';
import {Routes} from '@angular/router';

@Component({
  standalone: true,
  template: 'default exported',
  selector: 'test-route',
})
export class TestRoute {
}


export default [
  {path: '', pathMatch: 'full', component: TestRoute},
] as Routes;
