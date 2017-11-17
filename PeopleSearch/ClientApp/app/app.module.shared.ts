import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpTransferModule } from '@ngx-universal/state-transfer';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
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
        HttpTransferModule.forRoot(),
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', component: PeopleComponent, pathMatch: 'full' },
            { path: 'people/:id', component: PersonComponent },
            { path: '**', redirectTo: '' }
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
