
export class CreateReferee {
    fullName: string;
    age: number;
    currentLeague: string;

    constructor(fullName: string, age: number, currentLeague: string) {
        this.fullName = fullName,
        this.age = age,
        this.currentLeague = currentLeague
    }
}

export class UpdateReferee {
    id: number;
    fullName: string;
    age: number;
    currentLeague: string;

    constructor(id: number, fullName: string, age: number, currentLeague: string) {
        this.id = id;
        this.fullName = fullName,
        this.age = age,
        this.currentLeague = currentLeague
    }
}


export class RefereeFormModel {
    id: number;
    fullName: string;
    age: number;
    currentLeague: string;
}