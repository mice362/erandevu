import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

import { Doctor } from '../../models/doctor';
import { AuthService } from '../../../../../core/auth/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { NavbarComponent } from '../../../../../shared/components/navbar/navbar.component';
import { TokenService } from '../../../../../core/auth/services/token.service';
import { DoctorService } from '../../services/doctor.service';

@Component({
  selector: 'app-doctor-sidebar',
  standalone: true,
  templateUrl: './doctorSidebar.component.html',
  styleUrl: './doctorSidebar.component.scss',
  imports: [CommonModule, RouterModule, NavbarComponent],
})
export class DoctorSidebarComponent implements OnInit {
  doctor: Doctor;
  doctorName: string = '';
  doctorTitle: string = '';
  doctorBranch: string = '';

  errorMessage: string;

  constructor(
    private doctorService: DoctorService,
    private authService: AuthService,
    private toastrService: ToastrService,
    private router: Router,
    private tokenService: TokenService
  ) {}

  ngOnInit(): void {
    this.doctorService.getDoctorProfile().subscribe(
      (doctor) => {
        this.doctor = doctor;
        this.doctorTitle = doctor.title;
        this.doctorName = doctor.firstName + ' ' + doctor.lastName;
        this.doctorBranch = doctor.branchName;
        const tokenExpirationDate = this.tokenService.getTokenExpirationDate();
        if (tokenExpirationDate) {
          const currentTime = new Date();
          const timeDifference =
            tokenExpirationDate.getTime() - currentTime.getTime();

          const minutesUntilExpiration = Math.floor(
            timeDifference / (1000 * 60)
          );
          const secondsUntilExpiration = Math.floor(
            (timeDifference % (1000 * 60)) / 1000
          );
        }
      }
    );
  }

  logout(): void {
    this.authService.logout();
    this.toastrService.success(
      'Başarıyla çıkış yaptınız. Giriş sayfasına yönlendiriliyorsunuz',
      'Başarılı'
    );
    this.router.navigate(['/']);
  }
}
