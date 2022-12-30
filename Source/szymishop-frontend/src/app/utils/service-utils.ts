import { HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';


export function handleError(handler: (e: HttpErrorResponse) => Observable<never> | null): (e: HttpErrorResponse) => Observable<never> {
    return (error) => {
        if (error.status === 0) {
            console.log('An error occured:', error.error);
        }
        else {
            console.error(`Backend returned code ${error.status}, body was: `, error.error);
        }
        
        const err = handler(error);
        if (!err) {
            return throwError(() => 'Something went wrong');
        }
        return err;
    };
}