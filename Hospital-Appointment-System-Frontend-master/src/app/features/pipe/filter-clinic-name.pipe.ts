import { Pipe, PipeTransform } from '@angular/core';
import { Clinic } from '../clinics/models/clinic';

@Pipe({
  name: 'filterClinicName',
  standalone: true
})
export class FilterClinicNamePipe implements PipeTransform {

  transform(value: Clinic[], filterText: string): Clinic[] {
    filterText = filterText ? filterText.toLocaleLowerCase() : "";
    return filterText
      ? value.filter(
          (d: Clinic) =>
            d.name.toLocaleLowerCase().indexOf(filterText) !== -1
        )
      : value; // Eğer filtre metni yoksa, tüm klinikleri döndür
  }

}
