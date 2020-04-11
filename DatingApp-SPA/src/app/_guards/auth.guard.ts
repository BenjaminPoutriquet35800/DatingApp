import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService,
    private router: Router,
    private alertifyService: AlertifyService) { }

  canActivate(next: ActivatedRouteSnapshot): boolean {
    const roles = next.firstChild.data['roles'];

    if (roles) {
      const isMatched = this.authService.roleMatch(roles);

      if (!isMatched) {
        this.router.navigate(['members']);
        this.alertifyService.error('Accès non autorisé :/');
      }

      return isMatched;
    }

    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertifyService.error('Vous ne passerez pas !');
    this.router.navigate(['/home']);
    return false;
  }
}
