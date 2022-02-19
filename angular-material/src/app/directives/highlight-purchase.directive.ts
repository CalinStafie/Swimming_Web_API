import { Directive, ElementRef, HostListener, Input, OnInit } from '@angular/core';
import { PurchaseDTO } from '../interfaces/purchase-dto';
import { PurchaseDataTableModel } from '../interfaces/purchase-data-table-model';
import { ClientPurchaseModel } from '../interfaces/client-purchase';

@Directive({
  selector: 'appHighlightPurchase[purchase]'
})
export class HighlightPurchaseDirective implements OnInit{

  @Input() purchase!: PurchaseDTO | PurchaseDataTableModel | ClientPurchaseModel;

  pastColor = "red";
  stillAvalaibleColor = "green";

  constructor(private elr:ElementRef) { }

  @HostListener('mouseenter') onMouseEnter() {
    var currentTime = new Date().getTime();
    var endTime = new Date(this.purchase.endTime).getTime();
    console.log(currentTime);
    console.log(endTime);
    if (endTime < currentTime) {
      this.elr.nativeElement.style.background = this.pastColor;
      console.log("pastColor");
      return;
    }
    else {
      this.elr.nativeElement.style.background = this.stillAvalaibleColor;
      console.log("stillAvalaibleColor");
      return;
    }
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.elr.nativeElement.style.background = "";
  }
  
    ngOnInit(): void {
      
    }
}
