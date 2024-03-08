import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from "@angular/common/http";
import { Observable, catchError, concatMap, of, retryWhen, throwError } from "rxjs";
import { AlertifyService } from "./alertify.service";
import { Injectable } from "@angular/core";
import { observableToBeFn } from "rxjs/internal/testing/TestScheduler";
import { ErrorCode } from "../enums/enums";

@Injectable({
  providedIn: 'root'
})

export class HttpErrorInterceptorSevice implements HttpInterceptor {
  constructor (private alert: AlertifyService) {}
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    console.log("through here");
    return next.handle(req)
    .pipe(
      retryWhen(error => this.retryRequest(error, 3)),
      catchError((error: HttpErrorResponse) => {
        console.log(error.error)
        const errorMessage = this.setError(error);
        this.alert.error(errorMessage)
        return throwError(errorMessage)
      })
    );
  }

  retryRequest(error: Observable<HttpErrorResponse>, retryCount: number): Observable<HttpErrorResponse> {
    return error.pipe(
      concatMap((checkErr: HttpErrorResponse, count: number) => {
        if (count <= retryCount) {
          switch (checkErr.status) {
            case ErrorCode.serverDown:
              return of(checkErr);
            // case ErrorCode.unauthorised:
            //   return of(checkErr);
          }
        }
        return throwError(checkErr);
      })
    );
  }

  setError(error: HttpErrorResponse): string {
    let errorMessage = 'Unknown error occured';
    if(error.error instanceof ErrorEvent) {
        // Client side error
        errorMessage = error.error.message;
    } else {
        // server side error
        if (error.status!==0) {
            errorMessage = error.error.errorMessage;
        }
    }
    return errorMessage;
}
}
