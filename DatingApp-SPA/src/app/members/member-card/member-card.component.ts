import { UserService } from './../../_services/user.service';
import { AlertifyService } from './../../_services/alertify.service';
import { AuthService } from './../../_services/auth.service';
import { User } from './../../_models/user';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() user: User;

  constructor(private authService: AuthService,
    private userService: UserService,
    private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  sendLike(id: number) {
    this.userService.sendLike(this.authService.decodedToken.nameid, id).subscribe(
      response => this.alertifyService.success('Tu as like ' + this.user.knownAs),
      error => this.alertifyService.error(error));
  }
}
