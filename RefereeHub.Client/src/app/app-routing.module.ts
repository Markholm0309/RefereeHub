import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './guards/auth-guard.guard';
import { RefereesComponent } from './components/referees/referees.component';
import { RefereeDetailsComponent } from './components/referees/referee-details/referee-details.component';
import { RefereeDetailsResolver } from './resolvers/referee-details.resolver';
import { ReportsComponent } from './components/reports/reports.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'reports', component: ReportsComponent },
  { path: 'referees', component: RefereesComponent },
  { path: 'referees/:username', component: RefereeDetailsComponent, resolve: { referee: RefereeDetailsResolver } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
