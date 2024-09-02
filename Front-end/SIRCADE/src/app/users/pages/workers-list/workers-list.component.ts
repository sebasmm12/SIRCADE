import { Component, inject } from '@angular/core';
import { UsersListComponent } from '../../components/users-list/users-list.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-workers-list',
  standalone: true,
  imports: [UsersListComponent],
  templateUrl: './workers-list.component.html',
  styleUrl: './workers-list.component.scss',
})
export class WorkersListComponent {
  router = inject(Router);

  workerRoles = ['Administrador', 'Recepcionista'];

  goToRegister() {
    this.router.navigate(['personal', 'registro']);
  }

  goToUpdate(userId: number) {
    this.router.navigate(['personal', userId, 'edicion']);
  }
}
