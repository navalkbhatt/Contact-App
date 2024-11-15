import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { combineLatest } from 'rxjs';
import { ContactResponse } from 'src/app/models/contact.model';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'contact-list',
  templateUrl: './contact-list-component.component.html',
  styleUrls: ['./contact-list-component.component.css'],
})
export class ContactListComponent {
  enabled: boolean = false;
  contacts!: Array<ContactResponse>;
  constructor(private route: Router, private contactService: ContactService) {}
  ngOnInit() {
    combineLatest([this.contactService.getContacts()]).subscribe((x) => {
      this.contacts = x[0];
      console.log(this.contacts);
    });
  }
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
