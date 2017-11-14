import { Inject, Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Person } from "../models/Person";
import "rxjs/add/operator/map";
import "rxjs/add/operator/toPromise";

@Injectable()
export class PeopleService {
    private readonly baseUrl: string;
    
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = `${baseUrl}api/People`;
    }

    search(page: number, pageSize: number, searchTerm?: string): Promise<ISearchResults> {
        return this.http.get(`${this.baseUrl}?page=${page}&pageSize=${pageSize}&search=${encodeURIComponent(searchTerm || '')}`)
            .map(r => r.json() as ISearchResults)
            .toPromise<ISearchResults>();
    }

    create(person: Person): Promise<Person> {
        return this.http.post(this.baseUrl, person)
            .map(r => r.json())
            .toPromise<Person>();
    }

    get(id: number): Promise<Person> {
        return this.http.get(`${this.baseUrl}/${id}`)
            .map(r => r.json())
            .toPromise<Person>();
    }

    update(person: Person): Promise<void> {
        return this.http.put(`${this.baseUrl}/${person.id}`, person)
            .map(r => {})
            .toPromise<void>();
    }

    delete(id: number): Promise<void> {
        return this.http.delete(`${this.baseUrl}/${id}`)
            .map(r => {})
            .toPromise<void>();
    }
}

export interface ISearchResults {
    page: number;
    pageSize: number;
    totalRecords: number;
    results: Person[];
}