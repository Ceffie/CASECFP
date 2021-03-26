import { AppComponent } from 'src/app/app.component';
import { AppPage } from './app.po';

describe('App', () => {
  let page: AppPage;
  let sut: AppComponent;
  let mockUpload

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getMainHeading()).toEqual('Welkom tot de Info Support Cursus App!');
  });

  it('should navigate to courses', () => {
    page.navigateTo();
    expect(page.getMainHeading()).toEqual('Welkom tot de Info Support Cursus App!');
  });
});
