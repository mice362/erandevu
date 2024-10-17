import { Component } from '@angular/core';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { CommonModule } from '@angular/common';
import { Feedback } from '../../../../feedbacks/models/feedback';
import { FeedbackService } from '../../../../feedbacks/services/feedback.service';
import { ConfirmDialogComponent } from '../../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { RouterModule } from '@angular/router';
import { PaginationComponent } from '../../../../../core/paging/components/pagination/pagination.component';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-feedback',
  standalone: true,
  imports: [
    AdminSidebarComponent,
    CommonModule,
    RouterModule,
    PaginationComponent,
    TokenComponent,
  ],
  templateUrl: './list-feedback.component.html',
  styleUrl: './list-feedback.component.scss',
})
export class ListFeedbackComponent {
  feedbacks: Feedback[] = [];
  pageIndex: number = 0;
  pageSize: number = 5;
  totalPages: number = 0;
  hasNext: boolean = false;

  constructor(
    private feedbackService: FeedbackService,
    private toastrService: ToastrService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getFeedbacks();
  }
  onPageChanged(newPageIndex: number) {
    this.pageIndex = newPageIndex;
    this.getFeedbacks();
  }

  sortFeedbacksByDateDescending(): void {
    this.feedbacks.sort((a, b) => {
      const dateA = new Date(a.createdDate);
      const dateB = new Date(b.createdDate);
      return dateB.getTime() - dateA.getTime();
    });
  }

  getFeedbacks() {
    this.feedbackService
      .getFeedbacks(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.feedbacks = response.items;
        this.sortFeedbacksByDateDescending();
        this.totalPages = response.pages;
        this.hasNext = response.hasNext;
      });
  }

  confirmDelete(feedbackId: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'ONAY',
        message: 'Bu geri bildirimi silmek istediğinizden emin misiniz?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteFeedback(feedbackId);
      }
    });
  }

  deleteFeedback(feedbackId: number) {
    this.feedbackService.deleteFeedback(feedbackId).subscribe(
      (response) => {
        this.toastrService.success('Geri bildirim başarıyla silindi');
        this.getFeedbacks();
      },
    );
  }

  approveFeedback(feedbackId: number) {
    const feedback = this.feedbacks.find((f) => f.id === feedbackId);
    if (feedback) {
      const updatedFeedback = { ...feedback, isApproved: true };
      this.feedbackService.updateFeedback(updatedFeedback).subscribe(
        (response) => {
          feedback.isApproved = true;
          this.toastrService.success('Geri bildirim başarıyla onaylandı');
        },
        (error) => {
          this.toastrService.error('Geri bildirim onaylanamadı');
        }
      );
    }
  }
}
