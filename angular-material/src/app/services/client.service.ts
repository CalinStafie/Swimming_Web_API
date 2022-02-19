import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ClientPurchaseModel } from '../interfaces/client-purchase';
import { ClientDTO } from '../interfaces/client-dto';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  getClientData(id: number): Observable<ClientDTO> {
    return this.http.get(environment.baseUrl + "api/clients/get-user-data/" + id)
    .pipe(map((response) => <ClientDTO>(response)));
  }

  getClientPurchases(id: number): Observable<any> {
    return this.http.get(environment.baseUrl + "api/purchases/get-client-purchases/" + id)
    .pipe(map((response) =>  <ClientPurchaseModel[]>response))
  }

  updateClientData(id: number, updatedClient: ClientDTO): Observable<any> {
    return this.http.put(environment.baseUrl + "api/clients/update-client/" + id, updatedClient);
  }
}
