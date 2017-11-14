import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PeopleComponent } from './components/people/people.component';
import { PersonComponent } from './components/person/person.component';
import { PeopleService } from './services/people.service';
import { PlatformService } from './services/platform.service';
import { BusyTracker } from './services/busy.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        PersonComponent,
        PeopleComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'people', pathMatch: 'full' },
            { path: 'people/:id', component: PersonComponent },
            { path: 'people', component: PeopleComponent },
            { path: '**', redirectTo: 'people' }
        ])
    ],
    providers: [
        PeopleService,
        PlatformService,
        { provide: BusyTracker, useValue: new BusyTracker() } //singleton for global busy tracking
    ]
})
export class AppModuleShared {
}
