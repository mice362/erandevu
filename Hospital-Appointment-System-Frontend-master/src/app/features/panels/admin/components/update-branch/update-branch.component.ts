import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Branch } from '../../../../branches/models/branch';
import { BranchService } from '../../../../branches/services/branch.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-update-branch',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AdminSidebarComponent,
    TokenComponent,
  ],
  templateUrl: './update-branch.component.html',
  styleUrl: './update-branch.component.scss',
})
export class UpdateBranchComponent {
  branchForm: FormGroup;
  branch: Branch;

  constructor(
    private FormBuilder: FormBuilder,
    private branchService: BranchService,
    private toastrService: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.getBranches();
  }

  initForm() {
    this.branchForm = this.FormBuilder.group({
      id: [''],
      Name: ['', Validators.required],
    });
  }

  getBranches() {
    this.route.paramMap.subscribe((params) => {
      const branchId = params.get('branchId');

      if (branchId) {
        this.branchService.getByBranchId(branchId, 0, 1).subscribe((data) => {
          this.branch = data;
          this.branchForm.patchValue({
            id: data.id,
            Name: data.name,
          });
        });
      }
    });
  }

  updateBransh() {
    if (this.branchForm.valid) {
      const updatedBranch: Branch = this.branchForm.value;
      updatedBranch.id = this.branch.id;
      this.branchService.updateBranch(updatedBranch).subscribe(
        (response) => {
          this.toastrService.success('Branş başarıyla güncellendi');
          this.router.navigate(['/admin-branches']);
        }
      );
    } else {
      this.toastrService.error('Lütfen eksik alanları doldurun');
    }
  }
}
