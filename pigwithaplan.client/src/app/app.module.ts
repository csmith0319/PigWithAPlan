import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './_shared/nav/sidebar/sidebar.component';
import { AboutComponent } from './about/about.component';
import { BudgetsComponent } from './budgets/budgets.component';
import { NavLinkComponent } from './_shared/nav/nav-link/nav-link.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { AuthModule } from './auth/auth.module';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    AboutComponent,
    BudgetsComponent,
    NavLinkComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatSidenavModule,
    AuthModule,
  ],
  providers: [provideAnimationsAsync()],
  bootstrap: [AppComponent],
})
export class AppModule {}
