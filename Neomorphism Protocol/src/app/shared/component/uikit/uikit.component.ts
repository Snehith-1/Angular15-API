import { Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-uikit',
  templateUrl: './uikit.component.html',
  styleUrls: ['./uikit.component.scss']
})
export class UikitComponent {



  isInfoToast:boolean = false;
  isWarningToast:boolean = false;
  isErrorToast:boolean = false;
  isSuccessToast:boolean = false;
  isHoldToast:boolean = false;
  isRejectedToast:boolean = false;
  isApprovedToast:boolean = false;
  isDeleteToast:boolean = false;
  isCancelToast:boolean = false;

  n = 1;

  newlyAddedValue = '';
  isModalClose = '';
  isAddFormSubmitted:boolean = false;
  addDetailsFormValueArray:any = [];

  addDetailsForm = new FormGroup({
    addDetails1:new FormControl("", [Validators.required]),
    addDetails2:new FormControl("", [Validators.required]),
    addDetails3:new FormControl("", [Validators.required])
  });

  @ViewChild('toastDiv') toastDiv:any;

  constructor(public renderer:Renderer2,private elementRef: ElementRef){

  }

  showToastMessage(clickToast:string, toastElement : any,toastMessage: any) {

    if(clickToast == 'info')
    { 
      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-4" role="alert" aria-live="assertive" ' +
                      ' aria-atomic="true" style="border-left: 10px solid #2E86E7;"> ' +  
                      ' <div class="toast-body d-flex justify-content-between"> ' +
                      ' <div class="d-flex align-items-center"><div><span> ' +
                      ' <img src="assets/icons/info-icon.svg" alt=""></span></div> '  +
                      ' <div class="d-grid ps-4 light-dark-mode-text"> ' +
                      ' <span style="font-size: 16px; font-weight: 800;">Info</span> ' +
                      ' <span style="font-size: 12px;">' + toastMessage + ' </span> ' +
                      ' </div></div><div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" ' +
                      ' aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';
      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast'); 

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;
    }

    else if(clickToast == 'warning')
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" aria-atomic="true" style="border-left: 10px solid #FCC121;"><div class="toast-body d-flex justify-content-between"><div class="d-flex align-items-center"><div><span><img src="assets/icons/warning-circle.svg" alt=""></span></div><div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Warning</span><span style="font-size: 12px;">' + toastMessage + '</span></div></div><div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }

    else if(clickToast == 'error')
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" aria-atomic="true" style="border-left: 10px solid #FCC121;"><div class="toast-body d-flex justify-content-between"><div class="d-flex align-items-center"><div><span><img src="assets/icons/warning-circle.svg" alt=""></span></div><div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Error</span><span style="font-size: 12px;">' + toastMessage + '</span></div></div><div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }

    else if(clickToast == 'success')
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" '+
      'aria-atomic="true" style="border-left: 10px solid #43D966;">'+
      '<div class="toast-body d-flex justify-content-between">'+
      '<div class="d-flex align-items-center">'+
      '<div><span><img src="assets/icons/success-toast.svg" alt=""></span></div>'+
      '<div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Success</span>'+
      '<span style="font-size: 12px;">' + toastMessage + '</span></div></div>'+
      '<div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }

    else if(clickToast == 'hold')
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" aria-atomic="true" style="border-left: 10px solid #4E4E4E;"><div class="toast-body d-flex justify-content-between"><div class="d-flex align-items-center"><div><span><img src="assets/icons/hold-toast.svg" alt=""></span></div><div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Hold</span><span style="font-size: 12px;">' + toastMessage + '</span></div></div><div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }

    else if(clickToast == 'rejected')
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" aria-atomic="true" style="border-left: 10px solid #FE345B;"><div class="toast-body d-flex justify-content-between"><div class="d-flex align-items-center"><div><span><img src="assets/icons/rejected-toast.svg" alt=""></span></div><div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Rejected</span><span style="font-size: 12px;">' + toastMessage + '</span></div></div><div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }

    else if(clickToast == 'approved')
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" aria-atomic="true" style="border-left: 10px solid #43D966;"><div class="toast-body d-flex justify-content-between"><div class="d-flex align-items-center"><div><span><img src="assets/icons/success-toast.svg" alt=""></span></div><div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Approved</span><span style="font-size: 12px;">' + toastMessage + '</span></div></div><div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }

    else if(clickToast == 'delete')
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" aria-atomic="true" style="border-left: 10px solid #FE345B;"><div class="toast-body d-flex justify-content-between"><div class="d-flex align-items-center"><div><span><img src="assets/icons/delete-toast.svg" alt=""></span></div><div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Delete</span><span style="font-size: 12px;">' + toastMessage + '</span></div></div><div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }

    else if(clickToast == 'cancel')
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, clickToast+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" aria-atomic="true" style="border-left: 10px solid #FE345B;"><div class="toast-body d-flex justify-content-between"><div class="d-flex align-items-center"><div><span><img src="assets/icons/error-toast.svg" alt=""></span></div><div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Cancel</span><span style="font-size: 12px;">' + toastMessage + '</span></div></div><div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(toastElement.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = toastElement.nativeElement.querySelector('.'+clickToast+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }

    setTimeout(() => {
      this.n = 1;
    }, 7000);

  }

  onSubmit()
  {
    console.log(this.addDetailsForm);
    this.isAddFormSubmitted = true;
    this.addDetailsFormValueArray.push(this.addDetailsForm.value);
    console.log(this.addDetailsFormValueArray);
    this.addDetailsForm.reset();
    console.log(this.addDetailsFormValueArray);

  }

  success(message: string){
    {

      const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, 'success-toast'+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" '+
      'aria-atomic="true" style="border-left: 10px solid #43D966;">'+
      '<div class="toast-body d-flex justify-content-between">'+
      '<div class="d-flex align-items-center">'+
      '<div><span><img src="assets/icons/success-toast.svg" alt=""></span></div>'+
      '<div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Success</span>'+
      '<span style="font-size: 12px;">'+message+'</span></div></div>'+
      '<div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(this.toastDiv.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = this.toastDiv.nativeElement.querySelector('.'+'success-toast'+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;

    }
  }

  warning(message:string){
    const div: HTMLDivElement = this.renderer.createElement('div');
      this.renderer.addClass(div, 'warning-toast'+this.n);
      div.innerHTML = '<div id="liveToast" class="toast my-3" role="alert" aria-live="assertive" '+
      'aria-atomic="true" style="border-left: 10px solid #43D966;">'+
      '<div class="toast-body d-flex justify-content-between">'+
      '<div class="d-flex align-items-center">'+
      '<div><span><img src="assets/icons/warning-toast.svg" alt=""></span></div>'+
      '<div class="d-grid ps-4 light-dark-mode-text"><span style="font-size: 16px; font-weight: 800;">Warning</span>'+
      '<span style="font-size: 12px;">'+message+'</span></div></div>'+
      '<div><button type="button" class="btn-close light-dark-mode-text close-btn" data-bs-dismiss="toast" aria-label="Close" style="font-size: 8px; border: 1px solid; border-radius: 50%; padding: 5px;"></button></div></div></div>';

      this.renderer.appendChild(this.toastDiv.nativeElement, div);
      const span = div.querySelector('#liveToast');
      const maindiv = this.toastDiv.nativeElement.querySelector('.'+'warning-toast'+this.n);
      this.renderer.addClass(span, 'show-toast');

      setTimeout(() => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);

      }, 3000);

      const closeToast = div.querySelector('.close-btn');
      this.renderer.addClass(closeToast, 'close-toast'+this.n);
      this.renderer.listen(div.querySelector('.close-toast'+this.n), 'click', (event) => {
        this.renderer.addClass(span, 'fade-out');
        setTimeout(() => {
          this.renderer.removeClass(span, 'fade-out');
          this.renderer.removeChild(maindiv.parentNode, maindiv);
          this.n = 1;
        }, 500);
      });

      this.n++;
  }
  

  encryptURL(data:string) {
    var encryptedString = window.btoa(data);
    console.log(encryptedString);
    return encryptedString; 
} 
decryptURL(data:string) { 
    var decryptedString = window.atob(data); 
    var decryptedObject = JSON.parse('{"' + decodeURI(decryptedString.replace(/&/g, "\",\"").replace(/=/g,"\":\"")) + '"}');
    console.log(decryptedObject);
    return decryptedObject;

}

}
