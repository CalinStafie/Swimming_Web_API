import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { PurchaseDTO } from 'src/app/interfaces/purchase-dto';
import { PurchaseInformationModel } from 'src/app/interfaces/purchase-information-model';
import { PurchaseDataTableModel } from 'src/app/interfaces/purchase-data-table-model';
import { ReceptionistDTO } from 'src/app/interfaces/receptionist-dto';
import { ClientDTO } from 'src/app/interfaces/client-dto';
import { SubscriptionDTO } from 'src/app/interfaces/subscription-dto';
import { AdminInfoService } from 'src/app/services/admin-info.service';
import { InfoForUsersService } from 'src/app/services/info-for-users.service';
import { AddPurchaseComponent } from './add-purchase/add-purchase.component';
import { TimeIntervalComponent } from './time-interval/time-interval.component';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.scss']
})
export class PurchasesComponent implements OnInit {

  constructor(
    private infoForUsersService: InfoForUsersService,
    private adminInfoService: AdminInfoService,
    private liveAnnouncer: LiveAnnouncer,
    public dialog: MatDialog) { }

  displayedColumns: string[] = ['receptionistName', 'clientName', 'subscription', 'startTime', 'endTime', 'delete', 'editStartTime'];

  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  purchases: PurchaseDataTableModel[] = [];
  clients: ClientDTO[] = [];
  receptionists: ReceptionistDTO[] = [];
  subscriptions: SubscriptionDTO[] = [];

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this.liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this.liveAnnouncer.announce('Sorting cleared');
    }
  }

  delete(purchase: PurchaseDTO) {
    console.log(purchase);
    this.adminInfoService.deletePurchase(purchase).subscribe(response => {
      console.log("Delete purchase", response);
      this.purchases = this.purchases.filter(a => a != purchase);
      this.dataSource.data = this.purchases;
    });
  }

  editTime(purchase: PurchaseDTO) {
    console.log("Edit time", purchase);
    this.openNewTimeDialog(purchase);
  }

  ngOnInit(): void {

    this.adminInfoService.getAllClients().subscribe(clients => {
      this.clients = clients;
      console.log(clients);
    })

    this.infoForUsersService.getAllReceptionists().subscribe(receptionists => {
      this.receptionists = receptionists;
      console.log(receptionists);
    })

    this.infoForUsersService.getAllSubscriptions().subscribe(subscriptions => {
      this.subscriptions = subscriptions;
      console.log(subscriptions);
    })

    this.adminInfoService.getAllPurchases().subscribe(purchases => {
        this.purchases = purchases.map(function (purchase): PurchaseDataTableModel {
          return {
            clientId: purchase.clientDTO.id,
            receptionistId: purchase.receptionistDTO.id,
            subscriptionId: purchase.subscriptionDTO.id,
            clientName: purchase.clientDTO.lastName + " " + purchase.clientDTO.firstName,
            receptionistName: purchase.receptionistDTO.lastName + " " + purchase.receptionistDTO.firstName,
            subscriptionName: purchase.subscriptionDTO.subscriptionName,
            startTime: purchase.startTime,
            endTime: purchase.endTime,
          }
        });
        this.dataSource.data = this.purchases;
        console.log("Purchases", this.purchases);
      });
    
    }


  addPurchase() {
    this.openAddPurchaseDialog();
  }

  openAddPurchaseDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.data = {
      receptionists: this.receptionists,
      clients: this.clients,
      subscriptions: this.subscriptions
    };

    const dialogRef = this.dialog.open(AddPurchaseComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          console.log("Dialog output:", data);
          var addedPurchase: PurchaseDataTableModel = 
          {
            receptionistName: data.receptionistDTO.lastName + " " + data.receptionistDTO.firstName,
            receptionistId: data.receptionistDTO.id,
            clientName: data.clientDTO.lastName + " " + data.clientDTO.firstName,
            clientId: data.clientDTO.id,
            subscriptionName: data.subscriptionDTO.subscriptionName,
            subscriptionId: data.subscriptionDTO.id,
            startTime: data.startTime,
            endTime: data.endTime,
          }
          this.purchases.push(addedPurchase);
          this.dataSource.data = this.purchases;
        }
      }
    );
  }

  openNewTimeDialog(purchase: PurchaseDTO) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.data = {
      purchase: purchase
    };

    const dialogRef = this.dialog.open(TimeIntervalComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          console.log("Dialog output:", data);
          var purchaseToUpdate = this.purchases.filter(a => a == purchase)[0];////
          console.log("Update purchase time", purchaseToUpdate);
          purchaseToUpdate['startTime'] = data['startTime'];
          purchaseToUpdate['endTime'] = data['endTime'];
        }
      }
    );
  }

  isExpired(row: PurchaseDataTableModel)
  {
    return new Date(row.endTime) < new Date();
  }

}
