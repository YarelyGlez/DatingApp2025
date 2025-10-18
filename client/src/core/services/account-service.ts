import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { LoginCreds, User } from '../../types/user';
import { Observable, tap } from 'rxjs';
import { RegisterCreds } from '../../types/registerCreds';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);

  baseUrl = "https://localhost:5001/api/"

  register(creds: RegisterCreds): Observable<User> {
    return this.http.post<User>(this.baseUrl + "account/register", creds).pipe(//flujo de ejecucion
      tap(user => {
        if(user){
          this.setCurrentUser(user);
        }
      })
    );
  }

  login(creds: LoginCreds): Observable<User>{//Me subscribo con pipe
    return this.http.post<User>(this.baseUrl + "account/login", creds).pipe(//flujo de ejecucion
      tap(user => {
        if(user){
          this.setCurrentUser(user);
        }
      })
    );
  }
  
  setCurrentUser(user: User){
    localStorage.setItem("user", JSON.stringify(user));
    this.currentUser.set(user); //El post retorna un objeto entonces no puede ser user un objeto
  }

  logout(){
    localStorage.removeItem("user");
    this.currentUser.set(null);
  }
}
