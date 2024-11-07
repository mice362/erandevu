import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Clinic } from '../../../../clinics/models/clinic';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ClinicService } from '../../../../clinics/services/clinic.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AdminSidebarComponent } from '../sidebar/adminSidebar.component';
import { TokenComponent } from '../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-add-clinic',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AdminSidebarComponent,
    TokenComponent,
  ],
  templateUrl: './add-clinic.component.html',
  styleUrl: './add-clinic.component.scss',
})
export class AddClinicComponent {
  clinic: Clinic[] = [];
  clinicId: number;
  pageIndex: number = 0;
  pageSize: number = 50;
  clinicForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private clinicService: ClinicService,

    private toastrService: ToastrService,
    private router: Router
  ) {
    this.clinicForm = this.formBuilder.group({
      Name: ['', Validators.required],
      Phone: ['', Validators.required],
      Address: ['', Validators.required],
      Email: ['', Validators.required],
      About: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getClinics();
  }

  getClinics() {
    this.clinicService
      .getClinics(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.clinic = response.items;
        this.clinic.forEach((clinic) => {
          this.clinicId = clinic.id;
        });
      });
  }

  addClinics(): void {
    if (this.clinicForm.valid) {
      console.log(this.clinicForm.value);
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
