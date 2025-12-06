import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../core/services/account-service';
import { RegisterCreds } from '../../../types/user';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  private accountService = inject(AccountService)
  cancelRegister = output<boolean>(); //Va a emitir un valor
  protected creds = {} as RegisterCreds;

  register(): void{
    this.accountService.register(this.creds).subscribe({
      next: response => {
        console.log(response);
        this.cancel(); //Cancelara el hilo de la emision
      },
      error: error => console.log(error)
    });
  }

  cancel(): void{
    this.cancelRegister.emit(false);//Lo que queremos que emita
  }
}
