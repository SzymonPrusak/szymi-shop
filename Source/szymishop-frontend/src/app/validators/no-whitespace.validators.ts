import { FormControl, ValidationErrors } from '@angular/forms';


export function noWhitespaceValidator(control: FormControl): ValidationErrors | null {
  const hasWhitespace = /\s/.test(control.value);
  return hasWhitespace ? { 'whitespace': true } : null;
}
