import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Branch } from '../../../../branches/models/branch';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { BranchService } from '../../../../branches/services/branch.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-add-branch',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AdminSidebarComponent,
    TokenComponent,
  ],
  templateUrl: './add-branch.component.html',
  styleUrl: './add-branch.component.scss',
})
export class AddBranchComponent {
  branch: Branch[] = [];
  branchId: number;
  pageIndex: number = 0;
  pageSize: number = 50;
  BranchForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private branchService: BranchService,

    private toastrService: ToastrService,
    private router: Router
  ) {
    this.BranchForm = this.formBuilder.group({
      Name: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getBranches();
  }

  getBranches() {
    this.branchService
      .getBranches(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.branch = response.items;
        this.branch.forEach((branch) => {
          this.branchId = branch.id;
        });
      });
  }

  addBranches(): void {
    if (this.BranchForm.valid) {
      const selectedBranchId = this.BranchForm.get('branchId')?.value;
      this.branchService.addbranch(this.BranchForm.value).subscribe(
        (response) => {
          this.toastrService.success('Branş başarıyla eklendi');
          this.router.navigate(['/admin-branches']);
        }
      );
    } else {
      this.toastrService.error('Eksik alanlarını doldurunuz.');
    }
  }
}
