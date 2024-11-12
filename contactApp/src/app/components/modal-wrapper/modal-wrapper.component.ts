import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal-wrapper',
  templateUrl: './modal-wrapper.component.html',
  styleUrls: ['./modal-wrapper.component.css'],
})
export class ModalWrapperComponent {
  @Input() title = '';
  @Input() title2 = '';
  @Output() readonly closeModal = new EventEmitter();

  onCloseModal() {
    this.closeModal.emit();
  }
}
