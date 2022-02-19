import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { SubscriptionDTO } from 'src/app/interfaces/subscription-dto';
import { AdminInfoService } from 'src/app/services/admin-info.service';
import { AuthService } from 'src/app/services/auth.service';
import { InfoForUsersService } from 'src/app/services/info-for-users.service';
import { AddSubscriptionComponent } from '../add-subscription/add-subscription.component';

@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.scss']
})
export class SubscriptionsComponent implements OnInit {

  currency = "RON";

  role = "";

  subscriptions: SubscriptionDTO[] = [];

  constructor(
    public infoForUsersService: InfoForUsersService,
     public authService: AuthService,
     public adminInfoService: AdminInfoService,
     public dialog: MatDialog) { }

  ngOnInit(): void {
    this.infoForUsersService.getAllSubscriptions().subscribe((response: any) => {
      this.subscriptions = response;
      console.log("Subscriptions", this.subscriptions);
    });
    this.role = this.authService.getRole();
  }

  addSubscription() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.data = {
    };

    const dialogRef = this.dialog.open(AddSubscriptionComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          console.log("Dialog output:", data);
          this.adminInfoService.addSubscription(data).subscribe((subscription: any) => {
            this.subscriptions.push(subscription);
          });
        }
      }
    );
  }

  delete(subscriptionId: number) {
    this.adminInfoService.deleteSubscription(subscriptionId).subscribe((response: any) => {
      if (response === true) {
        this.subscriptions = this.subscriptions.filter(x => x.id != subscriptionId)
      }
    })
  }

  updateCurrency(event: MatSlideToggleChange) {
    if (event.checked == true) {
      this.currency = "EUR";
    } else {
      this.currency = "RON";
    }
    console.log("Currency", this.currency);
  }

}
