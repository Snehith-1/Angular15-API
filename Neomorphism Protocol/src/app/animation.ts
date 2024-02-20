import {
    animate,
    query,
    sequence,
    stagger,
    style,
    transition,
    trigger
  } from "@angular/animations";
  
  /** Dropdown Animation - Sliding the in the dropdowns - In hamburger Mennus */
  export const DropDownAnimation = trigger("dropDownMenu", [
    transition(":enter", [
      style({ height: 0, overflow: "hidden" }),
      query(".menu-item", [
        style({ opacity: 0, transform: "translateY(-50px)" })
      ]),
      sequence([
        animate("200ms", style({ height: "*" })),
        query(".menu-item", [
          stagger(-50, [
            animate("400ms ease", style({ opacity: 1, transform: "none" }))
          ])
        ])
      ])
    ]),
  
    transition(":leave", [
      style({ height: "*", overflow: "hidden" }),
      query(".menu-item", [style({ opacity: 1, transform: "none" })]),
      sequence([
        query(".menu-item", [
          stagger(50, [
            animate(
              "400ms ease",
              style({ opacity: 0, transform: "translateY(-50px)" })
            )
          ])
        ]),
        animate("200ms", style({ height: 0 }))
      ])
    ])
  ]);
  
  export const CardAnimation = trigger('CardFadeInFadeOut', [
    transition(':enter', [
      style({
        opacity: 0,
        margin: '-4% 2% 2% 2%'
      }),
      animate(
        '0.2s ease-in',
        style({
          opacity: 1, 
          margin: '0%'
        })
      ),
    ]),
  ]);