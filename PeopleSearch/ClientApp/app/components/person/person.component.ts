import { Component, Inject, OnInit, SecurityContext } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { PeopleService } from '../../services/people.service';
import { PlatformService } from '../../services/platform.service';
import { Person } from '../../models/person';

@Component({
    selector: 'person',
    templateUrl: './person.component.html',
    styleUrls: ['./person.component.css']
})
export class PersonComponent implements OnInit {
    person: Person;
    stalkerModeEnabled = false;

    constructor(
        private peopleService: PeopleService,
        private userAgent: PlatformService,
        private route: ActivatedRoute,
        private sanitizer: DomSanitizer) {
    }

    get data(): { key: string, value: any }[] {
        return Object.keys(this.person || {})
            .filter(k => ['givenName', 'surname', 'avatarUrl'].indexOf(k) === -1)
            .map(k => ({ key: k, value: this.person[k] }));
    }

    async ngOnInit() {
        console.log('onInit');
        this.route.params
            .subscribe(async (p: any) => {
                console.log('params', p);
                this.person = await this.peopleService.get(p.id);
            });
    }

    get mapUrl() {
        if (!this.person)
            return null;

        const safeName = this.sanitizer.sanitize(SecurityContext.URL, `${this.person.givenName} ${this.person.surname}`) || '';
        const encodedName = encodeURIComponent(safeName);
        const url = `https://www.bing.com/maps/embed/viewer.aspx?v=3&cp=${this.person.latitude}~${this.person.longitude}&lvl=18&w=500&h=400&sty=h&typ=d&pp=${encodedName}~~${this.person.latitude}~${this.person.longitude}&ps=&dir=0&mkt=en-us&src=O365&form=BMEMJS`;
        const trusted = this.sanitizer.bypassSecurityTrustResourceUrl(url);
        return trusted;
    }

    get browser() {
        return this.person
            ? this.userAgent.browser(this.person.browserUserAgent) || ''
            : '';
    }
    get device() {
        return this.person
            ? this.userAgent.device(this.person.browserUserAgent) || ''
            : '';
    }
    get operatingSystem() {
        return this.person
            ? this.userAgent.os(this.person.browserUserAgent) || ''
            : '';
    }
}