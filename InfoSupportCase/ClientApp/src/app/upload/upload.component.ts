import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Course } from '../models/Course';
import { Uploader } from './uploader';


@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  postId;
  CourseArray: Course[];
  uploader = new Uploader();
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  postCourseToServer(upl : Course[]){
    this.http.post<Course[]>("/api/course", upl).subscribe();
  }

  buttonClick(){
    this.postCourseToServer(this.CourseArray);
  }

  fileChange(event){
    //init necessary variables for reading the file as text
    const file:File = event.target.files[0];
    const reader = new FileReader();
    reader.readAsText(file);
    
    reader.onloadend = () => {   
      //init textfile
      const FullFile = reader.result
      
      const SplitFile = this.uploader.SplitFileOnLinebreaks(FullFile);

      const arrayOfArrays = this.uploader.PushSplitArrayIntoAnotherArray(SplitFile);

      this.CourseArray = this.uploader.TurnIntoAnArrayOfCourses(arrayOfArrays);
    };
  }
}


