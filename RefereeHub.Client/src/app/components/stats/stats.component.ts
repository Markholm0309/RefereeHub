import { Component, OnInit } from '@angular/core';
import { RefereeService } from 'src/app/services/referee.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {
  averageRating: number;
  registeredReferees: number;
  totalOfReports: number;

  constructor(private service: RefereeService, private reportService: ReportService) {}

  ngOnInit(): void {
    this.getReferees();
    this.GetReports();
  }

  private findAverage(numbers: number[]): number {
    const sum = numbers.reduce((acc, val) => acc + val, 0);
    const average = sum / numbers.length;
    return Math.ceil(average);
  }

  private async getReferees() {
    return (await this.service.getReferees()).subscribe((res) => {
      let ratings: number[] = [];
      res.filter(referee => ratings.push(referee.rating));

      this.registeredReferees = res.length;
      this.averageRating = this.findAverage(ratings);

      return this.findAverage(ratings);
    })
  }

  private async GetReports() {
    return (await this.reportService.getReports()).subscribe((res) => {
      this.totalOfReports = res.length;
    })
  }
}
