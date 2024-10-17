import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Doctor } from '../../../doctor/models/doctor';

import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { CommonModule } from '@angular/common';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { DoctorService } from '../../../doctor/services/doctor.service';

@Component({
  selector: 'app-update-doctor',
  standalone: true,
  imports: [
    AdminSidebarComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TokenComponent,
  ],
  templateUrl: './update-doctor.component.html',
  styleUrl: './update-doctor.component.scss',
})
export class UpdateDoctorComponent {
  doctorForm: FormGroup;
  doctor: Doctor;

  constructor(
    private formBuilder: FormBuilder,
    private doctorService: DoctorService,
    private toastrService: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.getDoctorProfile();
  }

  initForm() {
    this.doctorForm = this.formBuilder.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      branchID: ['', Validators.required],
      branchName: [''],
      title: ['', Validators.required],
      schoolName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      nationalIdentity: ['', Validators.required],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  getDoctorProfile() {
    this.route.paramMap.subscribe((params) => {
      const doctorId = params.get('doctorId');
      if (doctorId) {
        this.doctorService.getDoctorById(doctorId).subscribe((data) => {
          this.doctor = data;
          this.doctorForm.patchValue({
            id: data.id,
            firstName: data.firstName,
            lastName: data.lastName,
            branchID: data.branchID,
            branchName: data.branchName,
            title: data.title,
            schoolName: data.schoolName,
            dateOfBirth: data.dateOfBirth,
            nationalIdentity: data.nationalIdentity,
            phone: data.phone,
            address: data.address,
            email: data.email,
          });
        });
      }
    });
  }

  updateDoctor() {
    if (this.doctorForm.valid) {
      const updatedDoctor: Doctor = this.doctorForm.value;
      updatedDoctor.id = this.doctor.id;
      updatedDoctor.branchID = this.doctor.branchID;
      this.doctorService.updateDoctor(updatedDoctor).subscribe(
        (response) => {
          this.toastrService.success('Doktor başarıyla güncellendi');
          this.router.navigate(['admin-list-doctor']);
        }
      );
    } else {
      this.toastrService.error('Lütfen eksik alanları doldurun');
    }
  }
}
