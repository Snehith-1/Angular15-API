import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class HomeService {

    // financeData = [
    //     {
    //         "id":1,
    //         "menuName" : 'Business 1',
    //         "icon": '/assets/financeIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":2,
    //         "menuName" : 'Credit 1',
    //         "icon": '/assets/creditIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":3,
    //         "menuName" : 'Committee 1',
    //         "icon": '/assets/CCIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":4,
    //         "menuName" : 'Credit Administration 1',
    //         "icon": '/assets/CADIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":5,
    //         "menuName" : 'Customer Details 1',
    //         "icon": '/assets/customerDetailsIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":6,
    //         "menuName" : 'Reports 1',
    //         "icon": '/assets/reportIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     }
    // ];

    // commerceData = [
    //     {
    //         "id":1,
    //         "menuName" : 'Business 2',
    //         "icon": '/assets/financeIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":2,
    //         "menuName" : 'Credit 2',
    //         "icon": '/assets/creditIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":3,
    //         "menuName" : 'Committee 2',
    //         "icon": '/assets/CCIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":4,
    //         "menuName" : 'Credit Administration 2',
    //         "icon": '/assets/CADIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":5,
    //         "menuName" : 'Customer Details 2',
    //         "icon": '/assets/customerDetailsIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":6,
    //         "menuName" : 'Reports 2',
    //         "icon": '/assets/reportIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     }
    // ];

    // foundationData = [
    //     {
    //         "id":1,
    //         "menuName" : 'Business 3',
    //         "icon": '/assets/financeIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":2,
    //         "menuName" : 'Credit 3',
    //         "icon": '/assets/creditIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":3,
    //         "menuName" : 'Committee 3',
    //         "icon": '/assets/CCIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":4,
    //         "menuName" : 'Credit Administration 3',
    //         "icon": '/assets/CADIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":5,
    //         "menuName" : 'Customer Details 3',
    //         "icon": '/assets/customerDetailsIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":6,
    //         "menuName" : 'Reports 3',
    //         "icon": '/assets/reportIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     }
    // ];

    // commonData = [
    //     {
    //         "id":1,
    //         "menuName" : 'Business 4',
    //         "icon": '/assets/financeIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":2,
    //         "menuName" : 'Credit 4',
    //         "icon": '/assets/creditIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":3,
    //         "menuName" : 'Committee 4',
    //         "icon": '/assets/CCIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":4,
    //         "menuName" : 'Credit Administration 4',
    //         "icon": '/assets/CADIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":5,
    //         "menuName" : 'Customer Details 4',
    //         "icon": '/assets/customerDetailsIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":6,
    //         "menuName" : 'Reports 4',
    //         "icon": '/assets/reportIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     }
    // ];

    // masterData = [
    //     {
    //         "id":1,
    //         "menuName" : 'Business 5',
    //         "icon": '/assets/financeIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":2,
    //         "menuName" : 'Credit 5',
    //         "icon": '/assets/creditIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":3,
    //         "menuName" : 'Committee 5',
    //         "icon": '/assets/CCIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":4,
    //         "menuName" : 'Credit Administration 5',
    //         "icon": '/assets/CADIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":5,
    //         "menuName" : 'Customer Details 5',
    //         "icon": '/assets/customerDetailsIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":6,
    //         "menuName" : 'Reports 5',
    //         "icon": '/assets/reportIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     }
    // ];

    // settingData = [
    //     {
    //         "id":1,
    //         "menuName" : 'Business 6',
    //         "icon": '/assets/financeIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":2,
    //         "menuName" : 'Credit 6',
    //         "icon": '/assets/creditIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":3,
    //         "menuName" : 'Committee 6',
    //         "icon": '/assets/CCIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":4,
    //         "menuName" : 'Credit Administration 6',
    //         "icon": '/assets/CADIconGreen.svg',
    //         "ennableState" : true,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":5,
    //         "menuName" : 'Customer Details 6',
    //         "icon": '/assets/customerDetailsIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     },
    //     {
    //         "id":6,
    //         "menuName" : 'Reports 6',
    //         "icon": '/assets/reportIcon.svg',
    //         "ennableState" : false,
    //         "activeState" : true,
    //         "subMenu" : [
    //             {
    //                 "subMenuId" : 1,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 2,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 3,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             },
    //             {
    //                 "subMenuId" : 4,
    //                 "subMenuTitle": 'Create Application',
    //                 "ChildSubMenu" : ["New Application","Buyer","Supplier","Credit Buyer","Customer"]
    //             }
    //         ]
    //     }
    // ];

    // headerTabs =[
    //     {
    //       id:1,
    //       tabName:'Draft',
    //       active : false
    //     },
    //     {
    //       id:2,
    //       tabName:'Active',
    //       active : false
    //     },
    //     {
    //       id:3,
    //       tabName:'On-Hold',
    //       active : false
    //     },
    //     {
    //       id:4,
    //       tabName:'Rejected',
    //       active : false
    //     }
    //   ]
    //   applicationActiveNewTableHeader = [
    //     {
    //         id:1,
    //         title: 'Application Number'
    //     },
    //     {
    //       id:2,
    //       title: 'Applicant Name'
    //     },
    //     {
    //       id:3,
    //       title: 'Program'
    //     },
    //     {
    //       id:4,
    //       title: 'Vertical'
    //     },
    //     {
    //       id:5,
    //       title: 'Overall Limit'
    //     },
    //     {
    //       id:6,
    //       title: 'Aging'
    //     },
    //     {
    //       id:7,
    //       title: 'Status'
    //     },
    //   ]
    //   applicationActiveRenewalEnhancementTableHeader = [
    //     {
    //         id:1,
    //         title: 'Application Number'
    //     },
    //     {
    //       id:2,
    //       title: 'Applicant Name'
    //     },
    //     {
    //       id:3,
    //       title: 'Program'
    //     },
    //     {
    //       id:4,
    //       title: 'Vertical'
    //     },
    //     {
    //       id:5,
    //       title: 'Existing Limit'
    //     },
    //     {
    //       id:6,
    //       title: 'New Limit'
    //     },
    //     {
    //       id:7,
    //       title: 'Aging'
    //     },
    //     {
    //       id:8,
    //       title: 'Status'
    //     }
    //   ]

    //   applicationActiveTableData = [
    //     {
    //       id :1,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :2,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :3,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :4,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :5,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :6,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :7,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :8,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :9,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :10,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :11,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :12,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :13,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :14,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :15,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :16,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :17,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :18,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :19,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :20,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Pending-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       submittedOn : '28-Mar-2022 ',
    //       aging : '2 Days',
    //       status : 'Submitted to Approval'
    //     }
    //   ];
    
      
    //   applicationTypesTitle = [
    //     {
    //       id:1,
    //       type : 'New'
    //     },
    //     {
    //       id:2,
    //       type : 'Renewal'
    //     },
    //     {
    //       id:3,
    //       type : 'Enhancement'
    //     },
    //   ];

    //   applicationCategoryTitle = [
    //     {
    //       id:1,
    //       type : 'Business (86)'
    //     },
    //     {
    //       id:2,
    //       type : 'Credit (20)'
    //     },
    //     {
    //       id:3,
    //       type : 'CC (18)'
    //     },
    //   ];

    //   applicationDraftTableHeaderArr = [
    //     {
    //       id:1,
    //       title: 'Application Number'
    //     },
    //     {
    //       id:2,
    //       title: 'Applicant Name'
    //     },
    //     {
    //       id:3,
    //       title: 'Program'
    //     },
    //     {
    //       id:4,
    //       title: 'Vertical'
    //     },
    //     {
    //       id:5,
    //       title: 'Existing Limit'
    //     },
    //     {
    //         id:5,
    //         title: 'New Limit'
    //     },
    //     {
    //       id:6,
    //       title: 'Aging'
    //     },
    //     {
    //       id:7,
    //       title: 'Status'
    //     },
    //     {
    //       id:8,
    //       title: 'Progress'
    //     }
    //   ]

    //   applicationDraftNewTableHeaderArr = [
    //     {
    //       id:1,
    //       title: 'Application Number'
    //     },
    //     {
    //       id:2,
    //       title: 'Applicant Name'
    //     },
    //     {
    //       id:3,
    //       title: 'Program'
    //     },
    //     {
    //       id:4,
    //       title: 'Vertical'
    //     },
    //     {
    //       id:5,
    //       title: 'Overall Limit'
    //     },
    //     {
    //       id:6,
    //       title: 'Aging'
    //     },
    //     {
    //       id:7,
    //       title: 'Status'
    //     },
    //     {
    //       id:8,
    //       title: 'Progress'
    //     }
    //   ]

    //   applicationDraftTableData = [
    //     {
    //       id :1,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Former Producer Organisation',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :2,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'Renewal'
    //     },
    //     {
    //       id :3,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'Enhancement'
    //     },
    //     {
    //       id :4,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :5,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :6,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :7,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :8,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :9,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :10,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :11,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :12,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :13,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :14,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :15,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :16,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :17,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :18,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :19,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     },
    //     {
    //       id :20,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       program: 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       overallLimit : '₹ 1,05,00,000',          
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       vertical : 'Agri Enterprises',
    //       applicantType : 'Individual',
    //       createdOn : '26-Mar-2022',
    //       aging : '2 Days',
    //       progress : '70',
    //       status : 'Submitted to Approval',
    //       categoryButton: 'New'
    //     }
    //   ]






    //   applicationOnHoldNewTableHeader = [
    //     {
    //         id:1,
    //         title: 'Application Number'
    //     },
    //     {
    //       id:2,
    //       title: 'Applicant Name'
    //     },
    //     {
    //       id:3,
    //       title: 'Program'
    //     },
    //     {
    //       id:4,
    //       title: 'Vertical'
    //     },
    //     {
    //       id:5,
    //       title: 'Overall Limit'
    //     },
    //     {
    //       id:6,
    //       title: 'Hold Date'
    //     },
    //     {
    //       id:7,
    //       title: 'Status'
    //     },
    //   ]
    //   applicationOnHoldRenewalEnhancementTableHeader = [
    //     {
    //         id:1,
    //         title: 'Application Number'
    //     },
    //     {
    //       id:2,
    //       title: 'Applicant Name'
    //     },
    //     {
    //       id:3,
    //       title: 'Program'
    //     },
    //     {
    //       id:4,
    //       title: 'Vertical'
    //     },
    //     {
    //       id:5,
    //       title: 'Existing Limit'
    //     },
    //     {
    //       id:6,
    //       title: 'New Limit'
    //     },
    //     {
    //       id:7,
    //       title: 'Hold Date'
    //     },
    //     {
    //       id:8,
    //       title: 'Status'
    //     }
    //   ]

    //   applicationOnHoldTableData = [
    //     {
    //       id :1,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :2,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :3,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :4,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :5,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :6,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :7,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :8,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :9,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :10,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :11,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :12,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :13,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :14,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :15,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :16,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :17,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :18,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :19,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :20,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Hold-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',         
    //       holdOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     }
    //   ];
    








    //   applicationRejectedNewTableHeader = [
    //     {
    //         id:1,
    //         title: 'Application Number'
    //     },
    //     {
    //       id:2,
    //       title: 'Applicant Name'
    //     },
    //     {
    //       id:3,
    //       title: 'Program'
    //     },
    //     {
    //       id:4,
    //       title: 'Vertical'
    //     },
    //     {
    //       id:5,
    //       title: 'Overall Limit'
    //     },
    //     {
    //       id:6,
    //       title: 'Rejected Date'
    //     },
    //     {
    //       id:7,
    //       title: 'Status'
    //     },
    //   ]
    //   applicationRejectedRenewalEnhancementTableHeader = [
    //     {
    //         id:1,
    //         title: 'Application Number'
    //     },
    //     {
    //       id:2,
    //       title: 'Applicant Name'
    //     },
    //     {
    //       id:3,
    //       title: 'Program'
    //     },
    //     {
    //       id:4,
    //       title: 'Vertical'
    //     },
    //     {
    //       id:5,
    //       title: 'Existing Limit'
    //     },
    //     {
    //       id:6,
    //       title: 'New Limit'
    //     },
    //     {
    //       id:7,
    //       title: 'Rejected Date'
    //     },
    //     {
    //       id:8,
    //       title: 'Status'
    //     }
    //   ]

    //   applicationRejectedTableData = [
    //     {
    //       id :1,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :2,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :3,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :4,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :5,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :6,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :7,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :8,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :9,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :10,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :11,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :12,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :13,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :14,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :15,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :16,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :17,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :18,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :19,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     },
    //     {
    //       id :20,
    //       applicationNumber : 'ARNSF06080320220296IN01',
    //       applicantName : 'Samunnati Financial Intermediation & Services Private Limited',
    //       vertical : 'Farmer Producer Organization',
    //       program : 'Samunnati- Swiggy (Capital Assist Program) Restaurant Finance',
    //       label : 'Rejected-DRM',
    //       new : '₹ 1,05,00,000',
    //       existing : '₹ 1,05,00,000',
    //       overallLimit : '₹ 1,05,00,000',
    //       applicantType : 'Individual',       
    //       rejectedOn : '28-Mar-2022 ',       
    //       status : 'Submitted to Approval'
    //     }
    //   ];
    
    constructor(){

    }
}