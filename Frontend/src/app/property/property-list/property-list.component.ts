import { Component, OnInit } from '@angular/core';
import { HousingService } from 'src/app/services/housing.service';
import { ActivatedRoute } from '@angular/router';
import { IPropertyBase } from 'src/app/model/ipropertybase';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit{
    properties!: Array<IPropertyBase>;
    sellRent: number = 1;
    City = '';
    SearchCity = '';
    SortbyParam = '';
    SortDirection = 'asc';
    constructor(private housingService: HousingService, private route: ActivatedRoute){ }

    ngOnInit(): void {
      if (this.route.snapshot.url.toString()){
        this.sellRent = 2;
      }
      this.housingService.getAllProperties(this.sellRent).subscribe(
        data => {
         this.properties = data;
        }, error => {
        console.log(error);
      })
    }

    CityFilter() {
      this.SearchCity = this.City;
    }
    
    ClearFilter() {
      this.SearchCity = '';
      this.City = '';
    }

    onSortDirection() {
      if (this.SortDirection === 'asc') {
        this.SortDirection = 'desc';
      } else {
        this.SortDirection = 'asc'
      }
    }
}