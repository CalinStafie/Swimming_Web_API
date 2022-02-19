import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { ReceptionistDTO } from 'src/app/interfaces/receptionist-dto';
import { SelectedReceptionistService } from 'src/app/services/selected-receptionist.service';

@Component({
  selector: 'app-receptionist-info',
  templateUrl: './receptionist-info.component.html',
  styleUrls: ['./receptionist-info.component.scss']
})
export class ReceptionistInfoComponent implements OnInit, OnDestroy {

  @Input() receptionist: ReceptionistDTO = {
    id: -1,
    firstName: '',
    lastName: '',
    jobDescription: '',
    phoneNumber: ''
  };

  @Output() onMakePurchaseReceptionist: EventEmitter<ReceptionistDTO> = new EventEmitter<ReceptionistDTO>();

  constructor(private selectedReceptionistService: SelectedReceptionistService) { }

  selectedReceptionist: ReceptionistDTO = this.selectedReceptionistService.noReceptionist;

  subscription!: Subscription;

  ngOnInit(): void {
   this.subscription = this.selectedReceptionistService.currentSelectedReceptionist.subscribe((response: ReceptionistDTO) => {
      this.selectedReceptionist = response;
    });
  }

  selectReceptionist(receptionist: ReceptionistDTO)
  {
    this.selectedReceptionistService.changeReceptionist(receptionist);
  }

  isSelected(receptionist: ReceptionistDTO) {
    return this.selectedReceptionist == receptionist;
  }

  getImagePath(receptionist: ReceptionistDTO): string
  {
    return "assets/" + (receptionist.lastName + "_" + receptionist.firstName + "_Photo").toUpperCase() + ".jpg";
  }

  requestConsultation(receptionist: ReceptionistDTO){
    this.onMakePurchaseReceptionist.emit(receptionist);
  }

  ngOnDestroy(): void {
      this.subscription.unsubscribe();
  }

}
