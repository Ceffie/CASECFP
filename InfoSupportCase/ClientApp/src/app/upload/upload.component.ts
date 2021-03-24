import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { getBaseUrl } from 'src/main';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  postId;
  CourseArrays;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 

  }

  ngOnInit() {

  }

  postToCourseToViewModel(upl : Course[]){
    this.http.post<Course[]>("/api/course", upl).subscribe(data => {
      console.log("Hai");
    });
  }

  fileChange(event){
    const file:File = event.target.files[0];
    const reader = new FileReader();
    reader.readAsText(file);

    reader.onloadend = (e) => {   
      const FullFile = reader.result
      const SplitFile = FullFile.toString().split("\n");

      const size = 5; 
      const arrayOfArrays = [];
      const tempCourseArray: Course[] = [];
      const tempCourse: Course = {name: "", code: "", date: "", days: 0, id: 0};
      for (var i=0; i<SplitFile.length; i+=size) {
        arrayOfArrays.push(SplitFile.slice(i,i+size));
      }
      //remove empty items at the end
      if(arrayOfArrays.slice(-1).length <= 1){
        arrayOfArrays.pop();
      }
      //go through all Course arrays
      for (var i=0; i<arrayOfArrays.length; i++) {
        if(arrayOfArrays[i].slice(-1) == ""){
          arrayOfArrays[i].pop();
        }

        //remove these first characters in this order (Titel: , Cursuscode: , Duur: , Startdatum: ,) from each element
        for (var a=0; a<arrayOfArrays[i].length; a++) {
          if (a == 0){
            arrayOfArrays[i][a] = arrayOfArrays[i][a].replace("Titel: ", "");
            tempCourse.name = arrayOfArrays[i][a]
          }
          if (a == 1){
            arrayOfArrays[i][a] = arrayOfArrays[i][a].replace("Cursuscode: ", "");
            tempCourse.code = arrayOfArrays[i][a]
          }
          if (a == 2){
            arrayOfArrays[i][a] = arrayOfArrays[i][a].replace("Duur: ", "");
            arrayOfArrays[i][a] = arrayOfArrays[i][a].replace(" dagen", "");
            tempCourse.days = +arrayOfArrays[i][a]
          }
          if (a == 3){
            arrayOfArrays[i][a] = arrayOfArrays[i][a].replace("Startdatum: ", "");
            tempCourse.date = arrayOfArrays[i][a]
          }

        }
        tempCourseArray.push(tempCourse);
      }
      console.log(arrayOfArrays);
      //CourseArrays = (arrayOfArrays);
      this.postToCourseToViewModel(tempCourseArray); 
    };
  }
}
