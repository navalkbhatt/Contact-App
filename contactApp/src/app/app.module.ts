import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddContactComponent } from './components/addcontact.component';
import { ContactListComponent } from './components/contact-list-component/contact-list-component.component';
import { EditContactComponent } from './components/edit-contact/edit-contact.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalWrapperComponent } from './components/modal-wrapper/modal-wrapper.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    AddContactComponent,
    ContactListComponent,
    EditContactComponent,
    ModalWrapperComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
