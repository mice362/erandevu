import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentService } from '../../../../appointments/services/appointment.service';


import { BranchService } from '../../../../branches/services/branch.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { AdminSidebarComponent } from '../../../admin/components/sidebar/adminSidebar.component';
import { Appointment } from '../../../../appointments/models/appointmentModel';
import { Branch } from '../../../../branches/models/branch';

import { Patient } from '../../../patient/models/patientModel';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { PatientService } from '../../../patient/services/patient.service';
import { DoctorService } from '../../../doctor/services/doctor.service';
import { Doctor } from '../../../doctor/models/doctor';

@Component({
  selector: 'app-update-appointment',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AdminSidebarComponent,
    TokenComponent
  ],
  templateUrl: './update-appointment.component.html',
  styleUrls: ['./update-appointment.component.scss'],
})
export class UpdateAppointmentComponent implements OnInit {
  appointmentForm: FormGroup;
  appointment: Appointment;
  errorMessage: string = '';
  branches: Branch[] = [];
  doctors: Doctor[] = [];
  patients: Patient[] = []; // Hastaları listelemek için eklenmiştir

  constructor(
    private formBuilder: FormBuilder,
    private appointmentService: AppointmentService,
    private doctorService: DoctorService,
    private patientService: PatientService,
    private branchService: BranchService,
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadAppointment();
    this.loadBranches();
    this.loadDoctors();
    this.loadPatients();
    this.setMinDateAndTime();
  }

  initForm(): void {
    this.appointmentForm = this.formBuilder.group({
      id: [''],
      date: ['', Validators.required],
      time: ['', Validators.required],
      status: [true, Validators.required],
      patientId: ['', Validators.required],
      patientFirstName: ['', Validators.required],
      patientLastName: ['', Validators.required],
      branchId: ['', Validators.required],
      doctorId: ['', Validators.required],
    });
  }

  loadAppointment(): void {
    this.route.paramMap.subscribe((params) => {
      const appointmentId = +(params.get('id') || '0');
      if (appointmentId) {
        this.appointmentService.getAppointmentById(appointmentId).subscribe(
          (appointment) => {
            this.appointment = appointment;
            if (appointment) {
              this.loadDoctorAndPatientNames(
                appointment.doctorID,
                appointment.patientID
              );
              this.appointmentForm.patchValue({
                id: appointment.id,
                date: appointment.date,
                time: appointment.time,
                status: appointment.status,
                patientId: appointment.patientID,
                branchId: appointment.branchID,
                doctorId: appointment.doctorID,
              });
            }
          }
        );
      }
    });
  }

  loadBranches(): void {
    this.branchService.getBranches(0, 100).subscribe(
      (response) => {
        this.branches = response.items;
      }
    );
  }

  loadDoctors(): void {
    this.doctorService.getDoctors(0, 100).subscribe(
      (response) => {
        this.doctors = response.items;
      }
    );
  }

  loadPatients(): void {
    this.patientService.getPatients(0, 100).subscribe(
      (response) => {
        this.patients = response.items;
      }
    );
  }

  loadDoctorAndPatientNames(doctorId: string, patientId: string): void {
    this.doctorService.getDoctorById(doctorId).subscribe(
      (doctor) => {
        if (doctor) {
          this.appointmentForm.patchValue({
            doctorFirstName: doctor.firstName,
            doctorLastName: doctor.lastName,
          });
        }
      }
    );

    this.patientService.getByPatientId(patientId, 0, 1).subscribe(
      (patient) => {
        if (patient) {
          this.appointmentForm.patchValue({
            patientFirstName: patient.firstName,
            patientLastName: patient.lastName,
          });
        }
      }
    );
  }

  onBranchChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const branchId = Number(selectElement.value);
    this.doctorService.getDoctors(0, 100).subscribe(
      (response) => {
        this.doctors = response.items.filter(
          (doctor :any) => doctor.branchID === branchId
        );
      }
    );
  }

  private formatTime(time: string): string {
    const date = new Date(`1970-01-01T${time}Z`);
    const hours = date.getUTCHours().toString().padStart(2, '0');
    const minutes = date.getUTCMinutes().toString().padStart(2, '0');
    const seconds = date.getUTCSeconds().toString().padStart(2, '0');
    return `${hours}:${minutes}:${seconds}`;
  }

  setMinDateAndTime(): void {
    const today = new Date();
    const minDate = today.toISOString().split('T')[0];
    const hours = today.getHours().toString().padStart(2, '0');
    const minutes = today.getMinutes().toString().padStart(2, '0');
    const minTime = `${hours}:${minutes}`;

    (document.getElementById('date') as HTMLInputElement).setAttribute(
      'min',
      minDate
    );
    (document.getElementById('time') as HTMLInputElement).setAttribute(
      'min',
      minTime
    );
  }

  updateAppointment(): void {
    if (this.appointmentForm.valid) {
      const appointmentData = this.appointmentForm.value;
      const isValid = this.validateDateTime(
        appointmentData.date,
        appointmentData.time
      );

      if (!isValid) {
        this.toastrService.error(
          'Geçmiş tarih veya saate randevu oluşturulamaz.'
        );
        return;
      }

      const updatedAppointment: Appointment = {
        ...appointmentData,
        time: this.formatTime(appointmentData.time),
      };

      this.appointmentService
        .updateAppointment(updatedAppointment.id, updatedAppointment)
        .subscribe(
          () => {
            this.toastrService.success('Randevu başarıyla güncellendi');
            this.router.navigate(['/upcoming-appointments']);
          }
        );
    } else {
      this.toastrService.error('Lütfen eksik alanları doldurun');
    }
  }

  validateDateTime(date: string, time: string): boolean {
    const selectedDate = new Date(`${date}T${time}`);
    const currentDate = new Date();
    return selectedDate >= currentDate;
  }
}
