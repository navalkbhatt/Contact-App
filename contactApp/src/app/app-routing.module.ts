import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactListComponent } from './components/contact-list-component/contact-list-component.component';
import { AddContactComponent } from './components/addcontact.component';
import { EditContactComponent } from './components/edit-contact/edit-contact.component';

const routes: Routes = [
  { path: '', redirectTo: '/contacts', pathMatch: 'full' }, // Default route
  { path: 'contacts', component: ContactListComponent }, // List view of contacts
  { path: 'contacts/add', component: AddContactComponent }, // Add a new contact
  { path: 'contacts/edit/:id', component: EditContactComponent }, // Edit a contact with ID
  { path: '**', redirectTo: '/contacts' }, // Wildcard route for a 404 page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
