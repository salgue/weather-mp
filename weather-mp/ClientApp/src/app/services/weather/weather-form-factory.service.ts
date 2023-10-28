import { Injectable } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class WeatherFormFactoryService {
  private _searchForm!: FormGroup;
  constructor(private formBuilder: FormBuilder) {}

  initializeSearchForm() {
    this._searchForm = this.formBuilder.group({
      searchValue: [''],
    });
  }

  get searchForm(): FormGroup {
    return this._searchForm as FormGroup;
  }
}
