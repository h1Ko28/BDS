import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppComponent } from './app.component';
import { PropertyCardComponent } from './property/property-card/property-card.component';
import { PropertyListComponent } from './property/property-list/property-list.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HousingService } from './services/housing.service';
import { AddPropertyComponent } from './property/add-property/add-property.component';
import { PropertyDetailComponent } from './property/property-detail/property-detail.component';
import { UserLoginComponent } from './user/user-login/user-login.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { AlertifyService } from './services/alertify.service';
import { UserService } from './services/user.service';
import { AuthService } from './services/auth.service';
import { PropertyDetailResolverService } from './property/property-detail/property-detail-resolver.service';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { FilterPipe } from './Pipes/filter.pipe';
import { SortPipe } from './Pipes/sort.pipe';

const appRoute: Routes = [
  {path: 'add-property', component: AddPropertyComponent},
  {path: '', component: PropertyListComponent},
  {path: 'rent-property', component: PropertyListComponent},
  {path: 'property-detail/:id', 
          component: PropertyDetailComponent, 
          resolve: {prp: PropertyDetailResolverService}},
  {path: 'user/login', component: UserLoginComponent},
  {path: 'user/register', component: UserRegisterComponent},
  {path: '**', component: PropertyListComponent}
]

@NgModule({
  declarations: [	
    AppComponent,
    PropertyCardComponent,
    PropertyListComponent,
    NavBarComponent,
    AddPropertyComponent,
    PropertyDetailComponent,
    UserLoginComponent,
    UserRegisterComponent,
    FilterPipe,
    SortPipe
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoute),
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    TabsModule.forRoot(),
    ButtonsModule.forRoot(),
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    CarouselModule.forRoot()
  ],
  providers: [
    HousingService,
    AlertifyService,
    UserService,
    AuthService,
    PropertyDetailResolverService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
