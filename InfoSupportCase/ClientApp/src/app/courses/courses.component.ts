import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {

  public courses: Course[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Course[]>(baseUrl + 'api/course').subscribe(result => {
      this.courses = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

}
