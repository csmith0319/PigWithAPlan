import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BudgetService implements OnInit {
  constructor(private http: HttpClient) {}

  private baseUrl = environment.apiUrl;
  private apiUrl = this.baseUrl + '/api/Budget';

  ngOnInit(): void {}

  getAll(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
