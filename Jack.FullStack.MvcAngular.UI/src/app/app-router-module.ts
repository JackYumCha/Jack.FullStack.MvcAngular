import { AppComponent } from './app.component';
import { Routes, RouterModule } from "@angular/router";

const appRoutes: Routes = [
    {
        path: '',
        component: AppComponent
    }
];

export const AppRouterModule = RouterModule.forRoot(appRoutes);