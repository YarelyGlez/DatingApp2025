import { Component, input, output } from '@angular/core';
import { RegisterCreds } from '../../../types/registerCreds';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  cancelRegister = output<boolean>(); //Va a emitir un valor
  protected creds = {} as RegisterCreds;

  register(): void{
    console.log(this.creds);
  }

  cancel(): void{
    this.cancelRegister.emit(false);//Lo que queremos que emita
  }
}
