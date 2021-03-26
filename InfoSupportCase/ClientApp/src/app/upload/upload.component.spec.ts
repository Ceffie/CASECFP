import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UploadComponent } from './upload.component';
import { Uploader } from './uploader';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Course } from '../models/Course';

describe('UploadComponent', () => {
  let component: UploadComponent;
  let fixture: ComponentFixture<UploadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadComponent ],
      imports: [ HttpClientTestingModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', async (() => {
    expect(component).toBeTruthy();
  }));

  it('should seperate text on linebreaks', async(() => {
    //arrange
    const str:string = "seperate \n this. \n"
    const uploader = new Uploader();
    //act
    const a = uploader.SplitFileOnLinebreaks(str)
    //assert
    expect(a.length).toEqual(3);
    expect(a).toEqual(["seperate ", " this. ", ""])
  }));

  it('should seperate splitfile into array made of arrays', async(() => {
    //arrange
    const strArr:string[] = ["Titel", "Cursuscode", "Duur", "Startdatum", "", "Titel", "Cursuscode", "Duur", "Startdatum", ""];
    const uploader = new Uploader();

    //act
    const a = uploader.PushSplitArrayIntoAnotherArray(strArr)
    //assert
    const answer = [["Titel", "Cursuscode", "Duur", "Startdatum", ""],["Titel", "Cursuscode", "Duur", "Startdatum", ""]];
    expect(a[0].length).toEqual(5);
    expect(a[0]).toEqual(answer[0]); 
    expect(a[1]).toEqual(answer[1]);  
  }));

  it('should be converted to a Course[]', async(() => {
    //arrange
    const strArrArr:string[][] = [["Titel: Test titel", "Cursuscode: TEST", "Duur: 1", "Startdatum: 11/11/1111", ""],["Titel: Test titel", "Cursuscode: TEST", "Duur: 1", "Startdatum: 11/11/1111", ""],[""]]
    const uploader = new Uploader();
    //act
    const a = uploader.TurnIntoAnArrayOfCourses(strArrArr);
    //assert
    const answer:Course[] = [{code: "TEST", name: "Test titel", id: 0, date: "11/11/1111", days: 1},{code: "TEST", name: "Test titel", id: 0, date: "11/11/1111", days: 1}]
    expect(a.length).toEqual(2);
    expect(a).toEqual(answer);
  }));

  it('should be converted to a Course[] with different elements', async(() => {
    //arrange
    const strArrArr:string[][] = [["Titel: Test titel", "Cursuscode: TEST", "Duur: 1", "Startdatum: 11/11/1111", ""],["Titel: Test titel 2", "Cursuscode: TEST2", "Duur: 1", "Startdatum: 11/11/1112", ""],[""]]
    const uploader = new Uploader();
    //act
    const a = uploader.TurnIntoAnArrayOfCourses(strArrArr);
    //assert
    const answer:Course[] = [{code: "TEST", name: "Test titel", id: 0, date: "11/11/1111", days: 1},{code: "TEST2", name: "Test titel 2", id: 0, date: "11/11/1112", days: 1}]
    expect(a.length).toEqual(2);
    expect(a).toEqual(answer);
  }));

  it('should remove Titel: ', async(() => {
    //arrange
    const strArr:string[][] = [["Titel: Test titeltje"]]
    const uploader = new Uploader();
    //act
    const a = uploader.RemoveTitel(0,0,strArr);
    //assert
    expect(a).toEqual("Test titeltje");
  }));

  // This doesn't work (yet)
  // it('should throw if trying to remove a nonexistent Titel: ', async(() => {
  //   //arrange
  //   const strArr:string[][] = [["Test titeltje"]]
  //   const uploader = new Uploader();
  //   //act
  //   const a = uploader.RemoveTitel(0,0,strArr);
  //   //assert
  //   expect(a).toThrowError();
  // }));

  it('should remove Cursuscode: ', async(() => {
    //arrange
    const strArr:string[][] = [["Cursuscode: TESTCODE"]]
    const uploader = new Uploader();
    //act
    const a = uploader.RemoveCursusCode(0,0,strArr);
    //assert
    expect(a).toEqual("TESTCODE");
  }));

  it('should remove Duur: ', async(() => {
    //arrange
    const strArr:string[][] = [["Duur: 5 dagen"]]
    const uploader = new Uploader();
    //act
    const a = uploader.RemoveDuur(0,0,strArr);
    //assert
    expect(a).toEqual(5);
  }));

  it('should remove Startdatum: ', async(() => {
    //arrange
    const strArr:string[][] = [["Startdatum: 11/11/1111"]]
    const uploader = new Uploader();
    //act
    const a = uploader.RemoveStartDatum(0,0,strArr);
    //assert
    expect(a).toEqual("11/11/1111");
  }));
});
