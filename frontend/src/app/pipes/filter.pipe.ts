import { Pipe, PipeTransform } from '@angular/core';
import { ClientViewDTO } from '../models/client/client-view-dto';
import { TrainerViewDTO } from '../models/trainer/trainer-view-dto';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {
  transform(items: (ClientViewDTO | TrainerViewDTO)[], searchText: string): any[] {
    if (!items) return [];
    if (searchText == "" || !searchText) return items;

    searchText = searchText.toLowerCase();

    return items.filter(it => {
      return it.firstName.toLowerCase().includes(searchText) || it.lastName.toLowerCase().includes(searchText) ;
    });
  }
}