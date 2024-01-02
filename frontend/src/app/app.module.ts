import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { AuthService } from './services/auth.service';
import { CarouselComponent } from './components/main-page/carousel/carousel.component';
import { SideNavbarComponent } from './components/shared/side-navbar/side-navbar.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { ActivityListComponent } from './components/activities/activity-list/activity-list.component';
import { ActivityComponent } from './components/activities/activity-list/activity/activity.component';
import { ContactComponent } from './components/contact/contact.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TrainerListComponent } from './components/trainers/trainer-list/trainer-list.component';
import { TrainerComponent } from './components/trainers/trainer-list/trainer/trainer.component';
import { ActivityFormComponent } from './components/activities/activity-form/activity-form.component';
import { HttpClientModule } from '@angular/common/http';
import { ClientListComponent } from './components/clients/client-list/client-list.component';
import { ClientComponent } from './components/clients/client-list/client/client.component';
import { TrainerService } from './services/trainer.service';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { LoginDialogComponent } from './components/dialogs/login-dialog/login-dialog.component';
import { ProgramListComponent } from './components/programs/program-list/program-list.component';
import { ProgramComponent } from './components/programs/program-list/program/program.component';
import { ProgramService } from './services/program.service';
import { ProgramCreateComponent } from './components/programs/program-create/program-create.component';
import { WorkoutRequestService } from './services/workout-request.service';
import { WorkoutRequestListComponent } from './components/workout-requests/workout-request-list/workout-request-list.component';
import { WorkoutRequestComponent } from './components/workout-requests/workout-request/workout-request.component';
import { ClientService } from './services/client.service';
import { authInterceptorProviders } from './interceptors/authorization.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmationDialogComponent } from './components/dialogs/confirmation-dialog/confirmation-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CarouselComponent,
    SideNavbarComponent,
    FooterComponent,
    ActivityListComponent,
    ActivityComponent,
    ContactComponent,
    TrainerListComponent,
    TrainerComponent,
    ActivityFormComponent,
    ClientListComponent,
    ClientComponent,
    LoginDialogComponent,
    ProgramListComponent,
    ProgramComponent,
    ProgramCreateComponent,
    WorkoutRequestListComponent,
    WorkoutRequestComponent,
    ConfirmationDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatToolbarModule,
    MatMenuModule,
    MatNativeDateModule,
    MatDatepickerModule,
    DragDropModule,
    BrowserAnimationsModule,
  ],
  providers: [
    AuthService,
    TrainerService,
    ProgramService,
    WorkoutRequestService,
    ClientService,
    authInterceptorProviders,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
