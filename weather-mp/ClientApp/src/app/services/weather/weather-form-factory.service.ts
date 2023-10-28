import { Injectable } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { FavoritesService } from '../favorites/favorites.service';

@Injectable({
  providedIn: 'root',
})
export class WeatherFormFactoryService {
  private _searchForm!: FormGroup;
  constructor(private formBuilder: FormBuilder,
    private favoritesService: FavoritesService) {}

  initialize() {
    this._initializeForm();
    this._subscribeToValueChanges();
  }

  private _initializeForm() {
    this._searchForm = this.formBuilder.group({
      searchValue: ['', Validators.required],
      favorite: [false],
    });
  }

  private _subscribeToValueChanges() {
    this.searchValueControl.valueChanges.pipe().subscribe({
      next: (response: string) => {
        if (response === '') {
          this.favoriteValueControl.setValue(false);
        } else {
          if (this.favoritesService.verifyFavoriteCookie()) {
            const favoriteList = this.favoritesService.getFavoriteListFromCookie();
            if (favoriteList.includes(response.toLowerCase())) {
              this.favoriteValueControl.setValue(true);
            }
          }
        }
      },
    });
  }

  get searchForm(): FormGroup {
    return this._searchForm as FormGroup;
  }

  get searchValueControl(): FormControl {
    return this.searchForm?.get('searchValue') as FormControl;
  }

  get favoriteValueControl(): FormControl {
    return this.searchForm?.get('favorite') as FormControl;
  }
}
