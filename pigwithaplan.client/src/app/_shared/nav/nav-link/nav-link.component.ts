import { Component, Input } from '@angular/core';
import { SidebarLink } from '../../../_models/sidebar-link.model';

@Component({
  selector: 'app-nav-link',
  templateUrl: './nav-link.component.html',
  styleUrls: ['./nav-link.component.css'],
})
export class NavLinkComponent {
  @Input() link!: SidebarLink;
}
