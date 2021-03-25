import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  postId;
  CourseArray: Course[];
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {}

  ngOnInit() {}

  postCourseToServer(upl : Course[]){
    this.http.post<Course[]>("/api/course", upl).subscribe(data => {});
  }

  buttonClick(event){
    
  }

  fileChange(event){
    //init necessary variables for reading the file as text
    const file:File = event.target.files[0];
    const reader = new FileReader();
    reader.readAsText(file);

    reader.onloadend = () => {   
      //init textfile
      const FullFile = reader.result
      
      const SplitFile = SplitFileOnLinebreaks(FullFile);

      const arrayOfArrays = PushSplitArrayIntoAnotherArray(SplitFile);

      this.CourseArray = TurnIntoAnArrayOfCourses(arrayOfArrays);

      this.postCourseToServer(this.CourseArray); 
    };
  }
}

function SplitFileOnLinebreaks(FullFile) {
  return FullFile.toString().split("\n");
}

function PushSplitArrayIntoAnotherArray(SplitFile: any) {
  const arrayOfArrays = [];
  const size = 5;
  for (var i=0; i<SplitFile.length; i+=size) {
    arrayOfArrays.push(SplitFile.slice(i,i+size));
  }
  //remove empty items at the end if any
  if(arrayOfArrays.slice(-1).length <= 1){
    arrayOfArrays.pop();
  }
  return arrayOfArrays;
}

function TurnIntoAnArrayOfCourses(arrayOfArrays: any[]): Course[] {
  const tempCourseArray: Course[] = [];
  const tempCourse: Course = {name: "", code: "", date: "", days: 0, id: 0};
  //go through all Course arrays
  for (var i=0; i<arrayOfArrays.length; i++) {
    if(arrayOfArrays[i].slice(-1) == ""){
      arrayOfArrays[i].pop();
    }

    //remove these first characters in this order (Titel: , Cursuscode: , Duur: , Startdatum: ,) from each element
    for (var a=0; a<arrayOfArrays[i].length; a++) {
      if (a == 0){
        tempCourse.name = RemoveTitel(a, i, arrayOfArrays);
      }
      if (a == 1){
        tempCourse.code = RemoveCursusCode(a, i, arrayOfArrays);
      }
      if (a == 2){
        tempCourse.days = RemoveDuur(a, i, arrayOfArrays);
      }
      if (a == 3){
        tempCourse.date = RemoveStartDatum(a, i, arrayOfArrays);
      }
    }
    tempCourseArray.push(tempCourse);
  }
  return tempCourseArray;
}

function RemoveTitel(a: number, i: number, arrayOfArrays: any[]): string {
  return arrayOfArrays[i][a].replace("Titel: ", "");
}

function RemoveCursusCode(a: number, i: number, arrayOfArrays: any[]): string {
  return arrayOfArrays[i][a].replace("Cursuscode: ", "");
}

function RemoveDuur(a: number, i: number, arrayOfArrays: any[]): number {
  arrayOfArrays[i][a] = arrayOfArrays[i][a].replace("Duur: ", "");
  arrayOfArrays[i][a] = arrayOfArrays[i][a].replace(" dagen", "");
  return +arrayOfArrays[i][a]
}

function RemoveStartDatum(a: number, i: number, arrayOfArrays: any[]): string {
  return arrayOfArrays[i][a].replace("Startdatum: ", "");
}

