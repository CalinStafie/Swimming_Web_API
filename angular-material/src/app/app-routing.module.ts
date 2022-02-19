import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';
import { PurchasesComponent } from './purchases/purchases.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ReceptionistsProfileComponent } from './receptionists-profile/receptionists-profile.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { MakePurchaseComponent } from './make-purchase/make-purchase.component';
import { ClientPageProfileComponent } from './client-page-profile/client-page-profile.component';
import { SubscriptionsComponent } from './subscriptions/subscriptions/subscriptions.component';
import { RegisterComponent } from './register/register.component';
import { ReceptionistPageComponent } from './receptionist-page/receptionist-page.component'

const routes: Routes = [
  { 
    path: '',
    redirectTo: '/home',
    pathMatch: 'full' 
  },
  { 
    path: 'home', 
    component: HomepageComponent 
  },
  { 
    path: 'receptionists', 
    component: ReceptionistsProfileComponent 
  },
  { 
    path: 'purchases',
    component: PurchasesComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ["Admin"]}

  },
  { 
    path: 'dashboard', 
    component: DashboardComponent, 
    canActivate: [AuthGuard],
  },
  { 
    path: 'login', 
    component: LoginComponent 
  },
  { 
    path: 'register', 
    component: RegisterComponent 
  },
  { 
    path: 'subscriptions', 
    component: SubscriptionsComponent },
  {
    path: 'profile/:id',
    component: ClientPageProfileComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ["Client"]}  
  },
  {
    path: 'receptionist-page/:id',
    component: ReceptionistPageComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ["Receptionist"]}  
  },
  { 
    path: 'make-purchase/:id', 
    component: MakePurchaseComponent, 
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ["Client"]}
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
