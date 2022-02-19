import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientPurchaseModel } from 'src/app/interfaces/client-purchase';
import { ClientDTO } from 'src/app/interfaces/client-dto';
import { AuthService } from 'src/app/services/auth.service';
import { ClientService } from 'src/app/services/client.service';
import { EditClientComponent } from './edit-client/edit-client.component';

@Component({
  selector: 'app-client-page-profile',
  templateUrl: './client-page-profile.component.html',
  styleUrls: ['./client-page-profile.component.scss']
})
export class ClientPageProfileComponent implements OnInit {

  private id!: number;

  public clientData!: ClientDTO;

  public clientPurchases: ClientPurchaseModel[] = [];

  constructor(
    private activatedRoute: ActivatedRoute, 
    private clientService: ClientService,
    private authService: AuthService,
    private router: Router,
    private dialog: MatDialog) {}


    isUpcoming(purchase: ClientPurchaseModel){

      var currentTime = new Date().getTime();
      var startTime = new Date(purchase.startTime).getTime();
      
      if (startTime > currentTime) {
        return true;
      }
      return false;
    }
    
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: any) => {
      console.log("Route params", params);
      this.id = params['id'];
      this.clientService.getClientData(this.id).subscribe((response: any) => {
        console.log("Client data", response);
        this.clientData = response;
        this.clientService.getClientPurchases(this.clientData.id).subscribe((purchases: any) => {
          console.log("Client purchases", purchases);
          this.clientPurchases = purchases;
        })
      })

    });
  }

  edit():void {
    this.openEditClientDataDialog();
  }

  openEditClientDataDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.data = {
      clientData: this.clientData
    };

    const dialogRef = this.dialog.open(EditClientComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          console.log("Dialog output:", data);
          this.clientData = data;
        }
      }
    );
  }


  logout(): void {
    this.authService.logout();
    this.router.navigate([""]);
  }


}
