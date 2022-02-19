import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PurchaseDTO } from 'src/app/interfaces/purchase-dto';
import { AdminInfoService } from 'src/app/services/admin-info.service';
@Component({
  selector: 'app-time-interval',
  templateUrl: './time-interval.component.html',
  styleUrls: ['./time-interval.component.scss']
})
export class TimeIntervalComponent implements OnInit {

  form = new FormGroup({
    newDate: new FormControl(new Date(), [Validators.required]),
    newTime: new FormControl('12:00', [Validators.required]),
  }, {});

  purchase!: PurchaseDTO;

  constructor(
      private dialogRef: MatDialogRef<TimeIntervalComponent>,
      private adminInfoService: AdminInfoService,
      @Inject(MAT_DIALOG_DATA) data){
        this.purchase = data.purchase;
      }

  ngOnInit(): void {
  }

  save() {      
      if (this.form.valid) {
        this.adminInfoService.updatePurchaseTime(this.purchase, this.form.value['newDate'], this.form.value['newTime']).subscribe(response => {
          console.log(response);
          this.dialogRef.close(response);
        })
      }
  }

  close() {
      this.dialogRef.close();
  }

}
