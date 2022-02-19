import { ResourceLoader } from '@angular/compiler';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ReceptionistDTO } from 'src/app/interfaces/receptionist-dto';
import { InfoForUsersService } from 'src/app/services/info-for-users.service';
import { SelectedReceptionistService } from 'src/app/services/selected-receptionist.service';

@Component({
  selector: 'app-make-purchase',
  templateUrl: './make-purchase.component.html',
  styleUrls: ['./make-purchase.component.scss']
})
export class MakePurchaseComponent implements OnInit, OnDestroy {

  constructor(
    private selectedReceptionistService: SelectedReceptionistService, 
    private activatedRoute: ActivatedRoute,
    private infoForUsersService: InfoForUsersService) { }

  receptionist: ReceptionistDTO = this.selectedReceptionistService.noReceptionist;

  receptionistId!: number;

  subscription_obs!: Subscription;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: any) => {
      console.log("Route params", params);
      this.receptionistId = params['id'];
      this.subscription_obs = this.selectedReceptionistService.currentSelectedReceptionist.subscribe((response: any) => {
        this.receptionist = response;
        console.log(this.receptionist);
        if (this.receptionist.id != this.receptionistId) {
          this.infoForUsersService.getReceptionistById(this.receptionistId).subscribe((paramsReceptionist: any) => {
            this.receptionist = paramsReceptionist;
            this.selectedReceptionistService.changeReceptionist(this.receptionist);
            console.log(this.receptionist.phoneNumber);
          })
        }
      });
    });
  }

  alert() {
    window.alert('Receptionerul a fost instiintat! Vei primi mesaj in scurt timp de la el pentru a discuta detaliile abonamentului!!');
    window.location.reload();
  }
  ngOnDestroy(): void {
      this.subscription_obs.unsubscribe();
  }

}
