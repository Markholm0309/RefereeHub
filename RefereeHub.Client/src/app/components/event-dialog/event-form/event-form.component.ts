import { Component, EventEmitter, Output } from '@angular/core';
import { ReportEvent } from 'src/app/models/report/report';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.scss']
})
export class EventFormComponent {
  @Output() eventModel = new EventEmitter<ReportEvent>();

  event: ReportEvent = new ReportEvent();

  protected submitForm() {
    this.eventModel.emit(this.event);
  }
}
