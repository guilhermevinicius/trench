import { Injectable } from '@angular/core';
import { ILocaleLanguage } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class AuthComponentService {

  getTranslate(locale: Array<ILocaleLanguage>, localeGroupsId: string[]) {
    const defaultLanguage = this.getLang(locale);

    let localeById = {};
    localeGroupsId.forEach((x) => {
      localeById = Object.assign(localeById, locale.find(locale => locale.name === defaultLanguage)?.value[x] || {});
    });

    const language = 'pt-BR';
    let specificLocale = language !== defaultLanguage ?
      locale.find(locale => locale.name === language) :
      null;

    if (specificLocale) {
      localeGroupsId.forEach((x) => {
        localeById = Object.assign(localeById, specificLocale?.value[x]);
      });
    }

    return localeById;
  }

  //#region Private Methods

  private getLang(locale: Array<ILocaleLanguage>) {
    let lang = 'pt-DEFAULT';
    const existtLang = locale.findIndex(locale => locale.name === lang);

    if (existtLang === -1) {
      const msgDefaultLang = `%cReplacing the default language to "en-DEFAULT". "${lang}" not found in LOCALE.`;
      console.log('%cWarning! ', 'color: red; font-size: 20px');
      console.log(msgDefaultLang, 'color: red; font-size: 16px');
      lang = 'en-DEFAULT';
    }

    return lang;
  }

  //#endregion Private Methods
}