import { Component } from '@angular/core';
import { DoctorSidebarComponent } from '../sidebar/doctorSidebar.component';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FeedbackService } from '../../../../feedbacks/services/feedback.service';

import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Feedback } from '../../../../feedbacks/models/feedback';
import { Doctor } from '../../models/doctor';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { DoctorService } from '../../services/doctor.service';

@Component({
  selector: 'app-doctor-update-feedback',
  standalone: true,
  imports: [
    DoctorSidebarComponent,
    CommonModule,
    ReactiveFormsModule,
    TokenComponent,
  ],
  templateUrl: './doctor-update-feedback.component.html',
  styleUrl: './doctor-update-feedback.component.scss',
})
export class DoctorUpdateFeedbackComponent {
  doctor: Doctor;
  userID: string;
  feedbacks: Feedback[] = [];
  pageIndex: number = 0;
  pageSize: number = 50;
  feedbackForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private feedbackService: FeedbackService,
    private doctorService: DoctorService,
    private toastrService: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.feedbackForm = this.formBuilder.group({
      id: ['', Validators.required],
      userID: ['', Validators.required],
      text: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const feedbackId = params['feedbackId'];
      this.feedbackService
        .getFeedbackById(feedbackId)
        .subscribe((feedback: Feedback) => {
          this.feedbackForm.patchValue({
            id: feedback.id,
            userID: feedback.userID,
            text: feedback.text,
          });
        });
    });
  }

  updateFeedback(): void {
    if (this.feedbackForm.valid) {
      this.feedbackService.updateFeedback(this.feedbackForm.value).subscribe(
        (response) => {
          this.toastrService.success('Geri bildirim başarıyla güncellendi');
          this.router.navigate(['/doctor-feedbacks']);
        }
      );
    } else {
      this.toastrService.error('Eksik alanlarını doldurunuz.');
    }
  }
}
