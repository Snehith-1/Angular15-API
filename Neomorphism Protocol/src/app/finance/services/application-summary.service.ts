import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApplicationSummaryService {
  headerTabs: any;
  
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
    {
      id:8,
      title: 'Progress'
    },

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
    {
      id:7,
      title: 'Progress'
    },

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
    }
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
  applicationCategoryTitle:any;

  constructor() { }
}
