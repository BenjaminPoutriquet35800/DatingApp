import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { constructDependencies } from '@angular/core/src/di/reflective_provider';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  registerToggle() {
    this.registerMode = true;
  }

   cancelRegisterMode(registerMode: boolean) {
      this.registerMode = registerMode;
   }
}
