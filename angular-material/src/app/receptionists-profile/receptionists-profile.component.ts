import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReceptionistDTO } from 'src/app/interfaces/receptionist-dto';
import { InfoForUsersService } from 'src/app/services/info-for-users.service';
import { SelectedReceptionistService } from 'src/app/services/selected-receptionist.service';

@Component({
  selector: 'app-receptionists-profile',
  templateUrl: './receptionists-profile.component.html',
  styleUrls: ['./receptionists-profile.component.scss']
})
export class ReceptionistsProfileComponent implements OnInit {

  constructor(
    private infoForUsersService: InfoForUsersService,
    private router: Router) { }

  receptionists: ReceptionistDTO[] = [];

  ngOnInit(): void {
    this.infoForUsersService.getAllReceptionists().subscribe(receptionists => {
      this.receptionists = receptionists;
      console.log("Receptionists", receptionists);
    });
  }

  goToMakePurchase(receptionist: ReceptionistDTO) {
    this.router.navigate(["make-purchase", receptionist.id]);
  }

}
