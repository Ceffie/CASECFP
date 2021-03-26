import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo(pos:string) {
    return browser.get(pos);
  }

  getMainHeading() {
    return element(by.css('app-root h1')).getText();
  }
}
