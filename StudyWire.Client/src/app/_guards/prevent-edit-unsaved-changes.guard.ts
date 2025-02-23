import { CanDeactivateFn } from '@angular/router';
import { NewsEditComponent } from '../news/news-edit/news-edit.component';

export const preventEditUnsavedChangesGuard: CanDeactivateFn<NewsEditComponent> = (component) => {
  if (component.form?.dirty){
    return confirm('Are you sure you want to continue? Any unsaved changes will be lost.')
  }
  return true;
};
