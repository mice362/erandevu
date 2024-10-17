import { Routes } from '@angular/router';
import { DashboardComponent } from '../../features/panels/admin/components/charts/dashboard/dashboard.component';
import { AdminUpdateDrscheduleComponent } from '../../features/panels/admin/components/admin-update-drschedule/admin-update-drschedule.component';
import { AdminListDrscheduleComponent } from '../../features/panels/admin/components/admin-list-drschedule/admin-list-drschedule.component';
import { UpdateAppointmentComponent } from '../../features/panels/admin/components/update-appointment/update-appointment.component';
import { AddAppointmentComponent } from '../../features/panels/admin/components/add-appointment/add-appointment.component';
import { UpcomingAppointmentsComponent } from '../../features/panels/admin/components/list-appointment/upcoming-appointments/upcoming-appointments.component';
import { PastAppointmentsComponent } from '../../features/panels/admin/components/list-appointment/past-appointments/past-appointments.component';
import { UpdatePatientComponent } from '../../features/panels/admin/components/update-patient/update-patient.component';
import { AddPatientComponent } from '../../features/panels/admin/components/add-Patient/add-Patient.component';
import { ListPatientComponent } from '../../features/panels/admin/components/list-patient/list-Patient.component';
import { UpdateFeedbackComponent } from '../../features/panels/admin/components/update-feedback/update-feedback.component';
import { ListFeedbackComponent } from '../../features/panels/admin/components/list-feedback/list-feedback.component';
import { AdminAddReportComponent } from '../../features/panels/admin/components/admin-add-report/admin-add-report.component';
import { AdminEditReportComponent } from '../../features/panels/admin/components/admin-edit-report/admin-edit-report.component';
import { AdminListReportComponent } from '../../features/panels/admin/components/admin-list-report/admin-list-report.component';
import { AddDoctorComponent } from '../../features/panels/admin/components/add-doctor/add-doctor.component';
import { UpdateDoctorComponent } from '../../features/panels/admin/components/update-doctor/update-doctor.component';
import { AdminProfileComponent } from '../../features/panels/admin/components/admin-profile/admin-profile.component';
import { UpdateBranchComponent } from '../../features/panels/admin/components/update-branch/update-branch.component';
import { AddBranchComponent } from '../../features/panels/admin/components/add-branch/add-branch.component';
import { ListBranchComponent } from '../../features/panels/admin/components/list-branch/list-branch.component';
import { ListDoctorComponent } from '../../features/panels/admin/components/list-doctor/list-doctor.component';
import { AdminSidebarComponent } from '../../features/panels/admin/components/sidebar/adminSidebar.component';

export const adminRoutes: Routes = [

  {
    path: 'admin-sidebar',
    component: AdminSidebarComponent,
  },
  {
    path: 'admin-list-doctor',
    component: ListDoctorComponent,
  },
  {
    path: 'admin-branches',
    component: ListBranchComponent,
  },
  {
    path: 'admin-add-branch',
    component: AddBranchComponent,
  },
  {
    path: 'admin-update-branch/:branchId',
    component: UpdateBranchComponent,
  },
  {
    path: 'admin-profile',
    component: AdminProfileComponent,
  },
  {
    path: 'admin-update-doctor/:doctorId',
    component: UpdateDoctorComponent,
  },
  {
    path: 'admin-add-doctor',
    component: AddDoctorComponent,
  },
  {
    path: 'admin-reports',
    component: AdminListReportComponent,
  },
  {
    path: 'admin-report-detail/:id',
    component: AdminEditReportComponent,
  },
  {
    path: 'admin-add-report/:appointmentId',
    component: AdminAddReportComponent,
  },
  {
    path: 'admin-list-feedback',
    component: ListFeedbackComponent,
  },
  {
    path: 'admin-update-feedback/:feedbackId',
    component: UpdateFeedbackComponent,
  },
  {
    path: 'admin-patient',
    component: ListPatientComponent,
  },
  {
    path: 'admin-add-patient',
    component: AddPatientComponent,
  },
  {
    path: 'admin-update-patient/:patientId',
    component: UpdatePatientComponent,
  },
  {
    path: 'past-appointments',
    component: PastAppointmentsComponent,
  },
  {
    path: 'upcoming-appointments',
    component: UpcomingAppointmentsComponent,
  },
  {
    path: 'add-appointment',
    component: AddAppointmentComponent,
  },
  {
    path: 'update-appointment/:id',
    component: UpdateAppointmentComponent,
  },
  {
    path: 'admin-doctor-schedule/:doctorId',
    component: AdminListDrscheduleComponent,
  },
  {
    path: 'admin-edit-doctor-schedule/:doctorId/:scheduleId',
    component: AdminUpdateDrscheduleComponent,
  },
  {
    path: 'admin-charts',
    component: DashboardComponent,
  },
];
