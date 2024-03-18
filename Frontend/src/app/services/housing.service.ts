import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IPropertyBase } from '../model/ipropertybase';
import { Property } from '../model/property';
import { IKeyPairValue } from '../model/ikeypairvalue';


@Injectable({
  providedIn: 'root'
})

export class HousingService {

  constructor(private http: HttpClient) { }

  getAllCities(): Observable<string[]> {
    return this.http.get<string[]>('http://localhost:5237/api/city')
  }

  getFurnishingTypes(): Observable<IKeyPairValue[]> {
    return this.http.get<IKeyPairValue[]>('http://localhost:5237/api/furnishingtype/list')
  }

  getPropertyTypes(): Observable<IKeyPairValue[]> {
    return this.http.get<IKeyPairValue[]>('http://localhost:5237/api/propertytype/list')
  }

  getProperty(id: Number){
    return this.http.get<Property>("http://localhost:5237/api/property/detail/" + id.toString())
  }

  getAllProperties(sellRent: number): Observable<Property[]>{
    return this.http.get<Property[]>("http://localhost:5237/api/property/list/" + sellRent.toString());
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

  getPropertyAge(dateofEstablishment: Date): string
  {
      const today = new Date();
      const estDate = new Date(dateofEstablishment);
      let age = today.getFullYear() - estDate.getFullYear();
      const m = today.getMonth() - estDate.getMonth();

      // Current month smaller than establishment month or
      // Same month but current date smaller than establishment date
      if (m < 0 || (m === 0 && today.getDate() < estDate.getDate())) {
          age --;
      }

      // Establshment date is future date
      if(today < estDate) {
          return '0';
      }

      // Age is less than a year
      if(age === 0) {
          return 'Less than a year';
      }

      return age.toString();
  }

}
