import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, map, Observable, Subject, tap, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { IBudget } from '../_models/budget';
import { ICategoryGroup } from '../_models/category-group';

@Injectable({
  providedIn: 'root',
})
export class CategoryGroupService implements OnInit {
  constructor(private http: HttpClient) {}

  private baseUrl = environment.apiUrl;
  private apiUrl = this.baseUrl + '/api/CategoryGroup';

  CategoryGroupData_changed: Subject<any> = new Subject<any>();

  ngOnInit(): void {}

  getAll(): Observable<IBudget[]> {
    return this.http.get<IBudget[]>(this.apiUrl);
  }

  add(categoryGroup: ICategoryGroup): Observable<any> {
    const httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http
      .post<ICategoryGroup>(this.apiUrl, categoryGroup, {
        headers: httpHeaders,
        observe: 'response',
      })
      .pipe(
        map((response) => response.body),
        tap((response) => {
          if (response) this.CategoryGroupData_changed.next(response.id);

          return true;
        }),
        catchError((err) => {
          console.log('An error occurred:', err);

          return throwError(() => new Error('Failed to create category group'));
        })
      );
  }
}
