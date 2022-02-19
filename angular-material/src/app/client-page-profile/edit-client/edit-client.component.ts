import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ClientDTO } from 'src/app/interfaces/client-dto';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-edit-client',
  templateUrl: './edit-client.component.html',
  styleUrls: ['./edit-client.component.scss']
})
export class EditClientComponent implements OnInit {

  public clientData!: ClientDTO;

  form = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    phoneNumber: new FormControl('', []),
    dateOfBirth: new FormControl('', [Validators.required]),
  }, {});


  constructor(
    private dialogRef: MatDialogRef<EditClientComponent>,
    private clientService: ClientService,
    @Inject(MAT_DIALOG_DATA) data) {
      this.clientData = data.clientData;
  }

  ngOnInit(): void {
    this.form.setValue({
      'firstName': this.clientData.firstName,
      'lastName': this.clientData.lastName,
      'phoneNumber': this.clientData.phoneNumber,
      'dateOfBirth': this.clientData.dateOfBirth,
    })
  }

  save() {
    if (this.form.valid) {
      var updatedClient: ClientDTO = {
        id: this.clientData.id,
        firstName: this.form.controls['firstName'].value,
        lastName: this.form.controls['lastName'].value,
        phoneNumber: this.form.controls['phoneNumber'].value,
        dateOfBirth: this.form.controls['dateOfBirth'].value,
        userId: this.clientData.userId
      };
      this.clientService.updateClientData(this.clientData.id, updatedClient).subscribe((response: any) => {
        this.dialogRef.close(response);
      })
    }
  }

  close() {
    this.dialogRef.close();
  }

}
