import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  //Para el login 
  private accountService = inject(AccountService);
  //Creamos un objeto para las credenciales 
  protected creds: any = {};
  
  //Hacemos un metodo login
  login(): void {
    this.accountService.login(this.creds).subscribe({
      next: response => console.log(response),
      error: error => alert(error.message)
    });
  }
}
