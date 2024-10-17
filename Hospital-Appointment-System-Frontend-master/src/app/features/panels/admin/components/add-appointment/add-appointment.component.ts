import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AppointmentService } from '../../../../appointments/services/appointment.service';


import { BranchService } from '../../../../branches/services/branch.service';
import { Appointment } from '../../../../appointments/models/appointmentModel';
import { Branch } from '../../../../branches/models/branch';
import { Doctor } from '../../../doctor/models/doctor';
import { Patient } from '../../../patient/models/patientModel';
import { AppointmentForPatientPanel } from '../../../../appointments/models/appointmentforpatientpanel';
import { DoctorSchedule } from '../../../../doctorschedule/models/doctorschedule';
import { DoctorForAppointment } from '../../../doctor/models/doctorForAppointment';
import { DrscheduleService } from '../../../../doctorschedule/services/drschedule.service';
import { CreateAppointment } from '../../../../appointments/models/createAppointment';
import { TokenService } from '../../../../../core/auth/services/token.service';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SmsService } from '../../../../appointments/services/smsMock.service';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { PatientService } from '../../../patient/services/patient.service';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { DoctorService } from '../../../doctor/services/doctor.service';


@Component({
  selector: 'app-add-appointment',
  templateUrl: './add-appointment.component.html',
  styleUrls: ['./add-appointment.component.scss'],
  standalone: true,
  imports: [FormsModule, AdminSidebarComponent,CommonModule, TokenComponent],
})
export class AddAppointmentComponent implements OnInit {
  pageIndex: number = 0;
  pageSize: number = 100;
  branches: Branch[] = [];
  doctors: DoctorForAppointment[] = [];
  patients: Patient[] = [];
  schedules: DoctorSchedule[] = [];
  appointments: AppointmentForPatientPanel[] = [];
  availableDates: string[] = [];
  selectedBranch: Branch | null = null;
  selectedDoctor: DoctorForAppointment | null = null;
  selectedPatient: Patient | null = null;
  selectedDate: string | null = null;
  selectedTime: string | null = null;
  timesWithStatus: { time: string; disabled: boolean }[] = [];

  constructor(
    private patientService: PatientService,
    private branchService: BranchService,
    private doctorService: DoctorService,
    private doctorScheduleService: DrscheduleService,
    private appointmentService: AppointmentService,
    private tokenService: TokenService,
    private toastrService: ToastrService,
    private router: Router,
    private smsService: SmsService
  ) {}

  ngOnInit(): void {
    this.branchService
      .getBranches(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.branches = response.items;
      });
    this.getPatients();
  }

  getPatients(): void {
    this.patientService.getPatients(0, 100).subscribe(
      (response) => {
        this.patients = response.items;
      }
    );
  }

  onBranchChange(): void {
    if (this.selectedBranch) {
      this.doctorService
        .getListByBranchId(
          this.pageIndex,
          this.pageSize,
          this.selectedBranch.id
        )
        .subscribe((response) => {
          this.doctors = response.items;
          this.selectedDoctor = null;
          this.schedules = [];
          this.timesWithStatus = [];
        });
    }
  }

  onDoctorChange(): void {
    if (this.selectedDoctor) {
      this.doctorScheduleService
        .getDoctorSchedule(
          this.pageIndex,
          this.pageSize,
          this.selectedDoctor.id
        )
        .subscribe((schedules) => {
          this.schedules = schedules.items;
          this.availableDates = [
            ...new Set(this.schedules.map((schedule) => schedule.date)),
          ]
            .map((date) => this.formatDate(date))
            .filter((date) => this.isFutureDate(date)); // Sadece gelecekteki tarihleri filtrele
          this.selectedDate = null;
          this.timesWithStatus = [];
        });
    }
  }

  onDateChange(): void {
    if (this.selectedDoctor && this.selectedDate) {
      this.getDoctorAppointments();
      this.generateTimes();
      this.updateAvailableTimes();
    }
  }

  getDoctorAppointments(): void {
    if (this.selectedDoctor && this.selectedDate) {
      const formattedDate = this.formatDate(this.selectedDate);
      this.appointmentService
        .getByDoctorDate(
          this.pageIndex,
          this.pageSize,
          this.selectedDoctor.id,
          formattedDate
        )
        .subscribe(
          (response) => {
            this.appointments = response.items;
            this.updateAvailableTimes();
          }
        );
    }
  }

  updateAvailableTimes(): void {
    this.timesWithStatus = this.timesWithStatus.map((timeSlot) => ({
      ...timeSlot,
      disabled: this.isTimeSlotBooked(timeSlot.time),
    }));
  }

  isTimeSlotBooked(time: string): boolean {
    return this.appointments.some((appointment) => {
      const appointmentTime = appointment.time.split(':').slice(0, 2);
      const slotTime = time.split(':').slice(0, 2);
      return (
        appointmentTime[0] === slotTime[0] && appointmentTime[1] === slotTime[1]
      );
    });
  }

  formatDate(date: string): string {
    const d = new Date(date);
    if (isNaN(d.getTime())) {
      throw new Error('Geçersiz tarih değeri');
    }
    const month = (d.getMonth() + 1).toString().padStart(2, '0');
    const day = d.getDate().toString().padStart(2, '0');
    const year = d.getFullYear();
    return [year, month, day].join('-');
  }

  isFutureDate(date: string): boolean {
    const today = new Date();
    const d = new Date(date);
    return d >= today;
  }

  generateTimes(): void {
    this.timesWithStatus = [];
    if (this.schedules.length > 0) {
      const schedule = this.schedules.find(
        (schedule) => this.formatDate(schedule.date) === this.selectedDate
      );
      if (schedule) {
        const startTime = this.convertToTime(schedule.startTime);
        const endTime = this.convertToTime(schedule.endTime);
        let currentTime = startTime;
        while (currentTime <= endTime) {
          this.timesWithStatus.push({
            time: this.formatTime(currentTime),
            disabled: false, // initially, all time slots are not disabled
          });
          currentTime += 30; // increment by 30 minutes
        }
      }
    }
    this.updateAvailableTimes();
  }

  convertToTime(time: string): number {
    const [hour, minute] = time.split(':').map(Number);
    return hour * 60 + minute;
  }

  formatTime(minutes: number): string {
    const hour = Math.floor(minutes / 60);
    const minute = minutes % 60;
    return `${hour.toString().padStart(2, '0')}:${minute
      .toString()
      .padStart(2, '0')}`;
  }

  addAppointment(): void {
    if (
      this.selectedDoctor &&
      this.selectedDate &&
      this.selectedTime &&
      this.selectedPatient
    ) {
      const formattedTime = this.selectedTime + ':00'; // "HH:mm:ss" format
      const appointment: CreateAppointment = {
        date: this.selectedDate,
        time: formattedTime,
        status: true,
        doctorID: this.selectedDoctor?.id,
        patientID: this.selectedPatient?.id,
      };
      this.appointmentService.createAppointment(appointment).subscribe(
        (response) => {
          this.toastrService.success('Randevunuz oluşturuldu');
          this.router.navigate(['upcoming-appointments']);

          //SMS SIMULATION
          const message = `Sayın ${this.selectedPatient?.firstName} ${this.selectedPatient?.lastName},
                            ${this.selectedDoctor?.firstName} ${this.selectedDoctor?.lastName} doktorundan randevunuz başarıyla oluşturulmuştur.
                            Branş: ${this.selectedDoctor?.branchName}
                            Randevu Tarihi: ${this.selectedDate}
                            Randevu Saati: ${formattedTime}
                            Detaylı bilgi için web sitemizi ziyaret edebilirsiniz.`;
          this.smsService
            .sendSms(String(this.selectedPatient?.phone), message)
            .subscribe(
              (smsResponse) => {
                this.toastrService.success('Sms tarafınıza gönderildi.');
              }
            );
        }
      );
    } else {
      this.toastrService.error('Lütfen gerekli alanları doldurun.');
    }
  }
}
