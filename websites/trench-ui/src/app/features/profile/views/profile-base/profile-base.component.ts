import {Component, inject, OnInit} from '@angular/core';
import Keycloak from 'keycloak-js';
import {KeycloakService} from 'keycloak-angular';

@Component({
  selector: 'app-profile-base',
  imports: [],
  templateUrl: './profile-base.component.html',
  styleUrl: './profile-base.component.sass'
})
export class ProfileBaseComponent implements OnInit {
  profile: any = null;

  constructor(private readonly keycloak: Keycloak) {}

  async ngOnInit(): Promise<void> {
    // console.log(this.#keycloak.idToken)
    console.log(await this.keycloak.loadUserProfile())
    // console.log(await this.keycloak.getToken())
    // if (this.#keycloak.authenticated) {
    //   const profile = await this.#keycloak.loadUserProfile();
    //   this.profile = profile;
    // }
  }
}
