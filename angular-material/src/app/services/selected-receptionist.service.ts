import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ReceptionistDTO } from '../interfaces/receptionist-dto';

@Injectable({
  providedIn: 'root'
})
export class SelectedReceptionistService {

  constructor() { }

  public noReceptionist: ReceptionistDTO = {
    id: -1,
    firstName: "",
    lastName: "",
    jobDescription: "",
    phoneNumber: ""
  }

  private selectedReceptionistSource = new BehaviorSubject<ReceptionistDTO>(this.noReceptionist);

  public changeReceptionist(receptionist: ReceptionistDTO) {
    this.selectedReceptionistSource.next(receptionist);
  }

  public currentSelectedReceptionist: Observable<ReceptionistDTO> = this.selectedReceptionistSource.asObservable();
}
