import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  photoUrl: string;

  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(p => this.photoUrl = p);
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertifyService.success('Bienvenue ' + this.model.username);
    }, error => {
      if (error === 'Unauthorized') {
        this.alertifyService.error(`Compte introuvable.`);
        return;
      }
      this.alertifyService.error(`Impossible de se connecter pour le moment.`);
    }, () => {
      this.router.navigate(['/members']);
    }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertifyService.message('Déconnexion');
    this.router.navigate(['/home']);
  }
}
