import { Component } from '@angular/core';
import { Patient } from '../../models/patientModel';
import { Feedback } from '../../../../feedbacks/models/feedback';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FeedbackService } from '../../../../feedbacks/services/feedback.service';

import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PatientSidebarComponent } from '../sidebar/psidebar.component';
import { CommonModule } from '@angular/common';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-patient-update-feedback',
  standalone: true,
  imports: [
    PatientSidebarComponent,
    ReactiveFormsModule,
    CommonModule,
    TokenComponent,
  ],
  templateUrl: './patient-update-feedback.component.html',
  styleUrl: './patient-update-feedback.component.scss',
})
export class PatientUpdateFeedbackComponent {
  patient: Patient;
  userID: string;
  feedbacks: Feedback[] = [];
  pageIndex: number = 0;
  pageSize: number = 10;
  feedbackForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private feedbackService: FeedbackService,
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
          this.router.navigate(['/patient-feedbacks']);
        }
      );
    } else {
      this.toastrService.error('Eksik alanlarını doldurunuz.');
    }
  }
}
