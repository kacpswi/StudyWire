import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';

export const schoolAdminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  
  if(accountService.roles().includes('School-Admin')){
    return true;
  }
  else{
    return false;
  }
};
