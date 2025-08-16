import { inject, Injectable, OnInit } from '@angular/core';
import { Environment } from 'app/core/settings';
import Keycloak from 'keycloak-js';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService implements OnInit {
  #environment = inject(Environment);
  private keycloak!: Keycloak.KeycloakInstance;

  constructor() {
    console.log(this.#environment.auth)
    this.keycloak = new Keycloak({
      url: 'http://localhost:8080',
      realm: 'trench',
      clientId: 'trench-ui'
    });
  }

  ngOnInit(): void {
    console.log(this.#environment.apiBff)
  }

  async init(): Promise<boolean> {
    try {
      const authenticated = await this.keycloak.init({
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri: window.location.origin + '/assets/silent-check-sso.html'
      });
      if (authenticated) {
        this.startTokenRefresh();
      }
      return authenticated;
    } catch (error: any) {
      console.error('Erro ao inicializar Keycloak:', {
        message: error?.message,
        stack: error?.stack,
        error
      });
      return false;
    }
  }

  async login(): Promise<void> {
    await this.keycloak.login({
      redirectUri: window.location.origin + '/dashboard'
    });
  }

  async logout(): Promise<void> {
    await this.keycloak.logout({
      redirectUri: window.location.origin
    });
  }

  isLoggedIn(): boolean {
    return !!this.keycloak.authenticated;
  }

  getToken(): string | undefined {
    return this.keycloak.token;
  }

  async getUserProfile(): Promise<any> {
    try {
      return await this.keycloak.loadUserProfile();
    } catch (error) {
      console.error('Erro ao carregar perfil:', error);
      return null;
    }
  }

  getKeycloakInstance(): Keycloak.KeycloakInstance {
    return this.keycloak;
  }

  private startTokenRefresh(): void {
    const REFRESH_INTERVAL = 60 * 60 * 60; // Verifica a cada 10 segundos
    const MIN_VALIDITY = 30; // Renova se o token tiver menos de 30 segundos de validade

    setInterval(async () => {
      try {
        const refreshed = await this.keycloak.updateToken(MIN_VALIDITY);
        if (refreshed) {
          console.log('Token renovado com sucesso. Novo token:', this.keycloak.token);
        } else {
          console.log('Token ainda válido, renovação não necessária.');
        }
      } catch (error) {
        console.error('Erro ao renovar token:', error);
        // Opcional: Redirecionar para login se a renovação falhar
        if (this.isLoggedIn()) {
          this.logout();
        }
      }
    }, REFRESH_INTERVAL);
  }
}
