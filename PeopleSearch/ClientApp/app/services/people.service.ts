import { Inject, Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Person } from "../models/Person";
import { BusyTracker } from './busy.service';
import "rxjs/add/operator/map";
import "rxjs/add/operator/toPromise";

@Injectable()
export class PeopleService {
    private readonly baseUrl: string;
    
    constructor(
        private http: Http, 
        @Inject('BASE_URL') baseUrl: string,
        private busyTracker: BusyTracker    
    ) {
        this.baseUrl = `${baseUrl}api/People`;
    }

    search(page: number, pageSize: number, searchTerm?: string): Promise<ISearchResults> {
        const promise = this.http.get(`${this.baseUrl}?page=${page}&pageSize=${pageSize}&search=${encodeURIComponent(searchTerm || '')}`)
            .map(r => r.json() as ISearchResults)
            .toPromise<ISearchResults>();
        this.busyTracker.track(promise);
        return promise;
    }

    create(person: Person): Promise<Person> {
        const promise = this.http.post(this.baseUrl, person)
            .map(r => r.json())
            .toPromise<Person>();
        this.busyTracker.track(promise);
        return promise;
    }

    get(id: number): Promise<Person> {
        const promise = this.http.get(`${this.baseUrl}/${id}`)
            .map(r => r.json())
            .toPromise<Person>();
        this.busyTracker.track(promise);
        return promise;
    }

    update(person: Person): Promise<void> {
        const promise = this.http.put(`${this.baseUrl}/${person.id}`, person)
            .map(r => {})
            .toPromise<void>();
        this.busyTracker.track(promise);
        return promise;
    }

    delete(id: number): Promise<void> {
        const promise = this.http.delete(`${this.baseUrl}/${id}`)
            .map(r => {})
            .toPromise<void>();
        this.busyTracker.track(promise);
        return promise;
    }
}

export interface ISearchResults {
    page: number;
    pageSize: number;
    totalRecords: number;
    results: Person[];
}