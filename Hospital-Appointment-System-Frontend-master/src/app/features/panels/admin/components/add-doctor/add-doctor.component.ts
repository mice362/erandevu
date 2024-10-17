import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';

import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BranchService } from '../../../../branches/services/branch.service';
import { Branch } from '../../../../branches/models/branch';
import { ThisReceiver } from '@angular/compiler';
import { TokenComponent } from '../../../../../shared/components/token/token.component';
import { DoctorService } from '../../../doctor/services/doctor.service';

@Component({
  selector: 'app-add-doctor',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AdminSidebarComponent,
    TokenComponent
  ],
  templateUrl: './add-doctor.component.html',
  styleUrl: './add-doctor.component.scss',
})
export class AddDoctorComponent {
  branches: Branch[] = [];
  branchId: number;
  pageIndex: number = 0;
  pageSize: number = 50;
  doctorForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private doctorService: DoctorService,
    private branchService: BranchService,
    private toastrService: ToastrService,
    private router: Router
  ) {
    this.doctorForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      branchId: ['', Validators.required],
      title: ['', Validators.required],
      schoolName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      nationalIdentity: ['', Validators.required],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getBranches();
  }

  getBranches() {
    this.branchService
      .getBranches(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.branches = response.items;
        this.branches.forEach((branch) => {
          this.branchId = branch.id;
        });
      });
  }

  addDoctor(): void {
    if (this.doctorForm.valid) {
      const selectedBranchId = this.doctorForm.get('branchId')?.value;
      const doctorData = this.doctorForm.value;
      doctorData.branchId = selectedBranchId;

      this.doctorService.addDoctor(this.doctorForm.value).subscribe(
        (response) => {
          this.toastrService.success('Doktor başarıyla eklendi');
          this.router.navigate(['/admin-list-doctor']);
        }
      );
    } else {
      this.toastrService.error('Eksik alanlarını doldurunuz.');
    }
  }
}
