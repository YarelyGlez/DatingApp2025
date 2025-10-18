import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from "@angular/router";
import { ToastService } from '../../core/services/toast-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  private router = inject(Router);
  private toast = inject(ToastService)
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
        this.router.navigateByUrl("/members");//redirigimos a una url
        this.creds = {};
        this.toast.success("Logged in!");
      },
      error: error => {
        console.log(error);
        this.toast.error(error.error);
      }
    });
  }

  logout(): void{
    this.accountService.logout();
    this.router.navigateByUrl("/");
  }
}
