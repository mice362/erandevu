import { Component } from '@angular/core';
import { BasicLayoutComponent } from '../../../shared/components/basic-layout/basic-layout.component';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/auth/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-verify-email',
  standalone: true,
  imports: [BasicLayoutComponent],
  templateUrl: './verify-email.component.html',
  styleUrl: './verify-email.component.scss'
})
export class VerifyEmailComponent {
  activationKey: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private toastrService:ToastrService
  ) { }

  ngOnInit(): void {
    // ActivatedRoute üzerinden 'ActivationKey' parametresini al
    this.route.queryParams.subscribe(params => {
      this.activationKey = params['ActivationKey'];
   
    });
  }

  validate() {
    // AuthService üzerinden verifyEmail fonksiyonunu çağırma
    this.authService.verifyEmail(this.activationKey)
      .subscribe(
        (response) => {
         this.toastrService.success("Hesabınız başarıyla doğrulandı!")
         this.router.navigate(['/login'])
        },
        (responseError) => {
          this.toastrService.error(responseError.error.Detail, 'Hata');
          this.router.navigate(['/register'])
        }
      )
  }
}
