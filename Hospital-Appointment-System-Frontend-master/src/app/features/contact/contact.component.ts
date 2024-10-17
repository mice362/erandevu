import { Component } from '@angular/core';
import { BasicLayoutComponent } from '../../shared/components/basic-layout/basic-layout.component';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { FeedbackService } from '../feedbacks/services/feedback.service';
import { ScrollService } from '../../shared/components/footer-content/scroll-service.service';
import { PatientService } from '../panels/patient/services/patient.service';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [BasicLayoutComponent,FormsModule,ReactiveFormsModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss'
})
export class ContactComponent {

  patientFirstName: string = '';
  patientLastName: string = '';
  patientEmail: string = '';
  patientId: string;
  feedbackForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private patientService: PatientService,
    private feedbackService : FeedbackService,
    private toastrService: ToastrService,
    private router: Router,
    private scrollService: ScrollService
  ) {
    this.feedbackForm = this.formBuilder.group({
      userID: ['', Validators.required],
      text: ['', Validators.required],
    });
  }
  checkRegisteredUser() {
    const token = localStorage.getItem("token");
    if (!token) {
      this.toastrService.warning('İletişim formunu doldurmak için lütfen giriş yapın.');
      this.router.navigateByUrl('/login');
    } else{
      this.addFeedback();
    }
  }

  ngOnInit(): void {
    //this.getPatientProfile();
    this.patientService.getPatientProfile().subscribe((patient) => {
      this.patientFirstName = patient.firstName;
      this.patientLastName = patient.lastName;
      this.patientEmail = patient.email;
      this.patientId = patient.id;
      this.feedbackForm.patchValue({
        userID: this.patientId
      });
    });
  }

  addFeedback(): void {
    if (this.feedbackForm.valid) {
      this.feedbackService.addFeedback(this.feedbackForm.value).subscribe(

        (response) => {
          this.toastrService.success('Geri bildirim başarıyla eklendi');
          this.router.navigate(['/patient-feedbacks']);
        },
        (error) => {
          this.toastrService.error('Geri bildirim eklenemedi!');
        }
      );
    } else {
      this.toastrService.error('Eksik alanlarını doldurunuz.');
    }
  }


}
