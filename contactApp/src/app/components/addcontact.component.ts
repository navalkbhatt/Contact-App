import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'add-contact',
  templateUrl: './addcontact.component.html',
  styleUrls: ['./addcontact.component.css'],
})
export class AddContactComponent {
  userForm!: FormGroup;
  constructor(private router: Router, private fb: FormBuilder) {}
  ngOnInit() {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  onSubmit() {
    if (this.userForm.valid) {
      console.log(this.userForm.value);
    }
  }
  navigateTo(route: string) {
    this.router.navigate([route]);
  }
}
