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
import { ClinicService } from '../../../clinic/services/clinic.service';

@Component({
  selector: 'app-add-clinic',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AdminSidebarComponent,
    TokenComponent
  ],
  templateUrl: './add-clinic.component.html',
  styleUrl: './add-clinic.component.scss',
})
export class AddClinicComponent {
  branches: Branch[] = [];
  branchId: number;
  pageIndex: number = 0;
  pageSize: number = 50;
  clinicForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private clinicService: ClinicService,
    private branchService: BranchService,
    private toastrService: ToastrService,
    private router: Router
  ) {
    this.clinicForm = this.formBuilder.group({
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

  addClinic(): void {
    if (this.clinicForm.valid) {
      const clinicData = this.clinicForm.value;

      this.clinicService.addClinic(this.clinicForm.value).subscribe(
        (response) => {
          this.toastrService.success('Klinik başarıyla eklendi');
          this.router.navigate(['/admin-list-clinic']);
        }
      );
    } else {
      this.toastrService.error('Eksik alanlarını doldurunuz.');
    }
  }
}
