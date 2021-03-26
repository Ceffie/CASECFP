import { TestBed } from '@angular/core/testing';
import { element } from 'protractor';
import { Uploader } from '../../src/app/services/uploader.service';
import { UploadComponent } from '../../src/app/upload/upload.component';
import { AppPage } from './app.po';

describe('App', () => {
  let page: AppPage;
  let sutUpload: UploadComponent;
  let mockUploader: Uploader;
  mockUploader = jasmine.createSpyObj('mockUploader', 
  ['SplitFileOnLinebreaks', 'PushSplitArrayIntoAnotherArray', 'TurnIntoAnArrayOfCourses', 
  'RemoveTitel', 'RemoveCursusCode', 'RemoveDuur', 
  'RemoveStartDatum']);

  beforeEach(() => {
    // // TestBed.configureTestingModule({
    // //   declarations: [ UploadComponent ],
    // //   providers: [ { provide: Uploader, useClass: mockUploader } ]
    // // });

    // // sutUpload = TestBed.createComponent(UploadComponent).componentInstance;
    page = new AppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo('/');
    expect(page.getMainHeading()).toEqual('Welkom tot de Info Support Cursus App!');
  });
  // it('should click through to courses', () => {
  //   page.navigateTo('/');
  //   page.getNavButton('courses').click();
  //   expect(page.getHeaderCourses()).toEqual('Cursussen');
  // });
  // // it('should call uploader functions', () => {
  // //   page.navigateTo('/upload');
  // //   sutUpload.fileChange(null);
  // //   expect(mockUploader.SplitFileOnLinebreaks).toHaveBeenCalled();
  // // });
});
