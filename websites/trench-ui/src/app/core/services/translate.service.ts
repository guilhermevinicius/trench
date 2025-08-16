import { Injectable } from '@angular/core';
import { ILocaleLanguage } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ColporteurTranslateService {
  private language: string;

  constructor() {
    this.language = this.getBrowserDefaultLanguage();
  }

  getTranslate(locale: Array<ILocaleLanguage>, localesId: string[]) {
    let localeById = {};
    const defaultLanguage = this.formatDefaultLanguage();

    localesId.forEach((x) => {
      localeById = Object.assign(localeById, locale.find(locale => locale.name === defaultLanguage)?.value[x]);
    });

    let specificLocale = this.language !== defaultLanguage ?
      locale.find(locale => locale.name === this.language) :
      null;

    if (specificLocale) {
      localesId.forEach((x) => {
        localeById = Object.assign(locale, specificLocale?.value[x]);
      });
    }

    return localeById;
  }

  formatDate(date: Date, locale: string): string {
    return new Intl.DateTimeFormat(locale).format(date);
  }

  formatDateTime(date: Date, locale: string): string {
    const formattedDate = new Intl.DateTimeFormat(locale, {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
    }).format(date);

    const formattedTime = new Intl.DateTimeFormat(locale, {
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
      hour12: false
    }).format(date);

    return `${formattedDate} ${formattedTime}`;
  }

  formatMonthYear(date: Date, locale: string): string {
    const options: Intl.DateTimeFormatOptions = {
      year: 'numeric',
      month: '2-digit'
    };

    const formatter = new Intl.DateTimeFormat(locale, options);

    return formatter.format(date);
  }

  formatNumber(value: number, locale: string): string {
    return new Intl.NumberFormat(locale).format(value);
  }

  formatCurrency(value: number, locale: string, currency: string, showSymbol: boolean = false): string {
    const formattedValue = new Intl.NumberFormat(locale, {
      style: 'currency',
      currency: currency,
      currencyDisplay: showSymbol ? 'symbol' : 'code'
    }).format(value);

    return showSymbol ? formattedValue : formattedValue.replace(/[^\d.,-]/g, '').trim();
  }

  //#region Private Methods

  private getBrowserDefaultLanguage() {
    const wn = window.navigator as any;
    const browserLanguage = wn.language as string;
    const browserDefaultLanguage = (browserLanguage && browserLanguage !== '') ?
      'pt-DEFAULT' : //browserLanguage :
      'pt-DEFAULT';

    return browserDefaultLanguage;
  }

  private formatDefaultLanguage() {
    const index = this.language.lastIndexOf('-');
    const prefix = this.language.substring(0, index > -1 ? index : this.language.length);

    return `${prefix}-DEFAULT`;
  }

  //#endregion Private Methods
}
