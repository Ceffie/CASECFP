import { Course } from "../models/Course";

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
        const tempCourseArray: Course[] = [];
        const tempCourse: Course = {name: "", code: "", date: "", days: 0, id: 0};
        //go through all Course arrays
        for (var i=0; i<arrayOfArrays.length; i++) {
    
        //remove these first characters in this order (Titel: , Cursuscode: , Duur: , Startdatum: ,) from each element
        for (var a=0; a<arrayOfArrays[i].length; a++) {
            if (a == 0){
            tempCourse.name = this.RemoveTitel(a, i, arrayOfArrays);
            }
            if (a == 1){
            tempCourse.code = this.RemoveCursusCode(a, i, arrayOfArrays);
            }
            if (a == 2){
            tempCourse.days = this.RemoveDuur(a, i, arrayOfArrays);
            }
            if (a == 3){
            tempCourse.date = this.RemoveStartDatum(a, i, arrayOfArrays);
            }
        }
        tempCourseArray.push(tempCourse);
        }
        return tempCourseArray;
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
