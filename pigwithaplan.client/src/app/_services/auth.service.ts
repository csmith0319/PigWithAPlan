import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, Subject, tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl: string = environment.apiUrl;
  private baseUrl: string = this.apiUrl + '/api/Auth';
  private apiLogin = '/Login';
  private apiRegister = '/Register';
  private apiCheckToken = '/CheckToken';

  isLoggedIn = false;

  AuthSubject_Changed: Subject<boolean> = new Subject();

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

  register(username: string, email: string, password: string): Observable<any> {
    return this.http.post(this.baseUrl + this.apiLogin, {
      username,
      email,
      password,
    });
  }

  checkToken(): Observable<any> {
    return this.http.get(this.baseUrl + this.apiCheckToken, {
      withCredentials: true,
    });
  }

  googleLogin(token: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/Auth/GoogleLogin`, { token });
  }
}
