import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ContactService } from '../services/contact.service';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'add-contact',
  templateUrl: './addcontact.component.html',
  styleUrls: ['./addcontact.component.css'],
})
export class AddContactComponent {
  userForm!: FormGroup;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private contactService: ContactService
  ) {}
  ngOnInit() {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  onSubmit() {
    if (this.userForm.valid) {
      combineLatest([
        this.contactService.addContact({
          ...this.userForm.value,
        }),
      ]).subscribe((x) => {
        console.log('Hello');
      });

      console.log(this.userForm.value);
    }
  }
  navigateTo(route: string) {
    this.router.navigate([route]);
  }
}
