import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo(pos:string) {
    return browser.get(pos);
  }

  getMainHeading() {
    return element(by.css('app-root h1')).getText();
  }

  getNavButton(buttontoclick:string){
    return element(by.id ('navto'+buttontoclick));
  }

  getHeaderCourses(){
    return element(by.id ('tableLabel'));
  }
}
