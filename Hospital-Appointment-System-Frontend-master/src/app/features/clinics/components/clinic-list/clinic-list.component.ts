import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

import { Clinic } from '../../models/clinic';
import { BasicLayoutComponent } from '../../../../shared/components/basic-layout/basic-layout.component';
import { FormsModule } from '@angular/forms';
import { ClinicService } from '../../services/clinic.service';


@Component({
  selector: 'app-clinic-list',
  standalone: true,
  imports: [CommonModule,BasicLayoutComponent,FormsModule],
  templateUrl: './clinic-list.component.html',
  styleUrl: './clinic-list.component.scss'
})
export class ClinicListComponent implements OnInit {

  clinics: Clinic[] = [];
  pageIndex: number = 0;
  pageSize: number = 10;
  isLoading: boolean = true;
  selectedBranch: string = '';

  constructor(private clinicService: ClinicService,) {}

  ngOnInit(): void {
    this.getClinics();
  }
  getClinics() {
    this.clinicService.getClinics(this.pageIndex, this.pageSize).subscribe((response) => {
      this.clinics = response.items;
    });
  }
}
