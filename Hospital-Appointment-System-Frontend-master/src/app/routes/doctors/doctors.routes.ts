import { Routes } from '@angular/router';
import { DoctorSidebarComponent } from '../../features/panels/doctor/components/sidebar/doctorSidebar.component';
import { DoctorProfileComponent } from '../../features/panels/doctor/components/doctor-profile/doctor-profile.component';
import { AppointmentHistoryComponent } from '../../features/panels/doctor/components/appointment-history/appointment-history.component';
import { PendingAppointmentComponent } from '../../features/panels/doctor/components/pending-appointment/pending-appointment.component';
import { UpdateDoctorScheduleComponent } from '../../features/panels/doctor/components/update-doctor-schedule/update-doctor-schedule.component';
import { CreateDoctorScheduleComponent } from '../../features/panels/doctor/components/create-doctor-schedule/create-doctor-schedule.component';
import { ListDoctorScheduleComponent } from '../../features/panels/doctor/components/list-doctor-schedule/list-doctor-schedule.component';
import { ListReportComponent } from '../../features/panels/doctor/components/list-report/list-report.component';
import { EditReportComponent } from '../../features/panels/doctor/components/edit-report/edit-report.component';
import { AddReportComponent } from '../../features/panels/doctor/components/add-report/add-report.component';
import { DoctorListFeedbackComponent } from '../../features/panels/doctor/components/doctor-list-feedback/doctor-list-feedback.component';
import { DoctorAddFeedbackComponent } from '../../features/panels/doctor/components/doctor-add-feedback/doctor-add-feedback.component';
import { DoctorUpdateFeedbackComponent } from '../../features/panels/doctor/components/doctor-update-feedback/doctor-update-feedback.component';
import { SummaryComponent } from '../../features/panels/doctor/components/summary/summary/summary.component';
import { DoctorSidebarPatientComponent } from '../../features/panels/doctor/components/doctorSidebar-Patient/doctorSidebar-Patient.component';

export const doctorRoutes: Routes = [
  {
    path: 'doctor-sidebar',
    component: DoctorSidebarComponent,
  },
  {
    path: 'doctor-profile',
    component: DoctorProfileComponent,
  },
  {
    path: 'appointment-history',
    component: AppointmentHistoryComponent,
  },
  {
    path: 'pending-appointments',
    component: PendingAppointmentComponent,
  },
  {
    path: 'doctorschedule/:scheduleId',
    component: UpdateDoctorScheduleComponent,
  },
  {
    path: 'doctor-schedule',
    component: CreateDoctorScheduleComponent, 
  },
  {
    path: 'list-doctor-schedule', 
    component: ListDoctorScheduleComponent,
  }, 
  {
    path: 'doctor-reports',
    component: ListReportComponent, 
  },
  {
    path: 'report-detail/:id',
    component: EditReportComponent,
  },

  {
    path: 'add-report/:appointmentId',
    component: AddReportComponent,
  },
  {
    path: 'doctor-feedbacks',
    component: DoctorListFeedbackComponent,
  },
  {
    path: 'doctor-add-feedback',
    component: DoctorAddFeedbackComponent,
  },
  {
    path: 'doctor-update-feedback/:feedbackId',
    component: DoctorUpdateFeedbackComponent,
  },
  {
    path:"doctor-summary",
    component:SummaryComponent
  },
  {
    path: 'doctor-patient',
    component: DoctorSidebarPatientComponent,
  },

];
