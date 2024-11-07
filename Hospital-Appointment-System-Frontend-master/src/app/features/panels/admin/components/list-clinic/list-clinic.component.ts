import { Component, OnInit } from '@angular/core';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { CommonModule } from '@angular/common';

import { Clinic } from '../../../../clinics/models/clinic';
import { Router, RouterModule } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmDialogComponent } from '../../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { CapitalizeFirstPipe } from '../../../../pipe/capitalize-first.pipe';
import { FormsModule } from '@angular/forms';
import { PaginationComponent } from '../../../../../core/paging/components/pagination/pagination.component';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { ToastrService } from 'ngx-toastr';
import { ClinicService } from '../../../../clinics/services/clinic.service';
import { FilterClinicNamePipe } from '../../../../pipe/filter-clinic-name.pipe';

@Component({
  selector: 'app-list-clinic',
  standalone: true,
  imports: [
    AdminSidebarComponent,
    CommonModule,
    RouterModule,
    MatDialogModule,
    MatButtonModule,
    FormsModule,
    CapitalizeFirstPipe,
    FilterClinicNamePipe,
    PaginationComponent,
    TokenComponent,
  ],

  templateUrl: './list-clinic.component.html',
  styleUrl: './list-clinic.component.scss',
})
export class ListClinicComponent implements OnInit {
  clinics: Clinic[] = [];
  pageIndex: number = 0;
  pageSize: number = 5;
  totalPages: number = 0;
  hasNext: boolean = false;
  filterText: string = '';
  selectedBranch: string = '';

  constructor(
    private clinicService: ClinicService,
    private dialog: MatDialog,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getClinics();
  }

  onPageChanged(newPageIndex: number) {
    this.pageIndex = newPageIndex;
    this.getClinics();
  }

  getClinics() {
    this.clinicService
      .getClinics(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.clinics = response.items;
        this.clinics.sort((a, b) => a.name.localeCompare(b.name));
        this.totalPages = response.pages;
        this.hasNext = response.hasNext;
      });
  }

  confirmDelete(clinicId: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'ONAY',
        message: 'Bu kliniği silmek istediğinizden emin misiniz?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteClinic(clinicId);
      }
    });
  }

  deleteClinic(clinicId: number) {
    this.clinicService.deleteClinic(clinicId, this.pageIndex, 100).subscribe(
      (response) => {
        this.toastrService.success('Klinik başarıyla silindi.');
        this.getClinics();
      }
    );
  }

}
