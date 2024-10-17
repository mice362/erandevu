import { Routes } from '@angular/router';
import { PatientSidebarComponent } from '../../features/panels/patient/components/sidebar/psidebar.component';
import { PatientListPastAppointmentComponent } from '../../features/panels/patient/components/patient-list-past-appointment/patient-list-past-appointment.component';
import { PatientListUpcomingAppointmentComponent } from '../../features/panels/patient/components/patient-list-upcoming-appointment/patient-list-upcoming-appointment.component';
import { PatientProfileComponent } from '../../features/panels/patient/components/Patient-profile/Patient-profile.component';
import { PatientListReportComponent } from '../../features/panels/patient/components/patient-list-report/patient-list-report.component';
import { PatientReportDetailComponent } from '../../features/panels/patient/components/patient-report-detail/patient-report-detail.component';
import { PatientListFeedbackComponent } from '../../features/panels/patient/components/patient-list-feedback/patient-list-feedback.component';
import { PatientAddFeedbackComponent } from '../../features/panels/patient/components/patient-add-feedback/patient-add-feedback.component';
import { PatientUpdateFeedbackComponent } from '../../features/panels/patient/components/patient-update-feedback/patient-update-feedback.component';
import { CreateAppointmentComponent } from '../../features/panels/patient/components/create-appointment/create-appointment.component';
import { PatientSummaryComponent } from '../../features/panels/patient/components/summary/patient-summary/patient-summary.component';

export const patientRoutes: Routes = [
    
  {
    path: 'patient-sidebar',
    component: PatientSidebarComponent,
  },
  {
    path: 'patient-past-appointments',
    component: PatientListPastAppointmentComponent,
  },
  {
    path: 'patient-upcoming-appointments',
    component: PatientListUpcomingAppointmentComponent,
  },
  {
    path: 'patient-profile',
    component: PatientProfileComponent,
  },
  {
    path: 'patient-reports',
    component: PatientListReportComponent,
  },
  {
    path: 'patient-report-detail/:id',
    component: PatientReportDetailComponent,
  },
  {
    path: 'patient-feedbacks',
    component: PatientListFeedbackComponent,
  },
  {
    path: 'patient-add-feedback',
    component: PatientAddFeedbackComponent,
  },
  {
    path: 'patient-update-feedback/:feedbackId',
    component: PatientUpdateFeedbackComponent,
  },
  {
    path: 'create-appointment',
    component: CreateAppointmentComponent,
  },
  {
    path: 'patient-summary',
    component: PatientSummaryComponent,
  },
];
