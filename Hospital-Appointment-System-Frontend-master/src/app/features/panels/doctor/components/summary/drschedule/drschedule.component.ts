import { Component, OnInit } from '@angular/core';
import { DoctorSidebarComponent } from "../../sidebar/doctorSidebar.component";
import { TokenService } from '../../../../../../core/auth/services/token.service';
import { DrscheduleService } from '../../../../../doctorschedule/services/drschedule.service';
import { DoctorSchedule } from '../../../../../doctorschedule/models/doctorschedule';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-drschedule',
    standalone: true,
    templateUrl: './drschedule.component.html',
    styleUrl: './drschedule.component.scss',
    imports: [DoctorSidebarComponent,CommonModule]
})
export class DrscheduleComponent implements OnInit {
  schedules: DoctorSchedule[] = [];
  pageIndex: number = 0;
  pageSize: number = 100;

  constructor(
    private tokenService: TokenService,
    private drScheduleService: DrscheduleService,

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


}


