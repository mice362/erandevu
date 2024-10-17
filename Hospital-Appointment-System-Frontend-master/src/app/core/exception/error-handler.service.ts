import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class ErrorHandlerService {
  private recentErrors = new Set<string>();
  private errorExpiryTime = 5000; // 5 seconds

  constructor(private toastrService: ToastrService) {}

  handleHttpError(error: HttpErrorResponse): void {
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      console.error('Client-side error occurred:', error.error.message);
    } else {
      // Server-side error
      if (error.status === 400) {
        // Handle specific HTTP error codes
        if (error.error.Errors && error.error.Errors.length > 0) {
          for (let i = 0; i < error.error.Errors.length; i++) {
            const propertyErrors = error.error.Errors[i].Errors;
            for (let j = 0; j < propertyErrors.length; j++) {
              this.showErrorMessage(propertyErrors[j], 'Doğrulama Hatası');
            }
          }
        } else {
          this.showErrorMessage(error.error.detail, 'Hatalı İşlem');
        }
      } else {
        this.showErrorMessage('Bilinmeyen bir hata oluştu', 'Hata');
      }
    }
  }

  private showErrorMessage(message: string, title: string) {
    if (!this.recentErrors.has(message)) {
      this.toastrService.error(message, title);
      this.recentErrors.add(message);
      setTimeout(() => this.recentErrors.delete(message), this.errorExpiryTime);
    }
  }
}
