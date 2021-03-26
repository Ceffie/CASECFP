import { Injectable } from "@angular/core";
import { Course } from "../models/Course";

@Injectable({
    providedIn: 'root'
})
export class Uploader{
    SplitFileOnLinebreaks(FullFile) {
        return FullFile.toString().split("\n");
      }
      
    PushSplitArrayIntoAnotherArray(SplitFile: string[]) {
        const arrayOfArrays:string[][] = [];
        const size = 5;
        for (var i=0; i<SplitFile.length; i+=size) {
          arrayOfArrays.push(SplitFile.slice(i,i+size));
        }
        return arrayOfArrays;
      }
    
    TurnIntoAnArrayOfCourses(arrayOfArrays: string[][]): Course[] {
        let tempCourse: Course[] = []; 
        //go through all Course arrays
        for (var i=0; i<arrayOfArrays.length; i++) {
    
            //remove these first characters in this order (Titel: , Cursuscode: , Duur: , Startdatum: ,) from each element
            for (var a=0; a<arrayOfArrays[i].length; a++) {
                if (a == 0){
                tempCourse.push({name: "", code: "", date: "", days: 0, id: 0});
                tempCourse[i].name = this.RemoveTitel(a, i, arrayOfArrays);
                }
                if (a == 1){
                tempCourse[i].code = this.RemoveCursusCode(a, i, arrayOfArrays);
                }
                if (a == 2){
                tempCourse[i].days = this.RemoveDuur(a, i, arrayOfArrays);
                }
                if (a == 3){
                tempCourse[i].date = this.RemoveStartDatum(a, i, arrayOfArrays);
                }
            }
        }
        tempCourse.pop();
        return tempCourse;
    }
  
    RemoveTitel(a: number, i: number, arrayOfArrays: string[][]): string {
        return arrayOfArrays[i][a].replace("Titel: ", "");
    }
    
    RemoveCursusCode(a: number, i: number, arrayOfArrays: string[][]): string {
        return arrayOfArrays[i][a].replace("Cursuscode: ", "");
    }
    
    RemoveDuur(a: number, i: number, arrayOfArrays: string[][]): number {
        return +(arrayOfArrays[i][a].replace("Duur: ", "")).replace(" dagen", "");
    }
    
    RemoveStartDatum(a: number, i: number, arrayOfArrays: string[][]): string {
        return arrayOfArrays[i][a].replace("Startdatum: ", "");
    }
    
}
