import { Referee } from "../referee/referee";

export interface Report {
    id: number;
    title: string;
    timeCreated: Date;
    referee: Referee;
    events: ReportEvent[];
    rating: number;
}

export class ReportEvent {
    id: number;
    description: string;
    timeStamp: string;

    constructor();
    constructor(description: string, timeStamp: string);
    constructor(description?: string, timeStamp?: string) {
        this.description = description;
        this.timeStamp = timeStamp
    }
}

export class CreateReportEvent {
    description: string;
    timeStamp: string;
    reportId: number;

    constructor(description: string, timeStamp: string, reportId: number) {
        this.description = description;
        this.timeStamp = timeStamp;
        this.reportId = reportId;
    }
}

/* Create */

export class CreateReport {
    title: string;
    referee: string;
    events: ReportEvent[];
    rating: number;
}

export class CreateReportRequest {
    title: string;
    refereeId: number;
    events: ReportEvent[];
    rating: number;

    constructor(title: string, refereeId: number, events: ReportEvent[], rating: number) {
        this.title = title;
        this.refereeId = refereeId;
        this.events = events;
        this.rating = rating;
    }
}

/* Update */

export interface UpdateEvent {
    id: number;
    description: string;
    timeStamp: string;
    reportId: number;
}

export interface UpdateReport {
    id: number;
    title: string;
    refereeId: number;
    events: UpdateEvent[];
    rating: number;
}