import { Component, Input } from '@angular/core';
import { SidebarLink } from '../../../_models/sidebar-link.model';

@Component({
  selector: 'app-nav-link',
  templateUrl: './nav-link.component.html',
  styleUrls: ['./nav-link.component.scss'],
})
export class NavLinkComponent {
  @Input() link!: SidebarLink;

  onClick(): void {
    if (this.link.action) {
      this.link.action();
    }
  }
}
