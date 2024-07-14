import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';

export const teacherGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  
  if(accountService.roles().includes('Teacher')){
    return true;
  }
  else{
    return false;
  }
};
