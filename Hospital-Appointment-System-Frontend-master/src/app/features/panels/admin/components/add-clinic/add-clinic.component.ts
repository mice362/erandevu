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
  styleUrls: ['./add-clinic.component.scss'],
})
export class AddClinicComponent {
  clinic: Clinic[] = [];
  clinicId: number;
  pageIndex: number = 0;
  pageSize: number = 50;
  clinicForm: FormGroup;

  // logoName ve logo değişkenleri tanımlanıyor
  logoName: string = '';
  logo: Uint8Array | null = null;

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
      Email: ['', [Validators.required, Validators.email]],
      About: ['', Validators.required],
      Logo: [''],
      LogoName: [''],
    });
  }

  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      const file = fileInput.files[0];
      this.logoName = file.name;

      const reader = new FileReader();
      reader.onload = () => {
        const arrayBuffer = reader.result as ArrayBuffer;
        this.logo = new Uint8Array(arrayBuffer);
        this.clinicForm.patchValue({
          Logo: this.logo,
          LogoName: this.logoName
        });
      };
      reader.readAsArrayBuffer(file);
    }
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
      this.clinicService.addClinic(this.clinicForm.value).subscribe(
        (response) => {
          this.toastrService.success('Klinik başarıyla eklendi');
          this.router.navigate(['/admin-list-clinic']);
        },
        (error) => {
          this.toastrService.error('Klinik eklenirken bir hata oluştu.');
        }
      );
    } else {
      this.toastrService.error('Eksik alanları doldurunuz.');
    }
  }
}
