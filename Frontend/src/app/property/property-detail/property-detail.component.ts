import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Property } from 'src/app/model/property';
import { HousingService } from 'src/app/services/housing.service';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {

  public propertyId!: number;
  property = new Property();

  constructor(private route: ActivatedRoute,
              private routing: Router,
              private housing: HousingService) { }

  ngOnInit() {
    this.propertyId = Number(this.route.snapshot.params['id']);

    this.route.data.subscribe(
      (data) => {
        this.property = data['prp'];
      }
    )

    this.property.age = this.housing.getPropertyAge(this.property.estPossessionOn);

    // this.route.params.subscribe(
    //   (params) => {
    //     this.propertyId = +params['id']
    //     this.housing.getProperty(this.propertyId).subscribe(
    //       (data: Property) => {
    //         this.property = data
    //       }
    //     )
    //   }
    // )
  }
}
