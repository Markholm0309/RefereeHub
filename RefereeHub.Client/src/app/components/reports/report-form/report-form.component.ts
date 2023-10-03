import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { CreateReport, CreateReportRequest, ReportEvent } from 'src/app/models/report/report';
import { RefereeService } from 'src/app/services/referee.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-report-form',
  templateUrl: './report-form.component.html',
  styleUrls: ['./report-form.component.scss']
})
export class ReportFormComponent implements OnInit {
  @Output() stateEvent = new EventEmitter<boolean>();

  createMode: boolean;
  report: CreateReport = new CreateReport();
  referees: string[] = [];

  refereeId: number;
  tempEvents: ReportEvent[] = [];
  tempEvent: ReportEvent = new ReportEvent();

  constructor(private refereeService: RefereeService, private reportService: ReportService) {}

  ngOnInit(): void {
    this.getReferees();
  }
  
  protected submitForm() {
    this.getRefereeId(this.report.referee);
    this.stateEvent.emit(false);
  }

  protected addNewEvent(event: ReportEvent) {
    const model = new ReportEvent(event.description, event.timeStamp);
    this.tempEvents.push(model);
    this.report.events = this.tempEvents;
    this.createMode = false;
  }

  private async getReferees() {
    return (await this.refereeService.getReferees()).subscribe((res) => {
      return res.filter(referee => this.referees.push(referee.fullName));
    })
  }

  private async getRefereeId(name: string) {
    return (await this.refereeService.getRefereeId(name)).subscribe((res) => {
      if (this.tempEvents.length <= 0) {
        this.report.events = [];
      }
      
      this.createReport(new CreateReportRequest(this.report.title, res, this.report.events, this.report.rating));
      return res;
    })
  }

  private async createReport(body: CreateReportRequest) {
    return (await this.reportService.createReport(body)).subscribe((res) => {
      return res;
    })
  }
}
