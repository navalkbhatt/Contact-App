import { createFeatureSelector, createSelector } from '@ngrx/store';
import { RootState } from './contact.state';

//export const selectContacts = (state: RootState) => state.results;
const featureState = createFeatureSelector<RootState>('results');

export const selectContacts = createSelector(
  featureState,
  (state) => state.results
);
