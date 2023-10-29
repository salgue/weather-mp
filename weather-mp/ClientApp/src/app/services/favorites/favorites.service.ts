import { Injectable } from '@angular/core';
import { CookiesService } from '../cookies/cookies.service';
import { COOKIES_TYPES } from 'src/app/utils/consts';

@Injectable({
  providedIn: 'root',
})
export class FavoritesService {
  constructor(private cookieService: CookiesService) {}

  setFavoriteCookie(value: string) {
    this.cookieService.saveCookie(COOKIES_TYPES.Favorite_Cookie, value);
  }

  verifyFavoriteCookie(): boolean {
    return this.cookieService.verifyCookiExist(COOKIES_TYPES.Favorite_Cookie);
  }

  getFavoriteListFromCookie(): string[] {
    return JSON.parse(this.cookieService.getCookie(COOKIES_TYPES.Favorite_Cookie));
  }

  getFavoriteCookieValue() {
    return this.cookieService.getCookie(COOKIES_TYPES.Favorite_Cookie);
  }

  setAsFavorite(valueToStore: string) {
    if (!this.verifyFavoriteCookie()) {
      this.setFavoriteCookie(JSON.stringify([valueToStore]));
    } else {
      const favoriteList = this.getFavoriteListFromCookie();
      if (!favoriteList.includes(valueToStore)) {
        favoriteList.push(valueToStore);
        this.setFavoriteCookie(JSON.stringify(favoriteList));
        this.cookieService.saveCookie(COOKIES_TYPES.Favorite_Cookie, JSON.stringify(favoriteList));
      }
    }
  }

  removeAsFavorite(valueToStore: string) {
    if (this.verifyFavoriteCookie()) {
      const favoriteList = this.getFavoriteListFromCookie();
      if (favoriteList.includes(valueToStore)) {
        const filteredFavoriteList = favoriteList.filter(
          (f: string) => f !== valueToStore.toLowerCase()
        );
        if (filteredFavoriteList.length > 0) {
          this.setFavoriteCookie(JSON.stringify(filteredFavoriteList));
        } else {
          this.cookieService.deleteCookie(COOKIES_TYPES.Favorite_Cookie);
        }
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
