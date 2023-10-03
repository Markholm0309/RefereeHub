import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { CreateReportEvent, ReportEvent, UpdateEvent } from 'src/app/models/report/report';
import { EventService } from 'src/app/services/event.service';

@Component({
  selector: 'app-event-dialog',
  templateUrl: './event-dialog.component.html',
  styleUrls: ['./event-dialog.component.scss'],
  providers: [ConfirmationService, MessageService]
})
export class EventDialogComponent implements OnInit {
  @Input() events: ReportEvent[];
  @Input() reportId: number;

  visible: boolean;
  createMode: boolean;
  clonedEvents: { [s: string]: ReportEvent } = {};

  constructor(
    private service: EventService, 
    private confirmationService: ConfirmationService, 
    private messageService: MessageService,
    private router: Router) {}

  ngOnInit(): void {
    this.visible = true;
  }

  protected applyFilter(table: Table, event: Event) {
    return table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  protected createNewEvent(event: ReportEvent) {
    this.createEvent(new CreateReportEvent(event.description, event.timeStamp, this.reportId));
    this.router.navigateByUrl('/referees')
    this.createMode = false;
  }

  protected deleteEvent(reportEvent: ReportEvent, event: Event, rowIndex: number) {
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure that you want to delete this event?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteEventById(reportEvent.id);
        this.events[rowIndex] = this.clonedEvents[reportEvent.id];
        delete this.clonedEvents[reportEvent.id];
        this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: `You have deleted an event` });
      },
      reject: () => {
        this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have cancelled deletion' });
      }
    });
  }

  protected onRowEditInit(event: ReportEvent) {
    this.clonedEvents[event.id] = { ...event };
  }

  protected onRowEditCancel(event: ReportEvent, index: number) {
    this.events[index] = this.clonedEvents[event.id];
    delete this.clonedEvents[event.id];
  }

  protected onRowEditSave(event: ReportEvent) {
    delete this.clonedEvents[event.id];

    let body: UpdateEvent = {
      id: event.id,
      description: event.description,
      timeStamp: event.timeStamp,
      reportId: this.reportId
    }

    this.updateEvent(body);
  }

/* PRIVATE METHODS */

  private async updateEvent(body: UpdateEvent) {
    return (await this.service.updateEvent(body)).subscribe(res => {
      return res;
    });
  }

  private async createEvent(body: CreateReportEvent) {
    return (await this.service.createEvent(body)).subscribe(res => {
      return res;
    });
  }

  private async deleteEventById(id: number) {
    return (await this.service.deleteEvent(id)).subscribe(res => {
      return res;
    });
  }
}
