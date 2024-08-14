import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';
import { Subscription } from 'rxjs';
import { NavService } from '../../_services/nav.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit, OnDestroy {
  @ViewChild(MatSidenav) sidenav!: MatSidenav;

  mobileQuery: MediaQueryList;
  filteredLinks$ = this.navService.filteredLinks$;
  private _mobileQueryListener: () => void;
  private subscription: Subscription = new Subscription();

  constructor(
    private cdr: ChangeDetectorRef,
    media: MediaMatcher,
    private navService: NavService
  ) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => this.check();
    this.mobileQuery.addEventListener('change', this._mobileQueryListener);
  }

  check(): void {
    this.cdr.detectChanges();
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeEventListener('change', this._mobileQueryListener);
    if (!this.subscription.closed) this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.navService.loadLinks();
  }
}
