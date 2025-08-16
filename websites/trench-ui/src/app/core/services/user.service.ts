import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { IApiResponse, IUser, IUsername } from "../models";
import { Environment } from "../settings";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  #http = inject(HttpClient);
  #environment = inject(Environment);

  private userSubject = new BehaviorSubject<IUser | null>(null);
  user$ = this.userSubject.asObservable();

  private uriV1 = `${this.#environment.apiBff}/v1/users`

  me$() {
    return this.#http.get<IApiResponse<IUser>>(`${this.uriV1}/me`)
      .subscribe({
        next: res => this.userSubject.next(res.data)
      })
  }

  username$(username: string) {
    return this.#http.get<IApiResponse<IUsername>>(`${this.uriV1}/${username}`)
  }
}
