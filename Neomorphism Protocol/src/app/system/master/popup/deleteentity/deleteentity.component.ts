import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Entity } from 'src/app/system/model/entity';
import { EntityService } from 'src/app/system/service/entity.service';

@Component({
  selector: 'app-deleteentity',
  templateUrl: './deleteentity.component.html',
  styleUrls: ['./deleteentity.component.scss']
})
export class DeleteentityComponent {
  CurObj : Entity = new Entity();
  constructor(public getData:EntityService,private formBuilder: FormBuilder,private route:Router) { 
   
  }
  ngOnInit(): void {    

  }
  decline(){
window.location.reload();
  }
  accept(){
    this.CurObj.entity_gid = this.getData.form.value.entity_gid;
    console.log(this.CurObj.entity_gid)
  }
}
