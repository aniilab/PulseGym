import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
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
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTabsModule } from '@angular/material/tabs';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { LoginDialogComponent } from './components/dialogs/login-dialog/login-dialog.component';
import { ProgramListComponent } from './components/programs/program-list/program-list.component';
import { ProgramComponent } from './components/programs/program-list/program/program.component';
import { ProgramService } from './services/program.service';
import { ProgramCreateComponent } from './components/programs/program-create/program-create.component';
import { WorkoutRequestListComponent } from './components/workout-requests/workout-request-list/workout-request-list.component';
import { ClientService } from './services/client.service';
import { authInterceptorProviders } from './interceptors/authorization.interceptor';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { MatListModule } from '@angular/material/list';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmationDialogComponent } from './components/dialogs/confirmation-dialog/confirmation-dialog.component';
import { ClientFormComponent } from './components/clients/client-form/client-form.component';
import { ClientDetailComponent } from './components/clients/client-detail/client-detail.component';
import { ClientInfoComponent } from './components/clients/client-detail/client-info/client-info.component';
import { ClientProgramsComponent } from './components/clients/client-detail/client-programs/client-programs.component';
import { ClientWorkoutsComponent } from './components/clients/client-detail/client-workouts/client-workouts.component';
import { FilterPipe } from './pipes/filter.pipe';
import { TrainerFormComponent } from './components/trainers/trainer-form/trainer-form.component';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { TableModule } from 'primeng/table';
import { FullCalendarModule } from '@fullcalendar/angular';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { WorkoutCreateComponent } from './components/schedule/workout-create/workout-create.component';
import { AssistantComponent } from './components/assistant/assistant.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

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
    ConfirmationDialogComponent,
    ClientFormComponent,
    ClientDetailComponent,
    ClientInfoComponent,
    ClientProgramsComponent,
    ClientWorkoutsComponent,
    FilterPipe,
    TrainerFormComponent,
    ScheduleComponent,
    WorkoutCreateComponent,
    AssistantComponent,
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
    MatTabsModule,
    DragDropModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatSlideToggleModule,
    FormsModule,
    MatSelectModule,
    MatListModule,
    MatTableModule,
    MatCheckboxModule,
    TableModule,
    BrowserModule,
    FullCalendarModule,
    MatButtonToggleModule,
    MatProgressSpinnerModule,
  ],
  providers: [
    AuthService,
    TrainerService,
    ProgramService,
    ClientService,
    authInterceptorProviders,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
