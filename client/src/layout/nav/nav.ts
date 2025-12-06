import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from "@angular/router";
import { ToastService } from '../../core/services/toast-service';
import { themes } from '../theme';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav implements OnInit{
  private router = inject(Router);
  private toast = inject(ToastService)
  //Para el login 
  protected accountService = inject(AccountService);
  //Creamos un objeto para las credenciales 
  protected creds: any = {};
  //Para indicarle al DOM que el usuario no se ha registrado
  //protected loggedIn = signal(false);

  protected selectedTheme = signal<string>(localStorage.getItem("theme") || "light");
  protected themes = themes;

  ngOnInit(): void {
    document.documentElement.setAttribute("data-theme", this.selectedTheme());
  }

  handleSelectedTheme(theme: string) {
    this.selectedTheme.set(theme);
    localStorage.setItem("theme", theme);
    document.documentElement.setAttribute("data-theme", theme);
    const elem = document.activeElement as HTMLDivElement;
    if (elem) {
      elem.blur();
    }
  }

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

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl("/");
  }
}
