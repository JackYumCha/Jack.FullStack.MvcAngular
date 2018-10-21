import { Injectable } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http/src/interceptor';
import { HttpRequest, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http/src/backend';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

const MEDIGRAPH_SERVER = '<Jack.FullStack.MvcAngular.API>';

@Injectable()
export class MvcAngularInterceptor implements HttpInterceptor {
    constructor(private router: Router) {
    }

    /** this will add the authorization to the header */
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        if(req.url.includes(MEDIGRAPH_SERVER)){
            req = req.clone({
                url: req.url.replace(MEDIGRAPH_SERVER, environment.apiUrl)
                //,
                //withCredentials: true
            });
        }
        return next.handle(req).pipe(catchError((err: any, httpError) => {
            if(err instanceof HttpErrorResponse && err.status == 401){
                console.warn('Unauthorized Access:', err.url);
                this.router.navigate(['login']);
                throw 'Unauthorized Access Error';
            }
            return httpError;
        }));
    }
}
