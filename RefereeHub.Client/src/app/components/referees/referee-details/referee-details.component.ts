import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { RefereeFormModel } from 'src/app/models/referee/createReferee';
import { Referee } from 'src/app/models/referee/referee';
import { UpdateReport } from 'src/app/models/report/report';
import { Report, ReportEvent, UpdateEvent } from 'src/app/models/report/report';
import { RefereeService } from 'src/app/services/referee.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-referee-details',
  templateUrl: './referee-details.component.html',
  styleUrls: ['./referee-details.component.scss'],
  providers: [ConfirmationService, MessageService]
})
export class RefereeDetailsComponent implements OnInit {
  eventsDialogVisible: boolean;
  updateRefereeDialogVisible: boolean;
  referee: Referee;
  reports: Report[];
  clonedReports: { [s: string]: Report } = {};

  events: ReportEvent[];
  modeState: boolean = false;
  activeReportTitle: string;

  reportId: number;
  private refereeId: number;
  
  constructor(
      private route: ActivatedRoute, 
      private reportService: ReportService, 
      private refereeService: RefereeService,
      private confirmationService: ConfirmationService, 
      private messageService: MessageService,
      private router: Router) {}

  ngOnInit(): void {
    this.referee = this.route.snapshot.data['referee'];
    this.getReports();
  }

  protected showEvents(events: ReportEvent[], reportTitle: string) {
    this.getReportId(reportTitle);
    this.events = events;
    this.activeReportTitle = reportTitle;
    this.eventsDialogVisible = true;
  }

  protected handleUpdatedReferee(referee: RefereeFormModel) {
    this.referee.fullName = referee.fullName;
    this.referee.age = referee.age;
    this.referee.currentLeague = referee.currentLeague;
  }

  protected showUpdateDialog() {
    this.updateRefereeDialogVisible = true;
  }

  protected deleteReferee(event: Event) {
    this.confirmationService.confirm({
        target: event.target,
        message: `Are you sure that you want to delete ${this.referee.fullName}?`,
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.deleteRefereeByName(this.referee.fullName);
            this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: `You have deleted ${this.referee.fullName}` });
            this.router.navigateByUrl('/referees');
        },
        reject: () => {
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have cancelled deletion' });
        }
    });
  }

  protected deleteReport(report: Report, event: Event, rowIndex: number) {
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure that you want to delete ${report.title}?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteReportById(report.id);
        this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: `You have deleted ${report.title}` });
        this.reports[rowIndex] = this.clonedReports[report.id];
        delete this.clonedReports[report.id];      
      },
      reject: () => {
        this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have cancelled deletion' });
      }
    });
  }

  protected applyFilter(table: Table, event: Event) {
    return table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  protected handleStateEvent(state: boolean) {
    this.updateRefereeDialogVisible = state;
  }

  protected handleEventDialogState(state: boolean) {
    this.eventsDialogVisible = state;
  }

  protected onRowEditInit(report: Report) {
    this.getReportId(report.title);
    this.getRefereeId(this.referee.fullName);
    this.clonedReports[report.id] = { ...report };
  }

  protected onRowEditCancel(report: Report, index: number) {
    this.reports[index] = this.clonedReports[report.id];
    delete this.clonedReports[report.id];
  }

  protected onRowEditSave(report: Report) {
    let events: UpdateEvent[] = [];
    report.events.forEach((event) => {
      let body: UpdateEvent = {
        id: event.id,
        description: event.description,
        timeStamp: event.description,
        reportId: report.id
      };

      events.push(body);
    })

    const body: UpdateReport = {
      id: this.reportId,
      title: report.title,
      refereeId: this.refereeId,
      events: events,
      rating: report.rating
    }

    this.updateReport(body);
  }

  /* PRIVATE METHODS */

  private async getReports() {
    return (await this.reportService.getReportsForRefereeByName(this.referee.fullName)).subscribe((res) => {
      this.reports = res;
    })
  }

  private async getReportId(title: string) {
    return (await this.reportService.getReportId(title)).subscribe((res) => {
      this.reportId = res;
      return res;
    });
  }

  private async deleteReportById(id: number) {
    return (await this.reportService.deleteReport(id)).subscribe(res => {
      return res;
    });
  }

  private async getRefereeId(name: string) {
    return (await this.refereeService.getRefereeId(name)).subscribe((res) => {
      this.refereeId = res;
      return res;
    })
  }

  private async updateReport(body: UpdateReport) {
    return (await this.reportService.updateReport(body)).subscribe(res => {
      return res;
    });
  }

  private async deleteRefereeByName(name: string) {
    return (await this.refereeService.deleteReferee(name)).subscribe(res => {
      return res;
    });
  }
}
