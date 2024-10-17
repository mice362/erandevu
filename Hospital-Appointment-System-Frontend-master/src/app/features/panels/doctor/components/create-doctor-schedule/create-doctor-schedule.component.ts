import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CreateDrScheduleRequest } from '../../../../doctorschedule/models/create-request-drschedule';
import { ToastrService } from 'ngx-toastr';
import { TokenService } from '../../../../../core/auth/services/token.service';
import { DoctorSidebarComponent } from '../sidebar/doctorSidebar.component';
import { DrscheduleService } from '../../../../doctorschedule/services/drschedule.service';
import { Router } from '@angular/router';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-create-doctor-schedule',
  standalone: true,
  imports: [FormsModule, CommonModule, DoctorSidebarComponent, TokenComponent],
  templateUrl: './create-doctor-schedule.component.html',
  styleUrls: ['./create-doctor-schedule.component.scss'],
})
export class CreateDoctorScheduleComponent implements OnInit {
  selectedDate: string;
  startTime: string;
  endTime: string;
  minDate: string;
  maxDate: string;
  times: string[] = [];

  constructor(
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
    const startHour = 9;
    const endHour = 18;

    for (let hour = startHour; hour <= endHour; hour++) {
      this.times.push(this.formatTime(hour, 0));
      if (hour !== endHour) {
        this.times.push(this.formatTime(hour, 30));
      }
    }
  }

  private formatTime(hour: number, minute: number): string {
    return `${hour.toString().padStart(2, '0')}:${minute
      .toString()
      .padStart(2, '0')}`;
  }

  add() {
    if (this.selectedDate && this.startTime && this.endTime) {
      const userId: string = this.tokenService.getUserId();

      const schedule: CreateDrScheduleRequest = {
        doctorID: userId,
        date: this.formatDateForDatabase(this.selectedDate),
        startTime: this.formatTimeForDatabase(this.startTime),
        endTime: this.formatTimeForDatabase(this.endTime),
      };

      this.drScheduleService.add(schedule).subscribe(
        (response) => {
          this.toastrService.success(
            'Takvim çizelgenize başarılı bir şekilde eklendi',
            'Başarılı'
          );
          this.router.navigate(['list-doctor-schedule']);
        },
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
