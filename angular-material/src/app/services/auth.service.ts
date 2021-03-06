import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthModel } from '../interfaces/auth-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ClientRegistrationModel } from '../interfaces/client-registration';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = environment.baseUrl;

  private privateHttpHeaders = {
    headers: new HttpHeaders({
      'content-type': 'application/json'
    })
  };

  private registerPrivateHttpHeaders = {
    headers: new HttpHeaders({
      'content-type': 'application/json',
      'response-type': 'text'
    })
  };
  
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  login(authModel: AuthModel) {
    return this.http.post(
      this.baseUrl + 'api/auth/login',
      authModel,
      this.privateHttpHeaders
    );
  }

  logout() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("role");
  }

  // You can only register as a client from the website
  registerAsClient(clientRegistrationModel: ClientRegistrationModel) {
    return this.http.post(
      this.baseUrl + 'api/auth/register-as-client',
      clientRegistrationModel
    );
  }

  refreshToken() {
    var currentToken = {
      "accessToken": localStorage.getItem('accessToken'),
      "refreshToken": localStorage.getItem('refreshToken')
    };

    return this.http.post(
      this.baseUrl + 'api/auth/refresh',
      currentToken,
      this.privateHttpHeaders
    );
  }

  getRole(): string {
    const role = localStorage.getItem('role')
    if (role != null) {
      return role;
    }
    return "";
  }

  getUserId(): number | null {
    var token = localStorage.getItem('accessToken');
    if (token === null) {
      return null;
    }
    var userId = this.jwtHelper.decodeToken(token)['nameid'];
    if (userId === null) {
      return null;
    }
    return parseInt(userId);
  }

  isLoggedIn(): boolean {
    return localStorage['accessToken'] && localStorage['refreshToken'] && localStorage['role'];
  }

  isTokenExpired(token: string): boolean {
    return this.jwtHelper.isTokenExpired(token);
  }
}
