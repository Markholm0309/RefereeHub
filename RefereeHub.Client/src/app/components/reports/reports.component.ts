import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Referee } from 'src/app/models/referee/referee';
import { Report, UpdateEvent, UpdateReport } from 'src/app/models/report/report';
import { RefereeService } from 'src/app/services/referee.service';
import { ReportService } from 'src/app/services/report.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss'],
  providers: [ConfirmationService, MessageService]
})
export class ReportsComponent implements OnInit {
  reports: Report[];
  referee: Referee;
  clonedReports: { [s: string]: Report } = {};
  referees: Referee[];

  visible: boolean;

  constructor(private service: ReportService, private hub: SignalrService, private refereeService: RefereeService) {}

  ngOnInit(): void {
    this.getReports();
    this.getReferees();
    this.hub.addClientMethod('report', 'reportsUpdated', (data) => this.updateTable(data.result));
  }

  protected handleStateEvent(state: boolean) {
    this.visible = state;
  }

  protected getAvatar(name: string): string {
    return this.referees.find(obj => obj.fullName === name).image;
  }

  protected applyFilter(table: Table, event: Event) {
    return table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  protected createReport() {
    this.visible = true;
  }

  private updateTable(data: Report[]) {
    let tempReports: Report[] = [];
    
    data.forEach(obj => {
      tempReports.push(obj);
    })
    
    this.reports = tempReports;
  }

  private async getReferees() {
    return (await this.refereeService.getReferees()).subscribe((res) => {
      this.referees = res;
    })
  }

  private async getReports() {
    return (await this.service.getReports()).subscribe((res) => {
      this.reports = res;
    })
  }
}
