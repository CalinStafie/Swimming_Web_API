import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ClientDTO } from '../interfaces/client-dto';
import { PurchaseDataTableModel } from '../interfaces/purchase-data-table-model';
import { ReceptionistDTO } from '../interfaces/receptionist-dto';
import { SubscriptionDTO } from '../interfaces/subscription-dto';
import { AdminInfoService } from '../services/admin-info.service';
import { AuthService } from '../services/auth.service';
import { InfoForUsersService } from '../services/info-for-users.service';

@Component({
  selector: 'app-receptionist-page',
  templateUrl: './receptionist-page.component.html',
  styleUrls: ['./receptionist-page.component.scss']
})
export class ReceptionistPageComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }
  
  ngOnInit(): void {  
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate([""]);
  }
}
