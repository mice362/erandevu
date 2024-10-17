import { Component, OnInit } from '@angular/core';
import { DoctorSchedule } from '../../../../doctorschedule/models/doctorschedule';
import { TokenService } from '../../../../../core/auth/services/token.service';
import { DrscheduleService } from '../../../../doctorschedule/services/drschedule.service';
import { DoctorSidebarComponent } from '../sidebar/doctorSidebar.component';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-list-doctor-schedule',
  standalone: true,
  templateUrl: './list-doctor-schedule.component.html',
  styleUrl: './list-doctor-schedule.component.scss',
  imports: [DoctorSidebarComponent, CommonModule, RouterModule, TokenComponent],
})
export class ListDoctorScheduleComponent implements OnInit {
  schedules: DoctorSchedule[] = [];
  pageIndex: number = 0;
  pageSize: number = 100;

  constructor(
    private tokenService: TokenService,
    private drScheduleService: DrscheduleService,
    private toastrService: ToastrService,
    private router: Router,
    private dialog: MatDialog
  ) {}
  ngOnInit(): void {
    this.getDoctorSchedule();
  }

  getDoctorSchedule() {
    let doctorId = this.tokenService.getUserId().toString();
    this.drScheduleService
      .getDoctorSchedule(this.pageIndex, this.pageSize, doctorId)
      .subscribe((response) => {
        this.schedules = response.items;
      });
  }

  deleteDoctorSchedule(id: number) {
    this.drScheduleService.deleteDoctorSchedule(id).subscribe(
      (reponse) => {
        this.toastrService.success(
          'Seçtiğiniz tarihe ait takvim çizelgeniz silindi',
          'Başarılı'
        );
        this.getDoctorSchedule();
      }
    );
  }
  confirmDelete(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'ONAY',
        message: 'Bu çalışma takvimini silmek istediğinizden emin misiniz?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteDoctorSchedule(id);
      }
    });
  }

  goToRoute(scheduleId: number) {
    this.router.navigate(['doctorschedule', scheduleId]);
  }

  isFutureDate(date: string): boolean {
    const scheduleDate = new Date(date);
    const today = new Date();
    return scheduleDate > today;
  }

}
