import { Action, createReducer, on } from '@ngrx/store';

import { createContact } from './contact.actions';
import { ConactActions } from './actions';
import { Contact, RootState } from './contact.state';
import { state } from '@angular/animations';

export const initialState: RootState = {
  results: [{ Id: '', FirstName: '', LastName: '', Email: '' }],
};

export const contactsReducer = createReducer(
  initialState,
  on(ConactActions.getContacts, (state: RootState, action) => {
    state = {
      ...state,
    };
    return state;
  }),
  on(ConactActions.createContact, (state: RootState, action) => {
    state = {
      ...state,
      results: [...state.results, action.contact],
    };
    return state;
  })
);
export function reducer(state: RootState | undefined, action: Action) {
  return contactsReducer(state, action);
}
