import { NgIf } from '@angular/common';
import { Component, input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, NgModel, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-news-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule],
  templateUrl: './news-form.component.html',
  styleUrl: './news-form.component.css'
})
export class NewsFormComponent implements ControlValueAccessor{
  label = input<string>('');
  minHeight = input<string>('50px');
  constructor(@Self() public ngControl: NgControl){
    this.ngControl.valueAccessor = this
  }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

  get control(): FormControl{
    return this.ngControl.control as FormControl
  }
}
