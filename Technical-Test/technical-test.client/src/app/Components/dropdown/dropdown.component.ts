import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Dropdown } from './dropdown.interface';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.css']
})
export class DropdownComponent {
   @Input() includeAll = true;
   @Output() ddlChange = new EventEmitter();
   ddl: Dropdown[] = [{id:0, categoryName: 'ทั้งหมด'}]; 

   constructor(private http: HttpClient) { }
  
    ngOnInit() {
      this.getProducts();
    }
  
    getProducts() {
        this.http.get<Dropdown[]>('/api/categories').subscribe({
          next: (res:Dropdown[]) => {
           res.forEach(i =>{
            if(this.includeAll){
              this.ddl = [{id:0, categoryName: 'ทั้งหมด'}, ...res];
            }else{
              this.ddl = [{id:0, categoryName: '-- เลือกหมวดหมู่ --'}, ...res];
            }
           })
          },
          error: (err) => {
            console.error(err)
          }
        }
      );
    }

    selected(event: Event) {
      const selectedValue = (event.target as HTMLSelectElement).value;
      this.ddlChange.emit(selectedValue)
    }

}
