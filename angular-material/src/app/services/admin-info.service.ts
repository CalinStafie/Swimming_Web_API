import { formatDate } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PurchaseDTO } from '../interfaces/purchase-dto';
import { PurchaseInformationModel } from '../interfaces/purchase-information-model';
import { PurchasePostModel } from '../interfaces/purchase-post-model';
import { ClientDTO } from '../interfaces/client-dto';
import { SubscriptionDTO } from '../interfaces/subscription-dto';

@Injectable({
  providedIn: 'root'
})
export class AdminInfoService {

  constructor(private http: HttpClient) { }

  private privateHttpHeaders = new HttpHeaders({
    'content-type': 'application/json'
  });

  private baseUrl: string = environment.baseUrl;

  getAllPurchases(): Observable<PurchaseInformationModel[]> {
    return this.http.get(this.baseUrl + 'api/purchases/get-all-purchases')
      .pipe(map((response) => <PurchaseInformationModel[]>response));
  }

  getAllClients(): Observable<ClientDTO[]> {
    return this.http.get(this.baseUrl + 'api/clients/get-all-clients')
      .pipe(map((response) => <ClientDTO[]>response));
  }

  deletePurchase(purchase: PurchaseDTO) {
    console.log(purchase);
    const options = {
      headers: this.privateHttpHeaders,
      body: purchase,
    };
    return this.http.delete(this.baseUrl + 'api/purchases/delete-purchase', options);
  }

  updatePurchaseTime(purchase: PurchaseDTO, newDate: Date, newTime: String) {
    var formattedDate = formatDate(newDate, "yyyy-MM-dd", "en-US");
    var body = {
      "clientId": purchase.clientId,
      "receptionistId": purchase.receptionistId,
      "subscriptionId": purchase.subscriptionId,
      "startTime": purchase.startTime,
      "endTime": purchase.endTime,
      "newStartTime": formattedDate + "T" + newTime
    };
    console.log(body);
    return this.http.put(this.baseUrl + 'api/purchases/update-purchase-time', body);
  }

  addPurchase(purchase: PurchasePostModel) {
    console.log("POST Purchase", purchase);
    return this.http.post(this.baseUrl + 'api/purchases/insert-purchase', purchase);
  }

  getPurchaseInfo(purchase: PurchaseDTO) {
    return this.http.post(this.baseUrl + 'api/purchases/get-purchase-information', purchase);
  }

  addSubscription(subscription: SubscriptionDTO) {
    return this.http.post(this.baseUrl + 'api/subscriptions/insert-subscription', subscription);
  }

  deleteSubscription(id: number) {
    return this.http.delete(this.baseUrl + 'api/subscriptions/delete-subscription/' + id);
  }
}
