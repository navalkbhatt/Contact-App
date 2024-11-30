import { createAction, createActionGroup, props } from '@ngrx/store';
import { Contact } from './contact.state';

export const createContact = createAction(
  '[Create Contact]',
  (contact: Contact) => ({
    contact,
  })
);

export const getContacts = createAction(
  '[Get All Contacts]',
  props<{ results: Array<Contact> }>()
);
