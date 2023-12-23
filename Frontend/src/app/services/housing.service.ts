import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IPropertyBase } from '../model/ipropertybase';
import { Property } from '../model/property';


@Injectable({
  providedIn: 'root'
})

export class HousingService {

  constructor(private http: HttpClient) { }

  getProperty(id: Number){
    return this.getAllProperties().pipe(
      map(properiesArray => {
        return properiesArray.find(p => p.Id === id) as Property
      })
    )
  }

  getAllProperties(sellRent?: number): Observable<Property[]>{
    return this.http.get('data/properties.json').pipe(
      map(data => {
        const propertiesArray: Array<Property> = [];
        const localProperties = JSON.parse(localStorage.getItem("newProp") as string)

        if(localProperties){
          for (const id in localProperties) {
            if (sellRent){
              if (localProperties.hasOwnProperty(id) && localProperties[id as keyof object]['SellRent'] === sellRent){
                propertiesArray.push(localProperties[id as keyof object])
              }
            } else {
              propertiesArray.push(localProperties[id as keyof object])
            }
          }
        }

        for (const id in data) {
          if (sellRent) {
            if (data.hasOwnProperty(id) && data[id as keyof object]['SellRent'] === sellRent){
              propertiesArray.push(data[id as keyof object])
            }
          } else {
            propertiesArray.push(data[id as keyof object])
          }
        }
        return propertiesArray;
      })
    );
    return this.http.get<Property[]>('data/properties.json');
  }

  addProperty(property: Property){
    let newProp = [property];

    if(localStorage.getItem('newProp')){
      newProp = [property,
        ...JSON.parse(localStorage.getItem('newProp') as string)];
    }
    localStorage.setItem('newProp', JSON.stringify(newProp));
  }

  newPropID() {
    const local = localStorage.getItem('PID')
    if (local) {
      localStorage.setItem('PID', String(+local + 1));
      return +local + 1;
    } else {
      localStorage.setItem('PID', '101');
      return 101;
    }
  }
  
}