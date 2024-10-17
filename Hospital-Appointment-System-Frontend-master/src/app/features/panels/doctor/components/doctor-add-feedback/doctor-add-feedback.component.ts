import { Component } from '@angular/core';
import { DoctorSidebarComponent } from '../sidebar/doctorSidebar.component';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Feedback } from '../../../../feedbacks/models/feedback';
import { FeedbackService } from '../../../../feedbacks/services/feedback.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

import { Doctor } from '../../models/doctor';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { DoctorService } from '../../services/doctor.service';

@Component({
  selector: 'app-doctor-add-feedback',
  standalone: true,
  imports: [
    DoctorSidebarComponent,
    CommonModule,
    ReactiveFormsModule,
    TokenComponent,
  ],
  templateUrl: './doctor-add-feedback.component.html',
  styleUrl: './doctor-add-feedback.component.scss',
})
export class DoctorAddFeedbackComponent {
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
    private router: Router
  ) {
    this.feedbackForm = this.formBuilder.group({
      userID: ['', Validators.required],
      text: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.doctorService.getDoctorProfile().subscribe((doctor) => {
      this.doctor = doctor;
      this.userID = doctor.id;
      this.feedbackForm.patchValue({
        userID: this.userID,
      });
    });
  }

  addFeedback(): void {
    if (this.feedbackForm.valid) {
      this.feedbackService.addFeedback(this.feedbackForm.value).subscribe(
        (response) => {
          this.toastrService.success('Geri bildirim başarıyla eklendi');
          this.router.navigate(['/doctor-feedbacks']);
        }
      );
    } else {
      this.toastrService.error('Eksik alanlarını doldurunuz.');
    }
  }
}
