import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Environment } from "../settings";
import { IApiResponse } from "../models";

@Injectable({
  providedIn: 'root'
})
export class FollowerService {
  #http = inject(HttpClient);
  #environment = inject(Environment);

  private uriV1 = `${this.#environment.apiBff}/v1/followers`

  sendRequest$(followingId: number) {
    const body = {
      followingId
    }

    return this.#http.post<IApiResponse<boolean>>(this.uriV1, body);
  }

}
