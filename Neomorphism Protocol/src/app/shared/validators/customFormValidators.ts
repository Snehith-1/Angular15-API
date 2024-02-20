import { ValidatorFn, AbstractControl, ValidationErrors } from "@angular/forms";

  export function TextInputValidator(maxLength:number): ValidatorFn{
    return (control:AbstractControl) : ValidationErrors | null => {{        
        const value = control.value;
        const pattern = /^[a-zA-Z ]*$/.test(value);
        if(!pattern){
            return { inputValidationError: true };
          }else if(!(value && value.length <= maxLength)){
            return { inputValidationError: true };
          }else{
            return null;
          }
    }
  }
}

export function CustomEmailValidator(maxLength:number): ValidatorFn{
    return (control:AbstractControl) : ValidationErrors | null => {{
        
        const value = control.value;
        const pattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|in|co\.in|org|co)$/.test(value);
        if(!pattern){
            return { inputValidationError: true };
          }else if(!(value && value.length < maxLength)){
            return { inputValidationError: true };
          }else{
            return null;
          }
    }
  }
}

export function NumberInputValidator(maxLength:number): ValidatorFn{
    return (control:AbstractControl) : ValidationErrors | null => {{
        
        const value = control.value;
        const pattern = /^[0-9]*$/.test(value);
        if(!pattern){
            return { inputValidationError: true };
          }else if(!(value && value.length == maxLength)){
            return { inputValidationError: true };
          }else{
            return null;
          }
    }
  }
}

export function TableValidator(): ValidatorFn{
    return (control:AbstractControl) : ValidationErrors | null => {{      
      const value = control.value;
      for(let i=0; i<value.length; i++){
        for (const key in value[i]) {
          if (Object.prototype.hasOwnProperty.call(value[i], key)) {
            // console.log(`${key}: ${value[i][key]}`);
            if(value[i][key] == null || value[i][key] == '' || value.length == 0){
              // console.log("Mukilan");
              // console.log(`${key}: ${value[i][key]}`);              
              return { inputValidationError: true };
            }
          }
        }

      }
      
      return null; 
  }
}
}


export function MultiSeclectDropdownValidatior(): ValidatorFn{
  return (control:AbstractControl) : ValidationErrors | null => {{
      
      const value = control.value;
      // console.log("MultiSeclectDropdownValidatior");
      // console.log(value);
      if(value.length == 0){
        return { inputValidationError: true };
      }else{
      return null;
      }
  }
}
}