import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment.development";
import {HttpClient} from "@angular/common/http";
import {GetPostsModel} from "../../posts/getPostsModel";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private readonly postsUrl = environment.apiUrl + 'posts/';

  constructor(private httpClient: HttpClient) { }

  getPosts(sectionId: number): Observable<GetPostsModel> {
    return this.httpClient.get<GetPostsModel>(this.postsUrl + );
  }
}
