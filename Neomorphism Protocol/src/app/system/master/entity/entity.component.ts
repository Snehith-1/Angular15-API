import {MatDialog,MatDialogConfig, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject, OnInit,ViewChild,Output, EventEmitter,ChangeDetectorRef  } from '@angular/core';
import { FormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

// import { MatTableExporterModule } from 'mat-table-exporter';

import {
  faCheckCircle, faBan , faTrash, faDownload, faUserPlus,
  faPenSquare, faTrashAlt, faTasks, 
} from '@fortawesome/free-solid-svg-icons';

// import { ToastrService } from 'ngx-toastr';
import { EntityService } from '../../service/entity.service';
import { AddentityComponent } from '../popup/addentity/addentity.component';
import { AppComponent } from 'src/app/app.component';
import { DeleteentityComponent } from '../popup/deleteentity/deleteentity.component';
interface IEntity {
  name: string;
  nickname: string;
  email: string;
  password: string;
  showPassword: boolean;
}
@Component({
  selector: 'app-entity',
  templateUrl: './entity.component.html',
  styleUrls: ['./entity.component.scss']
})
export class EntityComponent {
  panelOpenState = false;
 
  faCheckCircle = faCheckCircle;
  faBan = faBan;
  faTrash = faTrash;
  faDownload = faDownload;
  faUserPlus = faUserPlus;
  faPenSquare = faPenSquare;
  faTrashAlt = faTrashAlt;
  faTasks = faTasks;
  @Output() dataChange = new EventEmitter<any>();
  @Output() opverifierdetail: EventEmitter<any> = new EventEmitter<any>();
  @Output() opdocaction: EventEmitter<any> = new EventEmitter<any>(); 
  dataSource: any;

  displayedColumns: string[] = ['entity_code','entity_name','entity_description','created_by','created_date','action'];
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  resultsLength = 0;
  
  data :any
  responsedata: any;
  entity_list: any[] = [];
  tempdataSource!: MatTableDataSource<any>;
  pageSizeOptions: number[] = [5, 10, 25, 50];


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.tempdataSource.filter = filterValue.trim().toLowerCase();

    if (this.tempdataSource.paginator) {
      this.tempdataSource.paginator.firstPage();
    }
  }  
  


  constructor(private getData:EntityService,private route:Router, public notify:AppComponent,  private cdr: ChangeDetectorRef, private dialog: MatDialog) { 
   
  }
 
  // isAllSelected() {
  //   console.log(this.dataSource.data);
  //   const numSelected = this.selection.selected.length;
  //   const numRows = this.dataSource.data.length;
  //   return numSelected === numRows;
  // }

  // /** Selects all rows if they are not all selected; otherwise clear selection. */
  // masterToggle() {
  //   this.isAllSelected()
  //     ? this.dataSource.clear()
  //     : this.dataSource.data.forEach((row: any) => this.dataSource.select(row));
  // }

  // logSelection() {
  //   this.selection.selected.forEach((s: { name: any; }) => console.log(s.name));
  // }
  
  ngOnInit(): void {
    this.notify.showToastMessage('info','success');
    this.getData.getEntity().subscribe((result,)=>{
      this.responsedata=result;

      this.dataSource = this.responsedata.entitylist1;
      this.tempdataSource = new MatTableDataSource(this.dataSource);
    this.tempdataSource.paginator = this.paginator;
    this.tempdataSource.sort = this.sort;
    this.cdr.detectChanges();
    
    });
    

  }
  Filterchange(data: Event) {
    const value = (data.target as HTMLInputElement).value;
    this.dataSource.filter = value;
  }
 
  addentity(){

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "50%";
    this.dialog.open(AddentityComponent,dialogConfig);
  }
  //   oneditentity(row:any){
  //   this.getData.populateForm(row);
  //   const dialogConfig = new MatDialogConfig();
  //   dialogConfig.disableClose = true;
  //   dialogConfig.autoFocus = true;
  //   dialogConfig.width = "30%";
  //   this.dialog.open(EditentitymasterComponent,dialogConfig);
  // }

  btndelete(val:any) {
    this.getData.populateForm(val);
   const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "30%";
    this.dialog.open(DeleteentityComponent,dialogConfig);
     
    
  }

 //}
  
}

