import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': sessionStorage.getItem("token") ? sessionStorage.getItem("token") : ''

    })
  };
  constructor(private http: HttpClient) { }
  get(path) {
    return this.http.get(`api/${path}`, this.httpOptions);
  }

  insert(path, repo: any) {
    return this.http.post(`api/${path}`, repo, this.httpOptions).pipe(
      catchError((error: any) => {

        if (error.status == 404) {
          sessionStorage.clear();
        }
        return throwError(error);
      })
    );
  }
  getAllReps() {
    return this.http.get('https://api.github.com/search/repositories?q=stars:>=10000');
  }

  findReps(rep) {
    return this.http.get(`https://api.github.com/search/repositories?q=${rep}`)
  }
}