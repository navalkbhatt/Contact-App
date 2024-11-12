import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'contact-list',
  templateUrl: './contact-list-component.component.html',
  styleUrls: ['./contact-list-component.component.css'],
})
export class ContactListComponent {
  enabled: boolean = false;
  constructor(private route: Router) {}
  navigateTo(route: string) {
    this.route.navigate([route]);
  }
  openedit() {
    this.enabled = true;
  }
  closeModel() {
    this.enabled = false;
  }
}
