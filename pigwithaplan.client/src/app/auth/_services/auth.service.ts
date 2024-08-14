import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, Subject, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl: string = environment.apiUrl;
  private baseUrl: string = this.apiUrl + '/api/Auth';
  private apiLogin = '/Login';
  private apiRegister = '/Register';
  private apiLogout = '/Logout';
  private apiCheckAuthenticated = '/CheckToken';

  AuthSubject_Changed: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    const user = {
      username,
      password,
    };

    return this.http
      .post<boolean>(this.baseUrl + this.apiLogin, user, {
        withCredentials: true,
      })
      .pipe(
        tap((response: boolean) => {
          this.AuthSubject_Changed.next(response);
        })
      );
  }

  register(user: any): Observable<any> {
    return this.http.post(this.baseUrl + this.apiRegister, user);
  }

  logout(): Observable<any> {
    return this.http.post(this.baseUrl + this.apiLogout, null).pipe(
      tap(() => {
        this.AuthSubject_Changed.next(false);
      })
    );
  }

  isAuthenticated(): Observable<any> {
    return this.http.get(this.baseUrl + this.apiCheckAuthenticated, {
      withCredentials: true,
    });
  }

  loginGoogle(token: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/Auth/GoogleLogin`, { token });
  }
}
