import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ReceptionistDTO } from '../interfaces/receptionist-dto';
import { SubscriptionDTO } from '../interfaces/subscription-dto';

@Injectable({
  providedIn: 'root'
})
export class InfoForUsersService {

  constructor(private http: HttpClient) { }

  private baseUrl: string = environment.baseUrl;

  private privateHttpHeaders = {
    headers: new HttpHeaders({
      observe: 'body',
      responseType: 'json'
    })
  };

  getAllReceptionists(): Observable<ReceptionistDTO[]>
  {
    return this.http.get(this.baseUrl + 'api/receptionists/get-all-receptionists')
    .pipe(map((response) => <ReceptionistDTO[]> response));
  }

  getAllSubscriptions(): Observable<SubscriptionDTO[]>
  {
    return this.http.get(this.baseUrl + 'api/subscriptions/get-all-subscriptions')
    .pipe(map((response) => <SubscriptionDTO[]> response));
  }

  getReceptionistById(id: number): Observable<ReceptionistDTO>
  {
    return this.http.get(this.baseUrl + 'api/receptionists/get-receptionist-by-id/' + id)
    .pipe(map((response) => <ReceptionistDTO> response));
  }
}
