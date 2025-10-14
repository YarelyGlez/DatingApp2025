import { Component, inject, signal } from '@angular/core';
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
  protected accountService = inject(AccountService);
  //Creamos un objeto para las credenciales 
  protected creds: any = {};
  
  //Para indicarle al DOM que el usuario no se ha registrado
  //protected loggedIn = signal(false);

  //Hacemos un metodo login
  login(): void {
    this.accountService.login(this.creds).subscribe({
      next: response => {
        console.log(response);
        this.creds = {};
      },
      error: error => alert(error.message)
    });
  }

  logout(): void{
    this.accountService.logout();
  }
}
