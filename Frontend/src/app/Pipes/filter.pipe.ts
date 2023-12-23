import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any[], filerString: string, propName: string): any[] {
    const resultArray = []
    if (value.length === 0 || filerString === '' || propName === '') {
      return value;
    }
    
    for (const item of value) {
      if (item[propName] === filerString) {
        resultArray.push(item)
      }
    }
    return resultArray;
  }
}
