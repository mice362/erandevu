import { Component } from '@angular/core';
import { Feedback } from '../../../../feedbacks/models/feedback';
import { FeedbackService } from '../../../../feedbacks/services/feedback.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { CommonModule } from '@angular/common';
import { PatientSidebarComponent } from '../sidebar/psidebar.component';
import { RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TokenService } from '../../../../../core/auth/services/token.service';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-patient-list-feedback',
  standalone: true,
  imports: [
    CommonModule,
    PatientSidebarComponent,
    RouterModule,
    TokenComponent,
  ],
  templateUrl: './patient-list-feedback.component.html',
  styleUrl: './patient-list-feedback.component.scss',
})
export class PatientListFeedbackComponent {
  userID: string;
  feedbacks: Feedback[];
  pageIndex: number = 0;
  pageSize: number = 12;

  constructor(
    private feedbackService: FeedbackService,
    private toastrService: ToastrService,
    private tokenService: TokenService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.userID = this.tokenService.getUserId().toString();
    this.getFeedbackByUserId(this.userID);
  }

  sortFeedbacksByDateDescending(): void {
    this.feedbacks.sort((a, b) => {
      const dateA = new Date(a.createdDate);
      const dateB = new Date(b.createdDate);
      return dateB.getTime() - dateA.getTime();
    });
  }

  getFeedbackByUserId(userID: string) {
    this.feedbackService
      .getFeedbackByUserId(this.pageIndex, this.pageSize, userID)
      .subscribe((response) => {
        this.feedbacks = response.items;
        this.sortFeedbacksByDateDescending();
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
        this.getFeedbackByUserId(this.userID);
      }
    );
  }
}
