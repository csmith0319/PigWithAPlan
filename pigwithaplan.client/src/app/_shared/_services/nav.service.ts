import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from '../../auth/_services/auth.service';
import { BehaviorSubject, Subscription } from 'rxjs';
import { SidebarLink } from '../../_models/sidebar-link.model';
import { startWith, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NavService implements OnInit, OnDestroy {
  private lastRoute: string | null = null;
  private subscription: Subscription = new Subscription();
  private links: SidebarLink[] = [
    { label: 'Home', path: '/home', roles: [], hide: false },
    { label: 'About', path: '/about', roles: [], hide: false },
    {
      label: 'Budgets',
      path: '/budgets',
      roles: [],
      hide: false,
      children: [
        {
          label: 'Budget A',
          path: '/budget-a',
          roles: [],
          hide: false,
          children: [
            {
              label: 'Accounts',
              path: '/budget-a/accounts',
              roles: [],
              hide: false,
            },
            {
              label: 'Transactions',
              path: '/budget-a/transactions',
              roles: [],
              hide: false,
            },
            {
              label: 'Settings',
              path: '/budget-a/settings',
              roles: [],
              hide: false,
            },
          ],
        },
      ],
    },
    {
      label: 'Login',
      path: '/login',
      roles: ['noaccess'],
      hide: false,
    },
    {
      label: 'Register',
      path: '/register',
      roles: ['noaccess'],
      hide: false,
    },
    {
      label: 'Logout',
      roles: ['user'],
      hide: false,
      action: () => this.onLogout(),
    },
  ];

  private filteredLinksSubject = new BehaviorSubject<SidebarLink[]>([]);
  filteredLinks$ = this.filteredLinksSubject.asObservable();

  constructor(private router: Router, private authService: AuthService) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.lastRoute = event.urlAfterRedirects;
      }
    });
  }

  ngOnDestroy(): void {
    if (!this.subscription.closed) this.subscription.unsubscribe();
  }

  ngOnInit(): void {}

  onLogout(): void {
    this.subscription.add(
      this.authService.logout().subscribe({
        next: () => {
          const routeToNavigate = this.lastRoute ? this.lastRoute : '/login';
          this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => this.router.navigate([routeToNavigate]));
        },
        error: (err) => {
          console.error('Logout failed', err);
        },
      })
    );
  }

  isVisible(link: SidebarLink, isAuthenticated: boolean): boolean {
    if (link.roles?.length === 0) return true;
    if (link.roles?.includes('noaccess')) return !isAuthenticated;
    if (!isAuthenticated) return false;

    // TODO; role based logic
    return true;
  }

  applyFilter(isAuthenticated: boolean): void {
    const filteredLinks = this.links.map((link) => {
      return {
        ...link,
        hide: !this.isVisible(link, isAuthenticated),
      };
    });

    this.filteredLinksSubject.next(filteredLinks);
  }

  loadLinks(): void {
    this.subscription.add(
      this.authService.AuthSubject_Changed.pipe(
        startWith(false),
        switchMap(() => this.authService.isAuthenticated())
      ).subscribe({
        next: (isAuthenticated) => {
          this.applyFilter(isAuthenticated);
        },
        error: (err) => {},
      })
    );
  }
}
