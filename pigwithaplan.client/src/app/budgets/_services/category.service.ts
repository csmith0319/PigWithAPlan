import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICategoryGroup } from 'app/_models/category-group';
import { environment } from 'environments/environment';
import { catchError, map, Observable, Subject, tap, throwError } from 'rxjs';
import { ICategory } from '../_models/Category';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  private baseUrl = environment.apiUrl;
  private apiUrl = this.baseUrl + '/api/Category';

  CategoryData_changed: Subject<any> = new Subject<any>();

  ngOnInit(): void {}

  getAll(id: number): Observable<ICategory[]> {
    const apiKey: string = 'groupId';
    const httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let httpParams = new HttpParams();
    httpParams = httpParams.set(apiKey, id.toString());

    return this.http.get<ICategory[]>(this.apiUrl, {
      headers: httpHeaders,
      params: httpParams,
    });
  }

  add(category: ICategory): Observable<any> {
    const httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http
      .post<ICategory>(this.apiUrl, category, {
        headers: httpHeaders,
        observe: 'response',
      })
      .pipe(
        map((response) => response.body),
        tap((response) => {
          if (response) this.CategoryData_changed.next(response.id);

          return true;
        }),
        catchError((err) => {
          console.log('An error occurred:', err);

          return throwError(() => new Error('Failed to create category'));
        })
      );
  }
}
