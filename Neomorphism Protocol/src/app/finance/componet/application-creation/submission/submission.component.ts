import { Component, OnInit } from '@angular/core';
import { SocketService } from 'src/app/shared/services/socket.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { UikitComponent } from 'src/app/shared/component/uikit/uikit.component';

@Component({
  selector: 'app-submission',
  templateUrl: './submission.component.html',
  styleUrls: ['./submission.component.scss']
})

export class SubmissionComponent implements OnInit {    
  
  hierarchyDetails:any;
  hierarchychangeDetails: any;
  hierarchyupdateDetails: any;

  params: any;
  application_gid: any;
  clustermanager_name: any;
  regionhead_name: any;
  zonalhead_name: any;
  businesshead_name: any;  
    
  constructor(private socketService: SocketService, private router:Router, private route:ActivatedRoute,public notify: AppComponent, public encrypt : UikitComponent) { }

  hierarchychangelist(){    
    var urlhierarchychangelist = "MstApplicationAdd/GetApprovalHierarchyChangeList";
    
    this.params = {
      application_gid: this.application_gid,
    }
    
    this.socketService.getparams(urlhierarchychangelist,this.params).subscribe((responsehierarchychangelist: any) => {
      this.hierarchychangeDetails = 
      [ { level: 'L0', position: 'RM',  name:responsehierarchychangelist.rm_name },
        { level: 'L1', position: 'DRM', name:responsehierarchychangelist.directreportingto_name },
        { level: 'L2', position: 'CH',  name:responsehierarchychangelist.clustermanager_name },
        { level: 'L3', position: 'RH',  name:responsehierarchychangelist.regionhead_name },
        { level: 'L4', position: 'ZH',  name:responsehierarchychangelist.zonalhead_name },
        { level: 'L5', position: 'BVH', name:responsehierarchychangelist.businesshead_name },   
      ]      
    });   
  }

  confirm(){
    this.params = {
      application_gid: this.application_gid,
      clustermanager_name: this.clustermanager_name,
      regionhead_name: this.regionhead_name,
      zonalhead_name: this.zonalhead_name,
      businesshead_name: this.businesshead_name,
    }

    var urlhierarchyconfirmlist = "MstApplicationAdd/UpdateApprovalHierarchyChange";
    
    this.socketService.post(urlhierarchyconfirmlist, this.params).subscribe((responsehierarchyconfirmlist: any) => {      
      if(responsehierarchyconfirmlist.status == true){        
        this.notify.showToastMessage('success',responsehierarchyconfirmlist.message);
      }
      else{
        this.notify.showToastMessage('warning',responsehierarchyconfirmlist.message);
      }
    });
  }

  submit(){
    this.params = {
      application_gid: this.application_gid,
    }

    var urloverallsubmit = "MstApplicationEdit/EditAppProceed";
    
    this.socketService.post(urloverallsubmit, this.params).subscribe((responseoverallsubmit: any) =>
    {      
      if(responseoverallsubmit.status == true){        
        this.notify.showToastMessage('success',responseoverallsubmit.message);
      }
      else{
        this.notify.showToastMessage('warning',responseoverallsubmit.message);
      }
    });    
    this.router.navigateByUrl('app/applicationsummary');
  }
  
  ngOnInit(): void {    
    var urlHierarchylist = "MstApplicationAdd/GetProceed"; 
  
    this.socketService.get(urlHierarchylist).subscribe((responsehierarchylist: any) => {       
      this.hierarchyDetails = 
      [ { level: 'L0', position: 'RM',  name: responsehierarchylist.level_zero },
        { level: 'L1', position: 'DRM', name: responsehierarchylist.level_one },
        { level: 'L2', position: 'CH',  name: responsehierarchylist.cluster_head },
        { level: 'L3', position: 'RH',  name: responsehierarchylist.regional_head },
        { level: 'L4', position: 'ZH',  name: responsehierarchylist.zonal_head },
        { level: 'L5', position: 'BVH', name: responsehierarchylist.business_head },
      ]
      console.log(responsehierarchylist, this.hierarchyDetails);
    });
    
    // For Decryption
    /* this.route.queryParams.subscribe(params => {
      const hash = params['hash'];
      if (hash) {
        const searchObject = this.encrypt.decryptURL(hash);
        console.log(searchObject);
      }
    }); */
  }    
}