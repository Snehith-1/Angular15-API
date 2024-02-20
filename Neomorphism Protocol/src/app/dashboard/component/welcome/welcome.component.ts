import { Component } from '@angular/core';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent {
  advanceHomePageContent = [
    {heading:'Stages - Active Application'},
    {heading:'Proposals - Open Tickets'},
    {heading:'Proposals - New vs Renewel'},
    {heading:'Proposal - Conversion Approval Logs'},
  ];
  commonModuleData = [
    {id:1, content: 'Zing HR', imgURL: 'assets/money-change.svg', isEnnable: true, link: 'https://login.microsoftonline.com/common/oauth2/authorize?response_type=code&client_id=c0b9d618-9f52-4cac-a3e2-cf989d30c334&resource=https%3A%2F%2Fgraph.windows.net&state=samunnati&redirect_uri=https%3A%2F%2Fportal.zinghr.com%2FAzureApp%2FAzureSignin%2FCallBack'},
    {id:2, content: 'Hinote', imgURL: 'assets/chart-square.svg', isEnnable: true, link: 'https://hinote.in/hrworksidp/secure.jsp'},
    {id:3, content: 'Dockabl', imgURL: 'assets/lock.svg', isEnnable: true, link: 'https://performance.xto10x.com/samunnati/login'},
    {id:4, content: 'Travel Booking', imgURL: 'assets/empty-wallet-remove.svg', isEnnable: true, link: 'https://corporate.atyourprice.net/index.php'},
    {id:5, content: 'Service Request', imgURL: 'assets/receipt-minus.svg', isEnnable: true, link:''},
    {id:6, content: 'TMS', imgURL: 'assets/coin.svg', isEnnable: true ,link:''},
    {id:7, content: 'SOPs', imgURL: 'assets/security-user.svg', isEnnable: true, link: 'https://samunittest.storyboarderp.com/v1/#/app/MstDocumentUploadSummary'},
    {id:8, content: 'Brand Resources', imgURL: 'assets/card-coin.svg', isEnnable: true, link: 'https://login.microsoftonline.com/655a0e0e-4a74-4a0c-86d8-370a992e90a6/oauth2/authorize?client%5Fid=00000003%2D0000%2D0ff1%2Dce00%2D000000000000&response%5Fmode=form%5Fpost&response%5Ftype=code%20id%5Ftoken&resource=00000003%2D0000%2D0ff1%2Dce00%2D000000000000&scope=openid&nonce=C3DA60732691A4D60CCDC9939958E51D5157BE41879D58B4%2D5E4D61F86AD95ECF28B8BCE57A6362CD5A97460380561AA1B73504538A711C97&redirect%5Furi=https%3A%2F%2Fsamfin%2Esharepoint%2Ecom%2F%5Fforms%2Fdefault%2Easpx&state=OD0w&claims=%7B%22id%5Ftoken%22%3A%7B%22xms%5Fcc%22%3A%7B%22values%22%3A%5B%22CP1%22%5D%7D%7D%7D&wsucxt=1&cobrandid=11bd8083%2D87e0%2D41b5%2Dbb78%2D0bc43c8a8e8a&client%2Drequest%2Did=4cb5b3a0%2D80bd%2D2000%2D47aa%2D091d01748c03'},
    {id:9, content: 'Induction', imgURL: 'assets/money-recive-disable-lightMode.svg', isEnnable: false ,link:''},
    {id:10, content: 'Induction', imgURL: 'assets/money-recive-disable-lightMode.svg', isEnnable: false ,link:''}
  ];

  quickToggleData = [
    {id:1, content: 'Create New Application', imgURL: '/assets/money-change.svg', isEnnable: true},
    {id:2, content: 'Business Approval', imgURL: '/assets/chart-square.svg', isEnnable: true},
    {id:3, content: 'Start Underwriting', imgURL: '/assets/lock.svg', isEnnable: true},
    {id:4, content: 'CC Approval', imgURL: '/assets/receipt-minus-disable-lightMode.svg', isEnnable: false},
    {id:5, content: 'CC Approval', imgURL: '/assets/receipt-minus-disable-lightMode.svg', isEnnable: false}

  ];
}
