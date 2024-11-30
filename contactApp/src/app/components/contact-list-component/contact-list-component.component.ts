import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { combineLatest } from 'rxjs';
import { ContactResponse, EditContactDto } from 'src/app/models/contact.model';
import { ContactService } from 'src/app/services/contact.service';
import { RootState } from 'src/app/store/contact.state';
import { ConactSelectors } from 'src/app/store/selectors';

@Component({
  selector: 'contact-list',
  templateUrl: './contact-list-component.component.html',
  styleUrls: ['./contact-list-component.component.css'],
})
export class ContactListComponent {
  enabled: boolean = false;
  contacts!: Array<ContactResponse>;
  editContact!: EditContactDto;
  store = inject(Store);
  conts$ = this.store.select(ConactSelectors.selectContacts);

  constructor(private route: Router, private contactService: ContactService) {}
  ngOnInit() {
    combineLatest([
      this.store.select(ConactSelectors.selectContacts),
    ]).subscribe(([contacts]) => {
      this.contacts = contacts;
    });
  }
  navigateTo(route: string) {
    this.route.navigate([route]);
  }
  openedit(contact: {
    Id?: string;
    FirstName: string;
    LastName: string;
    Email: string;
  }) {
    this.enabled = true;
    this.editContact = { ...contact, Id: this.editContact.Id };
  }
  closeModel() {
    this.enabled = false;
  }
}
