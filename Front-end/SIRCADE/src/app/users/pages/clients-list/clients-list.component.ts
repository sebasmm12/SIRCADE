import { Component, inject } from '@angular/core';
import { UsersListComponent } from '../../components/users-list/users-list.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clients-list',
  standalone: true,
  imports: [UsersListComponent],
  templateUrl: './clients-list.component.html',
  styleUrl: './clients-list.component.scss',
})
export class ClientsListComponent {
  router = inject(Router);

  clientRoles = ['Socio'];

  goToRegister() {
    this.router.navigate(['socios', 'registro']);
  }

  goToUpdate(userId: number) {
    console.log(userId);
    this.router.navigate(['socios', userId, 'edicion']);
  }
}
