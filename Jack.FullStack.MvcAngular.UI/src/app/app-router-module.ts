import { AdminViewComponent } from './admin-view/admin-view.component';
import { UserRouterGuard, AdminRouterGuard } from './auth/auth-guard';
import { UserViewComponent } from './user-view/user-view.component';
import { DashboardViewComponent } from './dashboard-view/dashboard-view.component';
import { AppComponent } from './app.component';
import { Routes, RouterModule } from "@angular/router";
import { UserLoginComponent } from './user-login/user-login.component';
import { UserRegisterComponent } from './user-register/user-register.component';

const appRoutes: Routes = [
    {
        path: '',
        component: DashboardViewComponent,
        canActivate: []
    },
    {
        path:'user',
        component: UserViewComponent,
        canActivate:[UserRouterGuard]
    },
    {
        path: 'admin',
        component: AdminViewComponent,
        canActivate:[AdminRouterGuard]
    },
    {
        path: 'login',
        component: UserLoginComponent
    },
    {
        path: 'register',
        component: UserRegisterComponent
    },
    {
        path: '**',
        component: DashboardViewComponent
    }
];

export const AppRouterModule = RouterModule.forRoot(appRoutes);