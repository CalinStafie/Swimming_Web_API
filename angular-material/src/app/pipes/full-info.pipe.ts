import { Pipe, PipeTransform } from '@angular/core';
import { ClientDTO } from '../interfaces/client-dto';
import { ReceptionistDTO } from '../interfaces/receptionist-dto';

@Pipe({
  name: 'fullInfo'
})
export class FullInfoPipe implements PipeTransform {

  transform(person: ReceptionistDTO): string {
    if(person.phoneNumber === undefined)
    {
      return "Receptionerul " + person?.lastName + " " + person?.firstName + " are jobul de a " + person?.jobDescription.toLowerCase();
    }

    return "Receptionerul " + person?.lastName + " " + person?.firstName + ", cu numarul de telefon "
          + person?.phoneNumber + " are jobul de a " + person?.jobDescription.toLowerCase();
  }


}
