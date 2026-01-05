import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {

  constructor(private router: Router) {}

  goProducts() {
    this.router.navigate(['/list-product']);
  }

  goCreateProduct() {
    this.router.navigate(['/add-product']);
  }
}
