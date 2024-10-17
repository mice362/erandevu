import { Component } from '@angular/core';
import { FeedbackService } from '../../../../../feedbacks/services/feedback.service';
import { TokenService } from '../../../../../../core/auth/services/token.service';
import { response } from 'express';
import { CommonModule } from '@angular/common';
import { Feedback } from '../../../../../feedbacks/models/feedback';

@Component({
  selector: 'app-feedback',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './feedback.component.html',
  styleUrl: './feedback.component.scss',
})
export class FeedbackComponent {
  patientID: string;
  feedbacks: Feedback[];
  pageIndex: number = 0;
  pageSize: number = 12;

  constructor(
    private feedbackService: FeedbackService,
    private tokenService: TokenService
  ) {}

  ngOnInit(): void {
    this.patientID = this.tokenService.getUserId().toString();
    this.getFeedbackByUserId(this.patientID);
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
}
