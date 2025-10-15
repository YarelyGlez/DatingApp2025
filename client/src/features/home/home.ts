import { Component, Input, signal } from '@angular/core';
import { Register } from "../account/register/register";
import { User } from '../../types/user';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  @Input({required: true}) membersFromApp: User[] = []; //Le pasamos del hijo home - app
  protected registerMode = signal(false);

  showRegister(value: boolean): void {
    this.registerMode.set(value);
  }
}
