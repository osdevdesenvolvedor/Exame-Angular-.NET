import { HttpInterceptorFn } from '@angular/common/http';


export const apiUrlInterceptor: HttpInterceptorFn = (req: any, next: (arg0: any) => any) => {
  return next(req);
};
