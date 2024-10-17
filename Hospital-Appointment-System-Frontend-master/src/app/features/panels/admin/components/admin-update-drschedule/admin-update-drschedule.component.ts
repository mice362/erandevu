import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateDrScheduleRequest } from '../../../../doctorschedule/models/create-request-drschedule';
import { ToastrService } from 'ngx-toastr';
import { TokenService } from '../../../../../core/auth/services/token.service';

import { DrscheduleService } from '../../../../doctorschedule/services/drschedule.service';
import { UpdateDoctorSchedule } from '../../../../doctorschedule/models/update-doctor-schedule';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-admin-update-drschedule',
  standalone: true,
  templateUrl: './admin-update-drschedule.component.html',
  styleUrl: './admin-update-drschedule.component.scss',
  imports: [FormsModule, CommonModule, AdminSidebarComponent,TokenComponent],
})
export class AdminUpdateDrscheduleComponent implements OnInit {
  selectedDate: string;
  startTime: string;
  endTime: string;
  minDate: string;
  maxDate: string;
  times: string[] = [];
  scheduleId: number;
  doctorId: string;

  constructor(
    private route: ActivatedRoute,
    private tokenService: TokenService,
    private drScheduleService: DrscheduleService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const today = new Date();
    const twoWeeksLater = new Date();
    twoWeeksLater.setDate(today.getDate() + 14);

    this.minDate = this.formatDate(today);
    this.maxDate = this.formatDate(twoWeeksLater);

    this.generateTimes();

    this.route.params.subscribe((params) => {
      if (params['doctorId'] && params['scheduleId']) {
        this.scheduleId = params['scheduleId'];
        this.doctorId = params['doctorId'];
      } else {
        this.scheduleId = 0;
        this.doctorId = '0';
      }
    });

    this.drScheduleService.getById(this.scheduleId).subscribe((schedule) => {
      this.selectedDate = schedule.date;
      this.startTime = this.formatTimeForDisplay(schedule.startTime);
      this.endTime = this.formatTimeForDisplay(schedule.endTime);
    });
  }

  private formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    return `${year}-${month.toString().padStart(2, '0')}-${day
      .toString()
      .padStart(2, '0')}`;
  }

  private generateTimes() {
    const startHour = 0;
    const endHour = 23;

    for (let hour = startHour; hour <= endHour; hour++) {
      this.times.push(this.formatTime(hour, 0));
      if (hour !== endHour || hour === 23) {
        this.times.push(this.formatTime(hour, 30));
      }
    }
  }

  private formatTime(hour: number, minute: number): string {
    return `${hour.toString().padStart(2, '0')}:${minute
      .toString()
      .padStart(2, '0')}`;
  }

  update() {
    if (this.selectedDate && this.startTime && this.endTime) {
      const userId: string = this.doctorId;

      const schedule: UpdateDoctorSchedule = {
        id: this.scheduleId,
        doctorID: userId,
        date: this.formatDateForDatabase(this.selectedDate),
        startTime: this.formatTimeForDatabase(this.startTime),
        endTime: this.formatTimeForDatabase(this.endTime),
      };

      this.drScheduleService.updateDoctorSchedule(schedule).subscribe(
        (response) => {
          this.toastrService.success(
            'Doktorun takvim çizelgesi başarılı bir şekilde güncellendi',
            'Başarılı'
          );
          this.router.navigate(['admin-doctor-schedule', this.doctorId]);
        }
      );
    } else {
      this.toastrService.error('', 'Tüm alanları doldurunuz.');
    }
  }

  private formatDateForDatabase(date: string): string {
    return new Date(date).toISOString().split('T')[0];
  }

  private formatTimeForDatabase(time: string): string {
    const [hour, minute] = time.split(':');
    return `${hour}:${minute}:00`;
  }
  private formatTimeForDisplay(time: string): string {
    const [hour, minute] = time.split(':');
    return `${hour}:${minute}`;
  }
  getFilteredTimes() {
    if (!this.startTime) {
      return this.times;
    }
    const startIndex = this.times.indexOf(this.startTime);
    return this.times
      .slice(startIndex + 1)
      .filter((time) => this.isValidEndTime(time));
  }

  private isValidEndTime(time: string): boolean {
    const [startHour, startMinute] = this.startTime.split(':').map(Number);
    const [endHour, endMinute] = time.split(':').map(Number);

    if (
      endHour < startHour ||
      (endHour === startHour && endMinute <= startMinute)
    ) {
      return false;
    }
    return true;
  }
}
