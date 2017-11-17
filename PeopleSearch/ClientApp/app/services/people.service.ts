import { Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { isPlatformServer } from '@angular/common';
import { HttpTransferService, StateTransferService } from "@ngx-universal/state-transfer";
import { Person } from "../models/Person";
import { BusyTracker } from './busy.service';
import "rxjs/add/operator/map";
import "rxjs/add/operator/do";
import "rxjs/add/operator/toPromise";

@Injectable()
export class PeopleService {
    private readonly baseUrl: string;
    
    constructor(
        private http: HttpTransferService, 
        @Inject('BASE_URL') baseUrl: string,
        private busyTracker: BusyTracker,
        @Inject(PLATFORM_ID) private platformId: object,
        private stateTransferService: StateTransferService
    ) {
        this.baseUrl = `${baseUrl}api/People`;
    }

    search(page: number, pageSize: number, searchTerm?: string): Promise<ISearchResults> {
        const promise = this.http.get(`${this.baseUrl}?page=${page}&pageSize=${pageSize}&search=${encodeURIComponent(searchTerm || '')}`)
            .do(() => this.cache())
            .toPromise<ISearchResults>();
        this.busyTracker.track(promise);
        return promise;
    }

    create(person: Person): Promise<Person> {
        const promise = this.http.post(this.baseUrl, person)
            .do(() => this.cache())
            .toPromise<Person>();
        this.busyTracker.track(promise);
        return promise;
    }

    get(id: number): Promise<Person> {
        const promise = this.http.get(`${this.baseUrl}/${id}`)
            .do(() => this.cache())
            .toPromise<Person>();
        this.busyTracker.track(promise);
        return promise;
    }

    update(person: Person): Promise<void> {
        const promise = this.http.put(`${this.baseUrl}/${person.id}`, person)
            .toPromise<void>();
        this.busyTracker.track(promise);
        return promise;
    }

    delete(id: number): Promise<void> {
        const promise = this.http.delete(`${this.baseUrl}/${id}`)
            .toPromise<void>();
        this.busyTracker.track(promise);
        return promise;
    }

    private cache() {
        if (isPlatformServer(this.platformId)) {
            this.stateTransferService.inject();
        }
    }
}

export interface ISearchResults {
    page: number;
    pageSize: number;
    totalRecords: number;
    results: Person[];
}