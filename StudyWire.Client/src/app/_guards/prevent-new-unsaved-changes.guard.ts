import { CanDeactivateFn } from '@angular/router';
import { NewsNewComponent } from '../news/news-new/news-new.component';


export const preventNewUnsavedChanges: CanDeactivateFn<NewsNewComponent> = (component) => {
  if (component.newNewsForm.dirty){
    return confirm('Are you sure you want to continue? Any unsaved changes will be lost.')
  }
  return true;
};
