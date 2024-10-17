import { Pipe, PipeTransform } from '@angular/core';
import { Branch } from '../branches/models/branch';

@Pipe({
  name: 'filterBranchName',
  standalone: true
})
export class FilterBranchNamePipe implements PipeTransform {

  transform(value: Branch[], filterText: string): Branch[] {
    filterText = filterText ? filterText.toLocaleLowerCase() : "";
    return filterText
      ? value.filter(
          (b: Branch) =>
            b.name.toLocaleLowerCase().indexOf(filterText) !== -1
        )
      : value; // Eğer filtre metni yoksa, tüm branşları döndür
  }

}
