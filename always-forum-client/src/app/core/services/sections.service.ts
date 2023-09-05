import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment.development";
import {Observable} from "rxjs";
import {GetSectionsResponseModel} from "../../sections/getSectionsResponse.model";

@Injectable({
  providedIn: 'root'
})
export class SectionsService {
  private readonly sectionsUrl = environment.apiUrl + 'sections/';

  constructor(private httpClient: HttpClient) { }

  getSections(): Observable<GetSectionsResponseModel>  {
    return this.httpClient.get<GetSectionsResponseModel>(this.sectionsUrl);
  }
}
