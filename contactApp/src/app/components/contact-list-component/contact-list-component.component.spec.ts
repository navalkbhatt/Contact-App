import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactListComponentComponent } from './contact-list-component.component';

describe('ContactListComponentComponent', () => {
  let component: ContactListComponentComponent;
  let fixture: ComponentFixture<ContactListComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactListComponentComponent]
    });
    fixture = TestBed.createComponent(ContactListComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
