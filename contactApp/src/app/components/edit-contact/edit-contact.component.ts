import { Component, EventEmitter, Output, Input, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { single } from 'rxjs';
import { ContactResponse, EditContactDto } from 'src/app/models/contact.model';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.css'],
})
export class EditContactComponent {
  @Input() contact!: EditContactDto;
  @Output()
  readonly closeModal = new EventEmitter();
  contactService = inject(ContactService);
  userForm!: FormGroup;
  constructor(private router: Router, private fb: FormBuilder) {}
  ngOnInit() {
    this.userForm = this.fb.group({
      firstName: [this.contact.FirstName, Validators.required],
      lastName: [this.contact.LastName, Validators.required],
      email: [this.contact.Email, [Validators.required, Validators.email]],
    });
  }
  onCloseModal(): void {
    this.closeModal.emit();
  }
  onSubmit() {
    if (this.userForm.valid) {
      console.log(this.userForm.value);
      const updateContact = { ...this.userForm.value, Id: this.contact.Id };
      this.contactService.EditContact({ ...updateContact }).subscribe((x) => {
        console.log(x);
      });
    }
  }
}
