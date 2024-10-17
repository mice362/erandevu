import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { FormBuilder, FormGroup, FormsModule,ReactiveFormsModule, Validators } from '@angular/forms';
import { Patient } from '../../../patient/models/patientModel';

import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { PatientService } from '../../../patient/services/patient.service';


@Component({
  selector: 'app-add-patient',
  standalone: true,
  imports: [
   CommonModule,
   FormsModule,
   ReactiveFormsModule,
   AdminSidebarComponent,
   TokenComponent

  ],
  templateUrl: './add-Patient.component.html',
  styleUrl: './add-Patient.component.scss',

})
export class AddPatientComponent {
  patient: Patient[] = [];
  patientId: string;
  pageIndex: number = 0;
  pageSize: number = 50;
  PatientForm: FormGroup;

  constructor(
    private formbuilder: FormBuilder,
    private patientService: PatientService,

    private toastrService:ToastrService,
    private router: Router
  ) {
    this.PatientForm = this.formbuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: [''],
      age: [''],
      height: [''],
      weight: [''],
      bloodGroup: [''],
      nationalIdentity: [''],
      phone: ['', Validators.required],
      address: [''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getPatients();
  }

  getPatients() {
    this.patientService
      .getPatients(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.patient = response.items;
        this.patient.forEach((patient) => {
          this.patientId = patient.id;
        });
      });
  }

  addPatient(): void {
    if (this.PatientForm.valid) {
      const selectedBranchId = this.PatientForm.get('patientId')?.value;

      this.patientService.addPatient(this.PatientForm.value).subscribe(
        (response) => {
          this.toastrService.success('Hasta başarıyla eklendi');
          this.router.navigate(['/admin-patient']);
        }
      );
    } else {
      this.toastrService.error('Eksik alanlarını doldurunuz.');
    }
  }







}
