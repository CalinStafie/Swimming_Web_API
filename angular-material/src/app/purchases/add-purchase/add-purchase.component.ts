import { formatDate } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { PurchaseDTO } from 'src/app/interfaces/purchase-dto';
import { PurchasePostModel } from 'src/app/interfaces/purchase-post-model';
import { ReceptionistDTO } from 'src/app/interfaces/receptionist-dto';
import { ClientDTO } from 'src/app/interfaces/client-dto';
import { SubscriptionDTO } from 'src/app/interfaces/subscription-dto';
import { AdminInfoService } from 'src/app/services/admin-info.service';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.scss']
})
export class AddPurchaseComponent implements OnInit {

  startWithIdRegx = "(^#[0-9]{1,}:[a-zA-Z ]*)";

  form = new FormGroup({
    receptionist: new FormControl('', [Validators.required, Validators.pattern(this.startWithIdRegx)]),
    client: new FormControl('', [Validators.required, Validators.pattern(this.startWithIdRegx)]),
    subscription: new FormControl('', [Validators.required, Validators.pattern(this.startWithIdRegx)]),
    startTime: new FormControl('12:00', [Validators.required]),
    endTime: new FormControl('13:00', [Validators.required]),
  }, {});

  clients: ClientDTO[] = [];
  subscriptions: SubscriptionDTO[] = [];
  receptionists: ReceptionistDTO[] = [];

  filteredReceptionists!: Observable<ReceptionistDTO[]>;
  filteredClients!: Observable<ClientDTO[]>;
  filteredSubscriptions!: Observable<SubscriptionDTO[]>;

  constructor(
    private dialogRef: MatDialogRef<AddPurchaseComponent>,
    private adminInfoService: AdminInfoService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.clients = data.clients;
    this.subscriptions = data.subscriptions;
    this.receptionists = data.receptionists;
  }

  findReceptionist(val: string) {
    return this.receptionists.filter(x => ("#" + x.id + ": " + x.lastName + " " + x.firstName).toLowerCase().includes(val.toLowerCase()));
  }

  findClient(val: string) {
    return this.clients.filter(x => ("#" + x.id + ": " + x.lastName + " " + x.firstName).toLowerCase().includes(val.toLowerCase()));
  }

  findSubscription(val: string) {
    return this.subscriptions.filter(x => ("#" + x.id + ": " + x.subscriptionName).toLowerCase().includes(val.toLowerCase()));
  }


  ngOnInit(): void {
    this.filteredReceptionists = this.form.controls['receptionist'].valueChanges.pipe(startWith(''), map(term => this.findReceptionist(term)));
    this.filteredClients = this.form.controls['client'].valueChanges.pipe(startWith(''), map(term => this.findClient(term)));
    this.filteredSubscriptions = this.form.controls['subscription'].valueChanges.pipe(startWith(''), map(term => this.findSubscription(term)));
    console.log(this.form.controls['client']);
  }

  save() {
    if (this.form.valid) {
      console.log(this.form.value);
      var client = this.form.controls['client'].value;
      var subscription = this.form.controls['subscription'].value;
      var receptionist = this.form.controls['receptionist'].value;

      var purchase: PurchasePostModel =
      {
        clientId: parseInt(client.substr(1, client.indexOf(':'))),
        subscriptionId: parseInt(subscription.substr(1, subscription.indexOf(':'))),
        receptionistId: parseInt(receptionist.substr(1, receptionist.indexOf(':'))),
        startTime: this.form.controls['startTime'].value,
        endTime: this.form.controls['endTime'].value,
      }
      this.adminInfoService.addPurchase(purchase).subscribe((response: any) => {
        console.log("Purchase Information", response);
        this.dialogRef.close(response);
      })
    }
  }

  close() {
    this.dialogRef.close();
  }

}
