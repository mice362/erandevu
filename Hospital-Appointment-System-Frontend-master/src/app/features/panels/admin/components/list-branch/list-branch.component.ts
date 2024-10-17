import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Branch } from '../../../../branches/models/branch';
import { BranchService } from '../../../../branches/services/branch.service';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { RouterModule } from '@angular/router';
import { ConfirmDialogComponent } from '../../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { PaginationComponent } from '../../../../../core/paging/components/pagination/pagination.component';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { FilterBranchNamePipe } from '../../../../pipe/filter-branch-name.pipe';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-list-branch',
  standalone: true,
  imports: [
    CommonModule,
    AdminSidebarComponent,
    RouterModule,
    PaginationComponent,
    FormsModule,
    FilterBranchNamePipe,
    TokenComponent,
  ],
  templateUrl: './list-branch.component.html',
  styleUrl: './list-branch.component.scss',
})
export class ListBranchComponent implements OnInit {
  branches: Branch[] = [];
  pageIndex: number = 0;
  pageSize: number = 5;
  totalPages: number = 0;
  hasNext: boolean = false;
  filterText: string = '';

  constructor(
    private branchService: BranchService,
    private dialog: MatDialog,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.getBranches();
  }

  onPageChanged(newPageIndex: number) {
    this.pageIndex = newPageIndex;
    this.getBranches();
  }

  getBranches() {
    this.branchService
      .getBranches(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.branches = response.items;
        this.branches.sort((a, b) => a.name.localeCompare(b.name));
        this.totalPages = response.pages;
        this.hasNext = response.hasNext;
      });
  }

  deleteBranch(branchId: number) {
    this.branchService.deleteBranch(branchId, this.pageIndex, 100).subscribe(
      (response) => {
        this.toastrService.success('Branş başarıyla silindi');
        this.getBranches();
      }
    );
  }

  confirmDelete(branchId: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'ONAY',
        message: 'Bu branşı silmek istediğinizden emin misiniz?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteBranch(branchId);
      }
    });
  }
}
