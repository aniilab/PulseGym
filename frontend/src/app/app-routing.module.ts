import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActivityListComponent } from './components/activities/activity-list/activity-list.component';
import { ContactComponent } from './components/contact/contact.component';
import { TrainerListComponent } from './components/trainers/trainer-list/trainer-list.component';
import {
  canActivateIsAdminGuard,
  canActivateLoggedInGuard,
} from './guards/auth-guard.service';
import { ActivityFormComponent } from './components/activities/activity-form/activity-form.component';
import { ClientListComponent } from './components/clients/client-list/client-list.component';
import { ProgramListComponent } from './components/programs/program-list/program-list.component';
import { WorkoutRequestListComponent } from './components/workout-requests/workout-request-list/workout-request-list.component';
import { canDeactivateGuard } from './guards/deactivate-guard.service';
import { ClientFormComponent } from './components/clients/client-form/client-form.component';
import { ClientDetailComponent } from './components/clients/client-detail/client-detail.component';
import { ClientInfoComponent } from './components/clients/client-detail/client-info/client-info.component';
import { ClientProgramsComponent } from './components/clients/client-detail/client-programs/client-programs.component';
import { ClientWorkoutsComponent } from './components/clients/client-detail/client-workouts/client-workouts.component';
import { TrainerFormComponent } from './components/trainers/trainer-form/trainer-form.component';

const routes: Routes = [
  { path: '', redirectTo: 'activities', pathMatch: 'full' },
  { path: 'activities', component: ActivityListComponent, pathMatch: 'full' },
  {
    path: 'activities/create',
    component: ActivityFormComponent,
    pathMatch: 'full',
    canActivate: [canActivateIsAdminGuard],
    canDeactivate: [canDeactivateGuard],
  },
  {
    path: 'activities/edit/:id',
    component: ActivityFormComponent,
    pathMatch: 'full',
    canActivate: [canActivateIsAdminGuard],
    canDeactivate: [canDeactivateGuard],
  },
  { path: 'contact', component: ContactComponent },
  {
    path: 'trainers',
    component: TrainerListComponent,
    canActivate: [canActivateLoggedInGuard],
    pathMatch: 'full',
  },
  {
    path: 'trainers/create',
    component: TrainerFormComponent,
    pathMatch: 'full',
    canActivate: [canActivateIsAdminGuard],
    canDeactivate: [canDeactivateGuard],
  },
  {
    path: 'clients',
    component: ClientListComponent,
    canActivate: [canActivateLoggedInGuard],
    pathMatch: 'full',
  },
  {
    path: 'clients/create',
    component: ClientFormComponent,
    pathMatch: 'full',
    canActivate: [canActivateIsAdminGuard],
    canDeactivate: [canDeactivateGuard],
  },
  {
    path: 'clients/:id',
    component: ClientDetailComponent,
    canActivate: [canActivateLoggedInGuard],
    children: [
      { path: '', redirectTo: 'info', pathMatch: 'full' },
      {
        path: 'programs', 
        component: ClientProgramsComponent, 
        canActivate: [canActivateLoggedInGuard],
      },
      {
        path: 'info', 
        component: ClientInfoComponent, 
        canActivate: [canActivateLoggedInGuard],
      },
      {
        path: 'workouts', 
        component: ClientWorkoutsComponent, 
        canActivate: [canActivateLoggedInGuard],
      },
    ]
  },
  { path: 'programs', component: ProgramListComponent, pathMatch: 'full' },
  {
    path: 'requests',
    component: WorkoutRequestListComponent,
    canActivate: [canActivateLoggedInGuard],
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
