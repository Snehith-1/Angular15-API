import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators} from '@angular/forms';
import { Router } from '@angular/router';
interface IEntity {
entity_name: string;
  entity_description: string;
}
@Component({
  selector: 'app-addentity',
  templateUrl: './addentity.component.html',
  styleUrls: ['./addentity.component.scss']
})
export class AddentityComponent {
  
  reactiveForm!: FormGroup;
  entity: IEntity;

  constructor() {
    this.entity = {} as IEntity;
  }
  ngOnInit(): void {
    this.reactiveForm = new FormGroup({
      entity_name: new FormControl(this.entity.entity_name, [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250),
      ]),
      entity_description: new FormControl(this.entity.entity_description, [
        Validators.maxLength(10000),
      ]),
    
    });
  }

  get entity_name() {
    return this.reactiveForm.get('entity_name')!;
  }

  get entity_description() {
    return this.reactiveForm.get('entity_description')!;
  }

  public validate(): void {
    if (this.reactiveForm.invalid) {
      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      return;
    }

    this.entity = this.reactiveForm.value;

    console.info(this.entity);

  
  }
  close(){
    window.location.reload();
  }
}
