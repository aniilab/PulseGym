import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActivityListComponent } from './components/activities/activity-list/activity-list.component';
import { ContactComponent } from './components/contact/contact.component';
import { TrainerListComponent } from './components/trainers/trainer-list/trainer-list.component';
import { canActivateIsAdminGuard, canActivateLoggedInGuard } from './services/auth-guard.service';
import { ActivityCreateComponent } from './components/activities/activity-create/create-activity.component';
import { ClientListComponent } from './components/clients/client-list/client-list.component';
import { ProgramListComponent } from './components/programs/program-list/program-list.component';
import { WorkoutRequestListComponent } from './components/workout-requests/workout-request-list/workout-request-list.component';

const routes: Routes = [
  {path:'', redirectTo:'activities', pathMatch:'full'},
  {path:'activities', component:ActivityListComponent, pathMatch:'full'},
  {path:'activities/create', component:ActivityCreateComponent, pathMatch:'full', canActivate:[canActivateIsAdminGuard]},
  {path:'contact', component:ContactComponent},
  {path:'trainers', component:TrainerListComponent, canActivate:[canActivateLoggedInGuard], pathMatch:'full'},
  {path:'clients', component:ClientListComponent, canActivate:[canActivateLoggedInGuard], pathMatch:'full'},
  {path:'programs', component:ProgramListComponent, canActivate:[canActivateLoggedInGuard, canActivateIsAdminGuard], pathMatch:'full'},
  {path:'requests', component:WorkoutRequestListComponent, canActivate:[canActivateLoggedInGuard, canActivateIsAdminGuard], pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
