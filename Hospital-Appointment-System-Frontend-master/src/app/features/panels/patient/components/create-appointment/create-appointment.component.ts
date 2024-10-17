import { Component, OnInit } from '@angular/core';
import { BranchService } from '../../../../branches/services/branch.service';
import { Branch } from '../../../../branches/models/branch';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DoctorForAppointment } from '../../../doctor/models/doctorForAppointment';
import { DoctorSchedule } from '../../../../doctorschedule/models/doctorschedule';
import { DrscheduleService } from '../../../../doctorschedule/services/drschedule.service';
import { PatientSidebarComponent } from '../sidebar/psidebar.component';
import { AppointmentForPatientPanel } from '../../../../appointments/models/appointmentforpatientpanel';
import { AppointmentService } from '../../../../appointments/services/appointment.service';
import { CreateAppointment } from '../../../../appointments/models/createAppointment';
import { TokenService } from '../../../../../core/auth/services/token.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

import { Patient } from '../../models/patientModel';
import { SmsService } from '../../../../appointments/services/smsMock.service';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { PatientService } from '../../services/patient.service';
import { DoctorService } from '../../../doctor/services/doctor.service';

@Component({
  selector: 'app-create-appointment',
  standalone: true,
  templateUrl: './create-appointment.component.html',
  styleUrl: './create-appointment.component.scss',
  imports: [CommonModule, FormsModule, PatientSidebarComponent, TokenComponent],
})
export class CreateAppointmentComponent implements OnInit {
  pageIndex: number = 0;
  pageSize: number = 100;
  branches: Branch[] = [];
  doctors: DoctorForAppointment[] = [];
  schedules: DoctorSchedule[] = [];
  appointments: AppointmentForPatientPanel[] = [];
  availableDates: string[] = [];
  selectedBranch: Branch | null = null;
  selectedDoctor: DoctorForAppointment | null = null;
  selectedDate: string | null = null;
  timesWithStatus: { time: string; disabled: boolean }[] = [];

  selectedTime: string | null = null;
  patient: Patient | null = null; // Patient objesini saklamak için

  constructor(
    private branchService: BranchService,
    private doctorService: DoctorService,
    private doctorScheduleService: DrscheduleService,
    private appointmentService: AppointmentService,
    private tokenService: TokenService,
    private toastrService: ToastrService,
    private router: Router,
    private patientService: PatientService, // Ekledik
    private smsService: SmsService
  ) {}

  ngOnInit(): void {
    this.patientService.getPatientProfile().subscribe((response) => {
      if (response.phone && response.nationalIdentity) {
        this.branchService
          .getBranches(this.pageIndex, this.pageSize)
          .subscribe((response) => {
            this.branches = response.items;
          });
      } else {
        this.toastrService.warning(
          'Randevu almak için önce profil bilgilerinizdeki eksik alanları doldurmanız gereklidir.'
        );
        this.router.navigate(['patient-profile']);
      }
    });
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

          if (this.availableDates.length === 0) {
            this.selectedDate = null;
            this.timesWithStatus = [];
            this.toastrService.warning(
              'Doktorun ilerleyen tarihlerde herhangi bir randevusu bulunmamaktadır.'
            );
          } else {
            this.selectedDate = null;
            this.timesWithStatus = [];
          }
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
        .subscribe((response) => {
          this.appointments = response.items;
          this.updateAvailableTimes();
        });
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
      const appointmentTime = appointment.time.split(':').slice(0, 2); // Saat ve dakika kısmı
      const slotTime = time.split(':').slice(0, 2); // Saat ve dakika kısmı
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
            disabled: false,
          });
          currentTime += 30;
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
    this.patientService.getPatientProfile().subscribe((patient: Patient) => {
      if (!patient || !patient.phone) {
        throw new Error(
          'Patient bilgileri alınamadı veya telefon numarası yok'
        );
      }
      if (this.selectedDate && this.selectedTime && this.selectedDoctor) {
        const formattedTime = this.selectedTime + ':00'; // "HH:mm:ss" format
        const appointment: CreateAppointment = {
          date: this.selectedDate,
          time: formattedTime,
          status: true,
          doctorID: this.selectedDoctor.id,
          patientID: patient.id,
        };

        this.appointmentService.createAppointment(appointment).subscribe(
          (response) => {
            this.toastrService.success('Randevunuz oluşturuldu');
            this.router.navigate(['patient-upcoming-appointments']);
            const message = `Sayın ${patient.firstName} ${patient.lastName},
                                                ${this.selectedDoctor?.firstName} ${this.selectedDoctor?.lastName} doktorundan randevunuz başarıyla oluşturulmuştur.
                                                Branş: ${this.selectedDoctor?.branchName}
                                                Randevu Tarihi: ${this.selectedDate}
                                                Randevu Saati: ${formattedTime}
                                                Detaylı bilgi için web sitemizi ziyaret edebilirsiniz.`;
            this.smsService
              .sendSms(patient.phone, message)
              .subscribe((smsResponse) => {
                this.toastrService.success('Sms tarafınıza gönderildi.');
              });
          },
          (responseError) => {
            this.toastrService.error(
              responseError.error.Detail,
              'Hatalı İşlem'
            );
          }
        );
      }
    });
  }
}
