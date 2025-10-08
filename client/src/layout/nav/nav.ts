import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  //Creamos un objeto para las credenciales 
  protected creds: any = {};

  //Hacemos un metodo login
  login(): void {
    console.log(this.creds);
  }
}
