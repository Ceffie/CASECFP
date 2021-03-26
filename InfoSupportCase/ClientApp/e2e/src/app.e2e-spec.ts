import { TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { CoursesComponent } from 'src/app/courses/courses.component';
import { HomeComponent } from 'src/app/home/home.component';
import { Uploader } from 'src/app/services/uploader.service';
import { UploadComponent } from 'src/app/upload/upload.component';
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
    page = new AppPage();
    // TestBed.configureTestingModule({
    //   declarations: [ UploadComponent ],
    //   imports: [ FormsModule ],
    //   providers: [ { provide: Uploader, useClass: mockUploader } ],
    // });

    // sutUpload = TestBed.createComponent(UploadComponent).componentInstance;
  });

  it('should display welcome message', () => {
    page.navigateTo('/');
    expect(page.getMainHeading()).toEqual('Welkom tot de Info Support Cursus App!');
  });

  // it('should call uploader functions', () => {
  //   page.navigateTo('/upload');
  //   sutUpload.fileChange(null);
  //   expect(mockUploader.SplitFileOnLinebreaks).toHaveBeenCalled();
  // });
});
