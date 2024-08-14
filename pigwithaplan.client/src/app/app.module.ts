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
import { AuthModule } from './auth/auth.module';
import { SharedModule } from './_shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateBudgetComponent } from './budgets/create-budget/create-budget.component';
import { BudgetComponent } from './budgets/budget/budget.component';
import { DatatableComponent } from './_shared/datatable/datatable.component';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    AboutComponent,
    BudgetsComponent,
    NavLinkComponent,
    CreateBudgetComponent,
    BudgetComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,

    AuthModule,
    SharedModule,
  ],
  providers: [provideAnimationsAsync()],
  bootstrap: [AppComponent],
})
export class AppModule {}
