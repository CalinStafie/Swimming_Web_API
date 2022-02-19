import { Pipe, PipeTransform } from '@angular/core';
import { ReceptionistDTO } from '../interfaces/receptionist-dto';
import { ClientDTO } from '../interfaces/client-dto';

@Pipe({
  name: 'fullName'
})
export class FullNamePipe implements PipeTransform {

  transform(person: ReceptionistDTO | ClientDTO): string {
    return person?.lastName + " " + person?.firstName;
  }

}
