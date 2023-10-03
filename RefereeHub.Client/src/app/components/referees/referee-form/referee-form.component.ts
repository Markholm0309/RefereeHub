import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RefereeFormModel, UpdateReferee } from 'src/app/models/referee/createReferee';
import { CreateReferee } from 'src/app/models/referee/createReferee';
import { Referee } from 'src/app/models/referee/referee';
import { RefereeService } from 'src/app/services/referee.service';

@Component({
  selector: 'app-referee-form',
  templateUrl: './referee-form.component.html',
  styleUrls: ['./referee-form.component.scss']
})
export class RefereeFormComponent implements OnInit {
  @Input() refereeFromParent: Referee;
  @Input() selectionType: 'create' | 'update';
  @Output() stateEvent = new EventEmitter<boolean>();
  @Output() updateRefereeEvent = new EventEmitter<RefereeFormModel>();

  visible: boolean;
  refereeId: number;
  referee: RefereeFormModel = new RefereeFormModel();
  leagues: string[] = [
    "Serie 1", "Serie 2", "Serie 3", "Serie 4", "Serie 5",
    "Jylland serien", "Danmarks serien",
    "1. Division", "2. Division", "3. Division",
    "Superliga", "Gjensidige Kvindeliga",
    "Kv. 1. Division", "Kv. 2. Division"
  ];
  

  constructor(private refereeService: RefereeService) {}

  ngOnInit(): void {
    if (this.selectionType == 'update') {
      this.getRefereeId(this.refereeFromParent.fullName);
      this.referee.fullName = this.refereeFromParent.fullName;
      this.referee.age = this.refereeFromParent.age;
      this.referee.currentLeague = this.refereeFromParent.currentLeague;
    }

    this.visible = true;
  }

  protected submitForm() {
    if (this.selectionType == 'create') {
      this.createReferee(new CreateReferee(this.referee.fullName, this.referee.age, this.referee.currentLeague));
    } else {
      this.updateReferee(new UpdateReferee(this.refereeId, this.referee.fullName, this.referee.age, this.referee.currentLeague));
      this.updateRefereeEvent.emit(this.referee);
    }

    this.visible = false;
    this.stateEvent.emit(this.visible);
  }

  private async getRefereeId(name: string) {
    return (await this.refereeService.getRefereeId(name)).subscribe((res) => {
      this.refereeId = res;
      return res;
    })
  }

  private async createReferee(body: CreateReferee) {
    return (await this.refereeService.createReferee(body)).subscribe(res => {
      return res;
    });
  }

  private async updateReferee(body: CreateReferee) {
    return (await this.refereeService.updateReferee(body)).subscribe(res => {
      return res;
    });
  }
}
