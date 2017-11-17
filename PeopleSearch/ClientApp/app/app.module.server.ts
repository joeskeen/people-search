import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ServerModule } from '@angular/platform-server';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app.component';
import { ServerStateTransferModule, StateTransferService } from '@ngx-universal/state-transfer';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule.withServerTransition({
            appId: 'people-search-app'
        }),
        ServerModule,
        ServerStateTransferModule.forRoot(),
        AppModuleShared
    ]
})
export class AppModule {
    constructor(private readonly stateTransfer: StateTransferService) {
    }
    
    ngOnBootstrap = () => {
        this.stateTransfer.set('test_key', JSON.stringify({'value': 'test'}));
        this.stateTransfer.inject();
    };
}
