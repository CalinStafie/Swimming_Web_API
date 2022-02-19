import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(public authService: AuthService) { }

  @Output() onMenuClick: EventEmitter<any> = new EventEmitter();

  emitOnMenuClick() {
    this.onMenuClick.emit("Clicked!");
  }

  ngOnInit(): void {
  }

}
