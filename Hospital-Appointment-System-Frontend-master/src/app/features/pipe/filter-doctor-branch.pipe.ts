import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterDoctorBranch',
  standalone: true
})
export class FilterDoctorBranchPipe implements PipeTransform {

  transform(doctors: any[], branchName: string): any[] {
    if (!doctors || !branchName) {
      return doctors;
    }
    return doctors.filter(doctor =>
      doctor.branchName === branchName
    );
  }
  
}
