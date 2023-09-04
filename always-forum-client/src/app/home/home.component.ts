import { Component } from '@angular/core';
import {SectionsService} from "../core/services/sections.service";
import {Observable} from "rxjs";
import {GetSectionsResponseModel} from "../sections/getSectionsResponse.model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  sectionsResponse$!: Observable<GetSectionsResponseModel>

  constructor(private sectionsService: SectionsService) { }

  ngOnInit() {
    this.sectionsResponse$ = this.sectionsService.getSections();
  }
}
