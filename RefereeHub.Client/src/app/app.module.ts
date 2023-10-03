import { APP_INITIALIZER, NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './components/nav/nav.component';
import { HomeComponent } from './components/home/home.component';
import { SignInComponent } from './components/sign-in/sign-in.component';

import { SidebarModule } from 'primeng/sidebar';
import { RatingModule } from 'primeng/rating';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { InputNumberModule } from 'primeng/inputnumber';
import { PasswordModule } from 'primeng/password';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { ToastModule } from 'primeng/toast';
import { RefereesComponent } from './components/referees/referees.component';
import { RefereeDetailsComponent } from './components/referees/referee-details/referee-details.component';
import { StatsComponent } from './components/stats/stats.component';
import { EventDialogComponent } from './components/event-dialog/event-dialog.component';
import { RefereeFormComponent } from './components/referees/referee-form/referee-form.component';
import { ReportsComponent } from './components/reports/reports.component';
import { EventFormComponent } from './components/event-dialog/event-form/event-form.component';
import { ReportFormComponent } from './components/reports/report-form/report-form.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { SignalrService } from './services/signalr.service';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    SignInComponent,
    RefereesComponent,
    RefereeDetailsComponent,
    StatsComponent,
    EventDialogComponent,
    RefereeFormComponent,
    ReportsComponent,
    EventFormComponent,
    ReportFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    SidebarModule,
    RatingModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule,
    InputNumberModule,
    PasswordModule,
    CardModule,
    TableModule,
    DialogModule,
    DropdownModule,
    ConfirmPopupModule,
    ToastModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: !isDevMode(),
      // Register the ServiceWorker as soon as the application is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    })
  ],
  providers: [
    SignalrService, {
      provide: APP_INITIALIZER,
      useFactory: (SignalrService: SignalrService) => () => { 
        SignalrService.startReportConnection(), 
        SignalrService.startRefereeConnection()
      },
      deps: [SignalrService],
      multi: true,    
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
