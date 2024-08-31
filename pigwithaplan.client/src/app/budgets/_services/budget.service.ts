import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, map, Observable, Subject, tap, throwError } from 'rxjs';
import { environment } from 'environments/environment';
import { IBudget } from 'app/_models/budget';

@Injectable({
  providedIn: 'root',
})
export class BudgetService implements OnInit {
  constructor(private http: HttpClient) {}

  private baseUrl = environment.apiUrl;
  private apiUrl = this.baseUrl + '/api/Budget';
  private apiFavorite = '/Favorite';

  BudgetData_changed: Subject<any> = new Subject<any>();

  ngOnInit(): void {}

  getAll(): Observable<IBudget[]> {
    return this.http.get<IBudget[]>(this.apiUrl);
  }

  add(budget: IBudget): Observable<any> {
    const httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http
      .post<IBudget>(this.apiUrl, budget, {
        headers: httpHeaders,
        observe: 'response',
      })
      .pipe(
        map((response) => response.body),
        tap((response) => {
          if (response) this.BudgetData_changed.next(response.id);

          return true;
        }),
        catchError((err) => {
          console.log('An error occurred:', err);

          return throwError(() => new Error('Failed to create budget'));
        })
      );
  }

  favorite(id: number): Observable<any> {
    const httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let httpParams = new HttpParams();
    httpParams = httpParams.set('id', id.toString());

    return this.http
      .post(this.apiUrl + this.apiFavorite, null, {
        headers: httpHeaders,
        params: httpParams,
      })
      .pipe(
        tap((response) => {
          if (response) this.BudgetData_changed.next(true);

          return true;
        }),
        catchError((err) => {
          console.log('An error occurred:', err);

          return throwError(() => new Error('Failed to favorite budget'));
        })
      );
  }
}
