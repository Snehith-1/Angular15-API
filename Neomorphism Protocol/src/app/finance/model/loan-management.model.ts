import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";


@Injectable({
  providedIn: 'root'
})
export class LoanManagementModel {

  application_gid : any;

  girdViewLoop = ['1', '2', '3', '4','5','6','7','8'];
  activeCardViewLoop = ['1', '2', '3', '4','5','6','7','8'];
   /* Array of Side menu contents (Quick links) */
   ApplicationCreationMenu= [
    {
      "id": 1,
      "title": "General-Details",
      subMenu: []
    },    
    {
      "id": 2,
      "title": "Borrower Details",
      subMenu: [
        
        {
          subMenuTitle:'Institution',
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'GST',
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'Address',
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'Contact Person',
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'Genetic code by Business', 
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'Other',  
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'License', 
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'FPO Coverage Area',
          hasError:false,
          completed:false,
        },
      ]
    },
    {
      "id": 3,
      "title": "Stakeholder Details", 
      "subMenu": [
         {
        subMenuTitle:'List',
        hasError:false,
        completed:false,
      },
        {
          subMenuTitle:"Stakeholder",
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:"GST",
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:"Address",
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:"Contact Person",
          hasError:false,
          completed:false,

        },
        
     

      ]
    },
    {
      "id": 4,
      "title": "Facility & Charges",
      "subMenu": [
        {
          subMenuTitle:"Limit Details",
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:"Charges",
          hasError:false,
          completed:false,
        }
      ]
    },
    {
      "id": 5,
      "title": "Documents",
      "subMenu": [
        {
          subMenuTitle:"Borrower Documents",
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:"Stakeholder Documents",
          hasError:false,
          completed:false,
        }
      ]
    },
    {
      "id": 6,
      "title": "Bureau",
      "subMenu": [
        {
          subMenuTitle:"Borrower Summary",
          hasError:false,
          completed:false,
        },
        {          
          subMenuTitle:"Stakeholder Summary",
          hasError:false,
          completed:false,
        }
      ]
    },
    {
      "id": 7,
      "title": "Submission",
      "subMenu": []
    }
  ];
   /* these following boolean variables are used to indicate the form sections and forms completed state */
  isGeneralInformationCompleted:boolean = false; 
  isSPoCDetailsCompleted:boolean = false; 
  isBusinessActivitiesCompleted:boolean = false; 
  isGeneralDetailsFormSubmitted:boolean = false;
  isLoginPageActive = false;
  tableData= [];
  productTableData= [];
  jsonObjectApplication = {

  }
    headerTabs =[
        {
          id:1,
          tabName:'Draft',
          active : false
        },
        {
          id:2,
          tabName:'Active',
          active : false
        },
        {
          id:3,
          tabName:'On-Hold',
          active : false
        },
        {
          id:4,
          tabName:'Rejected',
          active : false
        }
      ]
      applicationActiveNewTableHeader = [
        {
            id:1,
            title: 'Application Number'
        },
        {
          id:2,
          title: 'Applicant Name'
        },
        {
          id:3,
          title: 'Program'
        },
        {
          id:4,
          title: 'Vertical'
        },
        {
          id:5,
          title: 'Overall Limit'
        },
        {
          id:6,
          title: 'Aging'
        },
        {
          id:7,
          title: 'Status'
        },
      ]
      applicationActiveRenewalEnhancementTableHeader = [
        {
            id:1,
            title: 'Application Number'
        },
        {
          id:2,
          title: 'Applicant Name'
        },
        {
          id:3,
          title: 'Program'
        },
        {
          id:4,
          title: 'Vertical'
        },
        {
          id:5,
          title: 'Existing Limit'
        },
        {
          id:6,
          title: 'New Limit'
        },
        {
          id:7,
          title: 'Aging'
        },
        {
          id:8,
          title: 'Status'
        }
      ]

      applicationActiveTableData = [
        {
          id :1,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Mukilan Samunnati Financial Intermediation & Services Private Limited Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :2,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :3,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :4,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :5,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :6,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :7,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :8,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :9,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :10,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :11,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :12,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :13,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :14,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :15,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :16,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :17,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :18,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :19,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        },
        {
          id :20,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Pending-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          submittedOn : '28-Mar-2022 ',
          aging : '2 Days',
          status : 'Submitted to Approval'
        }
      ];
    
      
      applicationTypesTitle = [
        {
          id:1,
          type : 'New'
        },
        {
          id:2,
          type : 'Renewal'
        },
        {
          id:3,
          type : 'Enhancement'
        },
      ];

      applicationCategoryTitle = [
        {
          id:1,
          type : 'Business',
          count: 86
        },
        {
          id:2,
          type : 'Credit',
          count: 20
        },
        {
          id:3,
          type : 'CC',
          count:18
        },
      ];

      applicationDraftTableHeaderArr = [
        {
          id:1,
          title: 'Application Number'
        },
        {
          id:2,
          title: 'Applicant Name'
        },
        {
          id:3,
          title: 'Program'
        },
        {
          id:4,
          title: 'Vertical'
        },
        {
          id:5,
          title: 'Existing Limit'
        },
        {
          id:6,
          title: 'New Limit'
        },
        {
          id:7,
          title: 'Aging'
        },
        // {
        //   id:8,
        //   title: 'Status'
        // },
        {
          id:9,
          title: 'Progress'
        }
      ]

      applicationDraftNewTableHeaderArr = [
        {
          id:1,
          title: 'Application Number'
        },
        {
          id:2,
          title: 'Applicant Name'
        },
        {
          id:3,
          title: 'Program'
        },
        {
          id:4,
          title: 'Vertical'
        },
        {
          id:5,
          title: 'Overall Limit'
        },
        {
          id:6,
          title: 'Aging'
        },
        // {
        //   id:7,
        //   title: 'Status'
        // },
        {
          id:8,
          title: 'Progress'
        }
      ]

      applicationDraftTableData = [
        {
          id :1,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Mukilan Samunnati Financial Intermediation & Services Private Limited Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Former Producer Organisation',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :2,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Mukilan Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'Renewal'
        },
        {
          id :3,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Mukilan Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'Enhancement'
        },
        {
          id :4,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Muviereck Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :5,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Muviereck Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :6,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Muviereck Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :7,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Jerald Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :8,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Jerald Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :9,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Jerald Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :10,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Jerald Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :11,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Vinoth Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :12,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Vinoth Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :13,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Vinoth Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :14,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Vinoth Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :15,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :16,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :17,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited Mukilan',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :18,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :19,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        },
        {
          id :20,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          overallLimit : '₹ 1,05,00,000',          
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          vertical : 'Agri Enterprises',
          applicantType : 'Individual',
          createdOn : '26-Mar-2022',
          aging : '2 Days',
          progress : '70',
          status : 'Submitted to Approval',
          categoryButton: 'New'
        }
      ]
      
      applicationOnHoldNewTableHeader = [
        {
            id:1,
            title: 'Application Number'
        },
        {
          id:2,
          title: 'Applicant Name'
        },
        {
          id:3,
          title: 'Program'
        },
        {
          id:4,
          title: 'Vertical'
        },
        {
          id:5,
          title: 'Overall Limit'
        },
        {
          id:6,
          title: 'Hold Date'
        },
        {
          id:7,
          title: 'Status'
        },
      ]
      applicationOnHoldRenewalEnhancementTableHeader = [
        {
            id:1,
            title: 'Application Number'
        },
        {
          id:2,
          title: 'Applicant Name'
        },
        {
          id:3,
          title: 'Program'
        },
        {
          id:4,
          title: 'Vertical'
        },
        {
          id:5,
          title: 'Existing Limit'
        },
        {
          id:6,
          title: 'New Limit'
        },
        {
          id:7,
          title: 'Hold Date'
        },
        {
          id:8,
          title: 'Status'
        }
      ]

      applicationOnHoldTableData = [
        {
          id :1,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Mukilan Samunnati Financial Intermediation & Services Private Limited Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :2,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :3,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :4,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :5,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :6,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :7,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :8,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :9,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :10,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :11,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :12,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :13,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :14,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :15,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :16,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          holdOn : '28-Mar-2022 ',
          status : 'Submitted to Approval'
        },
        {
          id :17,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :18,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :19,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :20,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Hold-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
          
          holdOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        }
      ];
    
      applicationRejectedNewTableHeader = [
        {
            id:1,
            title: 'Application Number'
        },
        {
          id:2,
          title: 'Applicant Name'
        },
        {
          id:3,
          title: 'Program'
        },
        {
          id:4,
          title: 'Vertical'
        },
        {
          id:5,
          title: 'Overall Limit'
        },
        {
          id:6,
          title: 'Rejected Date'
        },
        {
          id:7,
          title: 'Status'
        },
      ]
      applicationRejectedRenewalEnhancementTableHeader = [
        {
            id:1,
            title: 'Application Number'
        },
        {
          id:2,
          title: 'Applicant Name'
        },
        {
          id:3,
          title: 'Program'
        },
        {
          id:4,
          title: 'Vertical'
        },
        {
          id:5,
          title: 'Existing Limit'
        },
        {
          id:6,
          title: 'New Limit'
        },
        {
          id:7,
          title: 'Rejected Date'
        },
        {
          id:8,
          title: 'Status'
        }
      ]

      applicationRejectedTableData = [
        {
          id :1,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Mukilan Samunnati Financial Intermediation & Services Private Limited Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :2,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :3,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :4,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :5,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :6,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :7,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :8,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :9,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :10,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :11,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :12,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :13,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :14,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :15,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :16,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :17,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :18,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :19,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        },
        {
          id :20,
          applicationNumber : 'ARNSF06080320220296IN01',
          applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
          vertical : 'Farmer Producer Organization',
          program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
          label : 'Rejected-DRM',
          new : '₹ 1,05,00,000',
          existing : '₹ 1,05,00,000',
          overallLimit : '₹ 1,05,00,000',
          applicantType : 'Individual',
        
          rejectedOn : '28-Mar-2022 ',
        
          status : 'Submitted to Approval'
        }
      ];

      /**General Info Details Data */

      verticals = [
        {
          vertical : 'Trade'
        },
        {
          vertical : 'Retail'
        },
        {
          vertical : 'Former Producer Organaization'
        },
        {
          vertical : 'F1 Leanding'
        },
        {
          vertical : 'FPO AC'
        },
        {
          vertical : 'Agrienterprises'
        }
      ]
      programs = [
        {
          program : 'Exchange LED'
        },
        {
          program : 'HORECA'
        },
        {
          program : 'Regular(AE)'
        },
      ];

      
  creditGroups = [
    {
      creditGroup : 'SAN - Input - Incubation'
    },
    {
      creditGroup : 'CUG-2-MSP'
    },
    {
      creditGroup : 'IT Internal Use - Do not select this'
    },
    {
      creditGroup : 'Market Linkage'
    },
    {
      creditGroup : 'EXIM'
    },
    {
      creditGroup : 'STF'
    },
    {
      creditGroup : 'Trade-Regular'
    },
    {
      creditGroup : 'Trade-Fast track'
    },
    {
      creditGroup : 'Trade- Modern Retail'
    }
  ];
  
  languages = [
    {
      language :  'Tamil'
    },
    {
      language : 'English'
    },
    {
      language : 'Malayalam'
    },
    {
      language : 'Hindi'
    }

  ];
  samNames = [
    {samName:    'Name 1'},
    {samName:    'Name 2'},
    {samName:    'Name 3'},
    {samName:    'Name 4'},
    {samName:    'Name 5'}
  ];

  products = [
    'Product 1',
    'Product 2',
    'Product 3',
    'Product 4'
  ];

  verities = [
    'Variety 1',
    'Variety 2',
    'Variety 3',
    'Variety 4'
  ]

  designations = [
    {
      designation : 'Trade'
    },
    {
      designation : 'Retail'
    },
    {
      designation : 'Former Producer Organaization'
    },
    {
      designation : 'F1 Leanding'
    },
    {
      designation : 'FPO AC'
    },
    {
      designation : 'Agrienterprises'
    }
  ];

  contactsType = [
    'Mobile Number',
    'E-Mail'
  ]
  generalInfoCardTitles = [
    'General Details','SPOC Details','Business Activities'
  ];
  businessActivitiesTableHeader =[
    'S.No','Product','Variety','SBU','Category','Action'
  ]
  // Stakeholder Company name
  Companyname = [
    {
      code:'Samunnati Financial Intermediation and Service Private Limited',
      status:false,
      observation: ''
    },
    {
      code: 'Samunnati Financial Intermediation and Service Private Limited',
      status:false,
      observation: ''
    },
    {
      code: 'Samunnati Financial Intermediation and Service Private Limited',
      status:false,
      observation: ''
    },
    {
      code: 'Samunnati Financial Intermediation and Service Private Limited',
      status:false,
      observation: ''
    }
  ]

  PANname = [
    {
      code:'BIMPY3665K',
      status:false,
      observation: ''
    },
    {
      code: 'BIMPY3665K',
      status:false,
      observation: ''
    },
    {
      code: 'BIMPY3665K',
      status:false,
      observation: ''
    },
    {
      code: 'BIMPY3665K',
      status:false,
      observation: ''
    }
  ]
  /**Borrower details Institution page */

  geneticCodes = [
    {
      code:'How are we meeting the requirements of the customer through supply of the commodity?',
      status:false,
      observation: ''
    },
    {
      code: 'What is the market feedback on the company as per business team?',
      status:false,
      observation: ''
    },
    {
      code: 'Do we know the management of the company directly?',
      status:false,
      observation: ''
    },
    {
      code: 'How is the sourcing done?',
      status:false,
      observation: ''
    },
    {
      code: 'Either the Buyer or Seller is strong and creditable player in the value chain.',
      status:false,
      observation: ''
    },
    {
      code: 'DAS / Samunnati having first access to cash-flows (Escrow) / Mortgage (as applicable).',
      status:false,
      observation: ''
    },
    {
      code: 'Seller being the Borrower and Buyer being the Guarantor.',
      status:false,
      observation: ''
    },
    {
      code: 'Transaction history between the Buyer and Seller.',
      status:false,
      observation: ''
    },
    {
      code: 'Applying the buyer dynamics from the Social Capital and Trade Capital perspective.',
      status:false,
      observation: ''
    }
  ]
  CalamitiesProneCities = [
    'option 1','option 2','option 4','option 5','option 6','option 7'
  ]


  mobileNumdata:any[]= [
    {
      mobNumber : 'null',
      WhatsappNumber : 'null',
      PrimaryStatus : 'null'
    }
  ];

  companyType = [
    'Registered', 'Un Registered'
  ];
  creditRatingAgency = [
    {agencyName:'Fitch'},
    {agencyName:'CRISIL'},
    {agencyName:'ICRA'},
    {agencyName:'ONICRA'},
    {agencyName:'SMERA'},
    {agencyName:'Others'},
    {agencyName:'CARE'},
    {agencyName:'TEST'}
  ]
  
  stakeholderType = [
    'Applicant',
    'Guarantor',
    'Member',
    'Applicant'
  ]
  creditRating = [
    {creditRate:'AAA+'}, {creditRate:'AAA'}, {creditRate:'AAA-'},
   { creditRate:'AA+'}, {creditRate:'AA'}, {creditRate:'AA-'},
    {creditRate:'A+'}, {creditRate:'A'}, {creditRate:'A-'},
    {creditRate:'BBB+'},{creditRate:'BBB'},{creditRate:'BBB-'},
    {creditRate:'BB+'}, {creditRate:'BB'}, {creditRate:'BB-'},
    {creditRate:'B+'}, {creditRate:'B'}, {creditRate:'B-'}
  ]
  AMLCategory = [
    {aml:'High'}, {aml:'Medium'}, {aml:'Low'}, {aml:'Not Applicable'}
  ]

  TANCategory = [
    {tan:'Tamil Nadu'}, {tan:'Kerala'}, {tan:'Andhra Pradesh'}, {tan:'Karnataka'}
  ]
  
  businessCategory = [
    {category:'Others'}, {category:'Large'}, {category:'Medium'}, {category:'MSME'}, {category:'SME'}, {category:'Micro'}
  ]
  borrowerInstitutionDesignations = [
    {designation:'Accountant'}, {designation:'Director'}, {designation:'Adminstrative Assistant'}, {designation:'Business Analyst'},  {designation:'CEO'}, {designation:'CFO'}, {designation:'CMO'}, {designation:'COO'}, {designation:'CTO'},
    {designation:'Executive'}, {designation:'Executive Assistant'}, {designation:'Joint Director'}, {designation:'Manager'}, {designation:'Manager - Finance'}, {designation:'Manager - HR'},
    {designation:'Manager - Marketing'},{designation:' Manager - Project'}, {designation:'President'}, {designation:'Representative - Customer loanManagementModel'},
    {designation:' Representative - Sales'},{ designation:'Vice President'},{ designation:'Partner'}, {designation:'Quality Manager1' }
  ]

  IndividualPANstatus = [
    {panstatus:'Customer Submitting PAN'}, {panstatus:'Customer Submitting Form 60'}   
  ]
  addressTypes = [
    {type:'Branch office'}, {type:'Regional office'}, {type:'WareHouse'}, {type:'Plant Address'}, {type:'Factory Address'}, {type:'Others'}, {type:'Registered Office'}, {type:'Current Address'}, {type:'Permenant Address'}
  ]
  filteredSourceTypes = [
    {filteredSourceTypes:'Fixed'}, {filteredSourceTypes:'Moving'}  , {filteredSourceTypes:'Property'}, {filteredSourceTypes:'Deposit'}  
  ];
  samunnatiBranches = [
    {branchName:'Chennai'}, {branchName:'TKallupatti'}, {branchName:'Katpadi'}, {branchName:'Pooriyambakkam'}, {branchName:'Ahmadabad'}, {branchName:'Salem'}, {branchName:'thiruvanamalai'}, {branchName:'Santha vasal'}, {branchName:'Palamaner'}, {branchName:'Madurai'}, {branchName:'Pune'}, {branchName:'Kilpenathur'}, {branchName:'Cholingur'}, {branchName:'Tirukoilur'}, {branchName:'Naydumangalam' }
  ]
  previousCrops: any = [
    'Food Crop','Horticulture Crop','Commercial Crop'
  ];
  states = [
    {state:'Andhra Pradesh'},
    {state:'Arunachal Pradesh'}, {state:'Assam'}, {state:'Bihar'}, {state:'Chhattisgarh'}, {state:'Goa'}, {state:'Gujarat'}, {state:'Haryana'}, {state:'Himachal Pradesh'},{ state:'Jammu and Kashmir'}, {state:'Jharkhand'}, {state:'Karnataka'}, {state:'Kerala'}, {state:'Madhya Pradesh'}, {state:'Maharashtra'}, {state:'Manipur'}, {state:'Meghalaya'}, {state:'Mizoram'}, {state:'Nagaland'}, {state:'Odisha (Orissa)'},{ state:'Punjab'},{ state:'Rajasthan'}, {state:'Sikkim'}, {state:'Tamil Nadu'}, {state:'Telangana'}, {state:'Tripura'}, {state:'Uttar Pradesh'} , {state:'Uttarakhand'},
   { state:'New Delhi'}
  ]
  equiqmentNames = [
    'Manual Crop Cutting Tool'
  ]
  liveStokeNames = [
    'Mules', 'Goats', 'Horses'
  ]
  licenseTypes = [
    {type:'Fertilisers'}, {type:'Trade'}, {type:'Pesticides'}, {type:'Seed'}, {type:'FSSAI'}
  ]

  NonCalamitiesProneCities = [
    'option 1','option 2','option 4','option 5','option 6','option 7'
  ]

  borrowerDetailsInstitutionCardTitles = [
    'Institution','GST','Address','Contact Person','Genetic code by Business', 'Other',
    'License', 'FPO Coverage Area'
  ];

  gstTableHeader = [
    {
      title:'S.No',
    },
    {
      title:'Source',
    },
    {
      title:'GST Number',
    },
    {
      title:'GST State',
    },
    {
      title:'Head Office',
    },
    {
      title:'Action',
    }
  ]
  gstTableHeader1 = [
    {
      title:'S.No',
    },
    {
      title:'GST Number',
    },
    {
      title:'GST State',
    },
    {
      title:'Head Office',
    },
    {
      title:'Action',
    }
  ]
  
  contactTableHeader = [
    'S.No', 'Type', 'Particulars', 'Primary Status', 'Action'
  ]
  
  documentuploadHeader = [
    'S.No','Title','Value','Attachment','Action'
  ]
  
  geneticCodeHeader = [
    'S.No', 'Genetic Code', 'Status', 'Observation(s)'
  ]
  
  equipmentTableHeader = [
    'S.No','Name','Count','Insurance Status','Description','Can Be Rented','Action'
  ]
  
  liveStokeTableHeader = [
    'S.No','Name','Count','Insurance Status','Description','Action'
  ]
  BorrowerHeader =[
    'S.No','Borrower Name','Type','Bureau','Score','Verified on','Request','Action'
  ]
  StakeholderHeader =[
    'S.No','Stakeholder Name','Type','Bureau','Score','Verified on','Request','Action'
  ]
  
  licenseTableHeader = [
    'S.No','Type','Number','Issue Date','Expiry Date','Action'
  ]
  
  fpoCoverageAreaTableHeader = [
    'Calamities Prone Cities', 'Non Calamities Prone Cities'
  ]
  
  gstselectedOption: any = {
    id: 1,
    name: 'Madhya Pradesh',
  };
 
  primaryAddressDetails = [
    {
      id: '1',
      registeredAddress: '14,15,16 and 32, Laxmibai Nagar, Industrial Area, Madhya Pradesh, pin: 452006',
      natureOfBusiness: 'Factory / Manufacturing, Export, Office / Sale Office, Recipient of Goods or Services, Wholesale Business, Import, Warehouse / Depot.',
      registeredEmail: 'siddharth.p@samunnati.com',
      mobileNumber: '+91 8925527259',
      lastUpdated: '10-Mar-2023'
    },
    {
      id: '2',
      registeredAddress: '14,15,16 and 32, Laxmibai Nagar, Industrial Area, Madhya Pradesh, pin: 452006',
      natureOfBusiness: 'Factory / Manufacturing, Export, Office / Sale Office, Recipient of Goods or Services, Wholesale Business, Import, Warehouse / Depot.',
      registeredEmail: 'siddharth.p@samunnati.com',
      mobileNumber: '+91 8925527259',
      lastUpdated: '10-Mar-2023'
    }
  ]
  
  additionalAddressDetails = [
    {
      id: '1',
      registeredAddress: '14,15,16 and 32, Laxmibai Nagar, Industrial Area, Madhya Pradesh, pin: 452006',
      natureOfBusiness: 'Factory / Manufacturing, Export, Office / Sale Office, Recipient of Goods or Services, Wholesale Business, Import, Warehouse / Depot.',
    },
    {
      id: '2',
      registeredAddress: '14,15,16 and 32, Laxmibai Nagar, Industrial Area, Madhya Pradesh, pin: 452006',
      natureOfBusiness: 'Factory / Manufacturing, Export, Office / Sale Office, Recipient of Goods or Services, Wholesale Business, Import, Warehouse / Depot.',
    },
    {
      id: '3',
      registeredAddress: '14,15,16 and 32, Laxmibai Nagar, Industrial Area, Madhya Pradesh, pin: 452006',
      natureOfBusiness: 'Factory / Manufacturing, Export, Office / Sale Office, Recipient of Goods or Services, Wholesale Business, Import, Warehouse / Depot.',
    }
  ]
  dataFromGSTportal = 
  
  {
    gstSource:'GST Portal',
    gstNumber:'12345GHJS',
    gstState:'Tamil Nadu',
    gstHeadOffice:true,
    gstfromapi:'Y'
  }
  /**Borrower details individual page*/

borrowerdetailsIndividualcardTitles = [
  'Basic','Address','Contact','Occupation','Family','Genetic code by Business', 'Other',

]
genders = [
  'Male', 'Female', 'Others'
];

martialStatusArray = [
  'Option 1', 'Option 2'
];
Form_60Array = [
  'My annual income is below 2,50,000', 'I am an agriculturist by profession and my non-agricultural income does not exceed 2,50,000','I reside in the notified district of Jammu and Kashmir. Further, I have earned income sourced only from these states, which is exempt under the Income-tax Act, 1961','I reside in the notified district of the north eastern states. Further, I have earned income sourced only from these states, which is exempt under the Income-tax Act, 1961'
];
physicalStatusArray = [
  'Fit and healthy',
'Overweight',
'Underweight',
'Physically challenged',
'Recovering from an injury or illness',
'Pregnant',
'Elderly'
];
nearestSamunnatiBranches = [
  {branchName:'Chennai'}, {branchName:'TKallupatti'}, {branchName:'Katpadi'}, {branchName:'Pooriyambakkam'}, {branchName:'Ahmadabad'}, {branchName:'Salem'}, {branchName:'thiruvanamalai'}, {branchName:'Santha vasal'}, {branchName:'Palamaner'}, {branchName:'Madurai'}, {branchName:'Pune'}, {branchName:'Kilpenathur'}, {branchName:'Cholingur'}, {branchName:'Tirukoilur'}, {branchName:'Naydumangalam' }
];
educationalQualifications: any = [
  'Option 1','Option 2','Option 3','Option 4'
];
incomeTypes: any = [
  'Option 1','Option 2','Option 3','Option 4'
];

Proposedcrop: any = [
  'Option 1','Option 2','Option 3','Option 4'
];

landOwnershipTypes: any = [
  'Option 1','Option 2','Option 3','Option 4'
];


landNames: any = [
  'Option 1','Option 2','Option 3','Option 4'
];

residenceTypes: any = [
  'Option 1','Option 2','Option 3','Option 4'
];


relationshipTypes: any = [
   'Mother', 'Sister', 'Brother', 'Daughter'
];
familyDetailsHeader = [
  'Relationship Type', 'First Name', 'Middle Name', 'Last Name', 'Nominee', 'Date of Birth', 'Age'
]

/**Beauru Details */
stackholderDetails = [
  {id: '1', stakeholderName: 'CHANDAN THYAVANAHALLI MANJUNATHA', name: 'Hignmark', score: '523', verifiedon: '10-Oct-2022', request: 'Initiated'},
  {id: '2', stakeholderName: 'JANAKIRAMAN', name: 'Hignmark', score: '550', verifiedon: '10-Oct-2022', request: 'Initiated'},
  {id: '3', stakeholderName: 'DIVYA', name: 'Hignmark', score: '580', verifiedon: '10-Oct-2022', request: 'Initiated'},
  {id: '4', stakeholderName: 'NIKHIL', name: 'Hignmark', score: '650', verifiedon: '10-Oct-2022', request: 'Initiated'},
  {id: '5', stakeholderName: 'SIDDHARTH', name: 'Hignmark', score: '700', verifiedon: '10-Oct-2022', request: 'Initiated'},
]

borrowertableDetails = [
{
  id:'1',
  borrowerName:'CHANDAN THYAVANAHALLI MANJUNATHA',
  bureau:'-',
  score:'-',
  verifiedOn:'-',
  request:false
},
{
  id:'2',
  borrowerName:'CHANDAN THYAVANAHALLI MANJUNATHA',
  bureau:'High Mark',
  score:'600',
  verifiedOn:'10-Oct-2022',
  request:true
},
{
  id:'3',
  borrowerName:'CHANDAN THYAVANAHALLI MANJUNATHA',
  bureau:'High Mark',
  score:'600',
  verifiedOn:'10-Oct-2022',
  request:true
}
]

/**Facilities and charges */
facilitiesAndChargesCardTitles:any = [
  'Limit Details','Charges'
];

collateralDocumentUploadTitle = ['Title 1', 'Title 2', 'Title 3'];
productDetailsData = ['Product 1', 'Product 2', 'Product 3'];
  productDetailsVariety = ['Variety 1', 'Variety 2', 'Variety 3'];
  
  collatralDocumentUploadTableHeader = [    'S.No','Title','Attachment','Action'  ] 
  
  hypothecationDocumentUploadTableHeader = [    'S.No','Title','Attachment','Action'  ] 

  interestFrequencies = ['Interest frequency 1', 'Interest frequency 2', 'Interest frequency 3'];
 
  charges = [
    'Processing Fee / Initiation Fee',
    'Term Life Insurance',
    'Adhoc Fee',
    'Field Visit Charges',
    'Documentation Charges',
    'Personal Accident Insurance'
  ]

  productTypes = ['option 1', 'option 2', 'option 3'];
  facilityTypes = ['Facility type 1', 'Facility type 2', 'Facility type 3'];
  facilityPurposeTypes = ['Facility sub type 1', 'Facility sub type 2', 'Facility sub type 3'];
  hypothecationDetailsTitleTypes = ['Facility sub type 1', 'Facility sub type 2', 'Facility sub type 3'];
  
  //moratoriumTypes = ['Moratorium type 1', 'Moratorium type 2', 'Moratorium type 3'];
  
  moratoriumTypes = [
  {moratoriumTypes:'Principal'}, {moratoriumTypes:'Principal and interest'}   
]
filterFacility = [
  {filterFacility:'New'}, {filterFacility:'Existing'}   
]
Facilitymode = [
  {Facilitymode:'Revolving'}, {Facilitymode:'Non Revolving'} ,{Facilitymode:'Not Applicable'}  
]
  sourceTypes = ['Source type 1', 'Source type 2', 'Source type 3'];
  securityTypes = ['Security type 1', 'Security type 2', 'Security type 3'];
  principalFrequencies = ['Principle frequency 1', 'Principle frequency 2', 'Principle frequency 3'];
  
/**Stackholder individual */
addressObj = {id:1, type : 'Branch',  address : 'Baid Hitech Park, 129-B, 8th Floor, ECR, Thiruvanmiyur, Chennai-600042', lanmark : 'Chruch'};
tableAddress = [
  {id:1, type : 'Branch',  address : 'Baid Hitech Park, 129-B, 8th Floor, ECR, Thiruvanmiyur, Chennai-600042', lanmark : 'Chruch'}
]
addressDropdown = [
  {
    address : 'Trade'
  },
  {
    address : 'Retail'
  },
  {
    address : 'Former Producer Organaization'
  },
  {
    address : 'F1 Leanding'
  },
  {
    address : 'FPO AC'
  },
  {
    address : 'Agrienterprises'
  }]
  StakeholderSelectionTableHeader = [
    'S.No','Full Name','PAN','Designation','Guarantor/ Co-applicant','Status','Action'
  ]
  StakeholderListSelectionTableHeader = [
    'S.No','Company Name','PAN','Guarantor','Status','Action'
  ]
  stackholderIndividualCardDetails =  [
    'List','StakeHolder Details'
  
  ];
  documentCardDetails =  [
    'Borrower Documents','Stakeholder Documents'
  
  ];
  DocTitles = [
  {title:'CBO / FIG / FPO incorporation'}, {title:'Partnership Deed'}, {title:'ITR Year 3'}, {title:'1 Year Bank Statement'}, {title:'YTD Sales'}, {title:'PAN'}, {title:'GST'} ,{title:'TIN'}, {title:'Registration Number'}, {title:'Utility Bill'}, {title:'Bank statement for the last financial year'}, {title:'Monthwise Sales Data YTD'}, {title:'GST for the last financial year'}, {title:'GST YTD'}, {title:'YTD P & L'}, {title:'CMA'}, {title:'Latest 3 months stock statement'}, {title:'Debtors Ageing latest month end.'}, {title:'Creditors Ageing Analysis (fiscal year end )- for Trade lines'}, {title:'Social Capital information - as per CAM format'}, {title:'Existing Sanction Letter of other Banks'}, {title:'Other documents'}, {title:'M &AoA'},{title:'ITR Year 1'},{title:'ITR Year 2'}, {title:'YTD Bank statement'}, {title:'Monthwise Sales Data for the last financial year'},{title:'Product wise break up of revenues (last 2 fiscal)'}, {title:'Debtors Ageing Analysis Fiscal year end'}, {title:'Trade Capital Information - as per CAM format'}, {title:'Analysis Fiscal year end'}
]
internalRating = [
  {internalRatingName:'Foundation IRB'}
]
stackholderInstitutionCardTitles = [
  'Stakeholder','GST','Address','Contact Person'
];
headerOptionsArr: any = [
  {
    id: 1,
    name: 'Madhya Pradesh',
  },
  {
    id: 2,
    name: 'Commerce',
  },
  {
    id: 3,
    name: 'Foundation',
  },
];
CoApplicantArray = [
  'option 1','option 2','option 3',
  'option 4','option 5','option 6',
]
      baseUrl = 'http://localhost:3000';
  borrowerIndividualPANstatus: any;
    
    constructor(private http:HttpClient){
      
    }

    setapplication_gid(app_gid:any){
      this.application_gid = app_gid;
    }

}