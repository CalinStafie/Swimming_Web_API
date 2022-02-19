import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AdminInfoService } from 'src/app/services/admin-info.service';
@Component({
  selector: 'app-add-subscription',
  templateUrl: './add-subscription.component.html',
  styleUrls: ['./add-subscription.component.scss']
})
export class AddSubscriptionComponent implements OnInit {

  numbersOnlyRegx = "^[0-9]*";

  form = new FormGroup({
    subscriptionName: new FormControl('', [Validators.required]),
    cost: new FormControl('', [Validators.required, Validators.pattern(this.numbersOnlyRegx)]),
  }, {});

  constructor(
    private dialogRef: MatDialogRef<AddSubscriptionComponent>,
    private adminInfoService: AdminInfoService,
    @Inject(MAT_DIALOG_DATA) data) {
  }

  ngOnInit(): void {
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
  
  close() {
    this.dialogRef.close();
  }
}
