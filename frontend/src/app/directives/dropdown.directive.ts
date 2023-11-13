import {
  Directive,
  HostListener,
  HostBinding,
  ElementRef,
  Renderer2,
} from '@angular/core';

@Directive({
  selector: '[appDropdown]',
})
export class DropdownDirective {
  private isOpen = false;

  constructor(private elRef: ElementRef, private renderer: Renderer2) {}

  @HostListener('document:click')
  onClick() {
    const dropdown = this.elRef.nativeElement.querySelector('.dropdown-menu');

    if (this.elRef.nativeElement.contains(event.target)) {
      if (!this.isOpen) {
        this.renderer.addClass(dropdown, 'show');
      }
    } 
    else {
      this.renderer.removeClass(dropdown, 'show');
    }
    
    this.isOpen = !this.isOpen;
  }
}
