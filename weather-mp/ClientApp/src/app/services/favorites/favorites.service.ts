import { Injectable } from '@angular/core';
import { CookiesService } from '../cookies/cookies.service';

@Injectable({
  providedIn: 'root',
})
export class FavoritesService {
  constructor(private cookieService: CookiesService) {}

  setFavoriteCookie(value: string) {
    this.cookieService.saveCookie('favorite', value);
  }

  verifyFavoriteCookie(): boolean {
    return this.cookieService.verifyCookiExist('favorite');
  }

  getFavoriteListFromCookie(): string[] {
    return JSON.parse(this.cookieService.getCookie('favorite'));
  }

  setAsFavorite(valueToStore: string) {
    if (!this.verifyFavoriteCookie()) {
      this.setFavoriteCookie(JSON.stringify([valueToStore]));
    } else {
      const favoriteList = this.getFavoriteListFromCookie();
      if (!favoriteList.includes(valueToStore)) {
        favoriteList.push(valueToStore);
        this.setFavoriteCookie(JSON.stringify(favoriteList));
        this.cookieService.saveCookie('favorite', JSON.stringify(favoriteList));
      }
    }
  }

  removeAsFavorite(valueToStore: string) {
    if (this.verifyFavoriteCookie()) {
      const favoriteList = this.getFavoriteListFromCookie();
      const filteredFavoriteList = favoriteList.filter(
        (f: string) => f !== valueToStore
      );
      if (filteredFavoriteList.length > 0) {
        this.setFavoriteCookie(JSON.stringify(filteredFavoriteList));
      } else {
        this.cookieService.deleteCookie('favorite');
      }
    }
  }

  addFavoriteListToCookie(markAsFavorite: boolean, valueToStore: string) {
    if (markAsFavorite) {
      this.setAsFavorite(valueToStore.toLowerCase());
    } else {
      this.removeAsFavorite(valueToStore.toLowerCase());
    }
  }
}
