import { Component, OnInit } from '@angular/core';
import { Table } from 'primeng/table';
import { Referee } from 'src/app/models/referee/referee';
import { RefereeService } from 'src/app/services/referee.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-referees',
  templateUrl: './referees.component.html',
  styleUrls: ['./referees.component.scss']
})
export class RefereesComponent implements OnInit {
  referees: Referee[];
  visible: boolean;

  constructor(private service: RefereeService, private hub: SignalrService) {}

  ngOnInit(): void {
    this.getReferees();
    this.hub.addClientMethod('referee', 'refereesUpdated', (data) => this.updateTable(data.result));
  }

  protected handleStateEvent(state: boolean) {
    this.visible = state;
  }

  protected applyFilter(table: Table, event: Event) {
    return table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  protected createReferee() {
    this.visible = true;
  }

  private updateTable(data: Referee[]) {
    let tempReferees: Referee[] = [];
    
    data.forEach(obj => {
      tempReferees.push(obj);
    })
    
    this.referees = tempReferees;
  }

  private async getReferees() {
    return (await this.service.getReferees()).subscribe((res) => {
      this.updateTable(res);
    })
  }
}
