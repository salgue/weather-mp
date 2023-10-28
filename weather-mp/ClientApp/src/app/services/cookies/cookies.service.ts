import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class CookiesService {
  constructor(private cookieService: CookieService) {}

  saveCookie(cookieName: string, cookieValue: string) {
    this.cookieService.set(cookieName, cookieValue);
  }

  getCookie(cookieName: string): string {
    return this.cookieService.get(cookieName);
  }

  verifyCookiExist(cookieName: string): boolean {
    return this.cookieService.check(cookieName);
  }

  deleteCookie(cookieName: string) {
    this.cookieService.delete(cookieName);
  }
}
