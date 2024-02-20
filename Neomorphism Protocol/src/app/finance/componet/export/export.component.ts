import { Component } from '@angular/core';
import { ApplicationService } from '../../services/application.service';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.scss']
})
export class ExportComponent {
  responsedata:any;
  excelresponsedata:any;
  constructor(private post:ApplicationService){}
  downloadexcel(){
    this.post.getexportexcel().subscribe((result) => {
      this.responsedata = result;
      console.log(this.responsedata);
      console.log(this.responsedata.lscloudpath);
      console.log(this.responsedata.lsname);
      this.post.downloadfile(this.responsedata.lscloudpath,this.responsedata.lsname).subscribe((result)=> {
        this.excelresponsedata = result;
        console.log(this.excelresponsedata);
        let b64data = this.excelresponsedata.file;
        let exportb64blob = this.b64toBlob(b64data,'application/vnd.ms-excel');
        let exportblob: Blob=exportb64blob as Blob;
        let a =document.createElement('a');
        a.download=this.responsedata.lsname;
        //a.href = window.URL.createObjectURL(blob);
        //var binaryData = [];
        //binaryData.push(this.excelresponsedata.file);
        a.href = window.URL.createObjectURL(exportblob);
        a.click();
      })
    });
  }

  b64toBlob(b64Data:any, contentType:any) {
    contentType = contentType || '';
    const sliceSize = 512; var byteCharacters = atob(b64Data);
    var byteArrays = [];
    for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        var slice = byteCharacters.slice(offset, offset + sliceSize);
        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }
    var blob = new Blob(byteArrays, { type: contentType }); return blob;
}

}
