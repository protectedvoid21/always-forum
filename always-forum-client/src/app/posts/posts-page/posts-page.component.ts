import { Component } from '@angular/core';
import {SectionsService} from "../../core/services/sections.service";
import {PostsService} from "../../core/services/posts.service";

@Component({
  selector: 'app-posts-page',
  templateUrl: './posts-page.component.html',
  styleUrls: ['./posts-page.component.scss']
})
export class PostsPageComponent {
  constructor(private postsService: PostsService) { }

  ngOnInit() {
    this.postsService.getPosts().subscribe();
  }
}
