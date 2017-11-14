import { Component, Inject, OnInit, OnChanges } from '@angular/core';
import { FormControl } from '@angular/forms';
import { PeopleService } from '../../services/people.service';
import { Person } from '../../models/person';
import 'rxjs/add/operator/debounceTime';

@Component({
    selector: 'people',
    templateUrl: './people.component.html',
    styleUrls: [
        './people.component.css'
    ]
})
export class PeopleComponent implements OnInit {
    people: Person[];
    page = 0;
    pageSize = 24;
    totalPages = 0;
    totalRecords = 0;
    searchControl = new FormControl();
    search = '';

    constructor(private peopleService: PeopleService) {
    }

    async ngOnInit() {
        this.searchControl.valueChanges
            .debounceTime(500)
            .subscribe(term => {
                this.search = term;
                this.page = 0;
                this.loadPeople();
            });
        await this.loadPeople();
    }

    nextPage() {
        this.page++;
        this.loadPeople();
    }
    previousPage() {
        if (this.page === 0)
            return;

        this.page--;
        this.loadPeople();
    }

    async loadPeople() {
        const results = await this.peopleService.search(this.page, this.pageSize, this.search);
        this.people = results.results;
        this.totalRecords = results.totalRecords;
        this.totalPages = Math.ceil(results.totalRecords / this.pageSize);
    }
}