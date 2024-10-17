import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Feedback } from '../../../../feedbacks/models/feedback';
import { FeedbackService } from '../../../../feedbacks/services/feedback.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-update-feedback',
  standalone: true,
  imports: [
    CommonModule,
    AdminSidebarComponent,
    ReactiveFormsModule,
    RouterModule,
    TokenComponent,
  ],
  templateUrl: './update-feedback.component.html',
  styleUrl: './update-feedback.component.scss',
})
export class UpdateFeedbackComponent {
  feedbackForm: FormGroup;
  feedback: Feedback;
  feedbackId: number;

  constructor(
    private formBuilder: FormBuilder,
    private feedbackService: FeedbackService,
    private toastrService: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.getFeedbackDetails();
  }

  initForm() {
    this.feedbackForm = this.formBuilder.group({
      id: [''],
      userID: [''],
      userFirstName: ['', Validators.required],
      userLastName: ['', Validators.required],
      text: ['', Validators.required],
    });
  }

  getFeedbackDetails() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('feedbackId');
      if (id !== null) {
        this.feedbackId = +id;
        if (isNaN(this.feedbackId)) {
          return;
        }
        this.feedbackService
          .getFeedbackById(this.feedbackId)
          .subscribe((data: Feedback) => {
            this.feedbackForm.patchValue({
              id: this.feedbackId,
              userId: data.userID,
              userFirstName: data.userFirstName,
              userLastName: data.userLastName,
              text: data.text,
            });
          });
      }
    });
  }

  updateFeedback() {
    if (this.feedbackForm.valid && this.feedbackId !== undefined) {
      const updatedFeedback: Feedback = this.feedbackForm.value;
      updatedFeedback.id = this.feedbackId;
      this.feedbackService.updateFeedback(updatedFeedback).subscribe(
        (response) => {
          this.toastrService.success('Geri bildirim başarıyla güncellendi');
          this.router.navigate(['admin-list-feedback']);
        }
      );
    } else {
      this.toastrService.error('Lütfen eksik alanları doldurun');
    }
  }
}
