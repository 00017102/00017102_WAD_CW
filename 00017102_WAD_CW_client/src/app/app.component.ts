import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = '00017102_WAD_CW_client';
  isSideMenuOpen = false;

  toggleSideMenu(): void {
    this.isSideMenuOpen = !this.isSideMenuOpen;
  }

  onNavigate(): void {
    this.isSideMenuOpen = false; // Close the side menu
  }
}
