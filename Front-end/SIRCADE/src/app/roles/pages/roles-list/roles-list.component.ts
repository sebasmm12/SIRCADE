import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { RolesService } from '../../services/roles-service.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-roles-list',
  standalone: true,
  imports: [],
  templateUrl: './roles-list.component.html',
  styleUrl: './roles-list.component.scss',
})
export class RolesListComponent implements OnInit {
  rolesService: RolesService = inject(RolesService);
  destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    this.rolesService
      .get()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((roles) => {
        console.log(roles);
      });
  }
}
