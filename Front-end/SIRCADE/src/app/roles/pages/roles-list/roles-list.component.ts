import {
  AfterViewInit,
  Component,
  DestroyRef,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { RolesService } from '../../services/roles-service.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CommonModule } from '@angular/common';
import { MatSortModule } from '@angular/material/sort';
import { RoleResponse } from '../../interfaces/responses/role.response';
import { switchMap } from 'rxjs/operators';
import { RolesQueries } from '../../interfaces/queries/roles.queries';
import { SearchTextComponent } from 'src/app/shared/components/search-text/search-text.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialog } from '@angular/material/dialog';
import { RoleDeletionDialogComponent } from '../../components/role-deletion-dialog/role-deletion-dialog.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RoleUpdateDialogComponent } from '../../components/role-update-dialog/role-update-dialog.component';
import { RoleRegisterDialogComponent } from '../../components/role-register-dialog/role-register-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-roles-list',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    MatPaginatorModule,
    MatProgressBarModule,
    CommonModule,
    MatSortModule,
    MatButtonModule,
    MatTooltipModule,
    MatIconModule,
    MatProgressSpinnerModule,
    SearchTextComponent,
  ],
  templateUrl: './roles-list.component.html',
  styleUrl: './roles-list.component.scss',
})
export class RolesListComponent implements OnInit, AfterViewInit {
  rolesService: RolesService = inject(RolesService);
  dialogService = inject(MatDialog);
  snackBarService = inject(MatSnackBar);

  destroyRef = inject(DestroyRef);

  roles: RoleResponse[] = [];
  roleColumns: string[] = ['name', 'totalPermissions', 'actions'];
  totalRoles: number = 0;
  pageSize: number = 10;
  searchText: string = '';
  loading: boolean = true;

  @ViewChild(MatPaginator)
  paginator: MatPaginator = Object.create(null);

  ngOnInit(): void {
    this.get();
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        switchMap(() => {
          this.loading = true;

          const rolesQueries: RolesQueries = {
            page: this.paginator.pageIndex,
            pageSize: this.paginator.pageSize,
            search: this.searchText,
          };

          return this.rolesService.get(rolesQueries);
        })
      )
      .subscribe((response) => {
        this.roles = response.data;
        this.totalRoles = response.totalElements;
        this.loading = false;
      });
  }

  search(searchText: string): void {
    this.searchText = searchText;
    this.get();
  }

  get(): void {
    this.loading = true;

    this.rolesService
      .get({ page: 0, pageSize: this.pageSize, search: this.searchText })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.roles = response.data;
        this.totalRoles = response.totalElements;
        this.paginator.firstPage();
        this.loading = false;
      });
  }

  showRoleDeletionConfirmation(role: RoleResponse): void {
    this.dialogService
      .open(RoleDeletionDialogComponent, {
        data: role,
      })
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((roleId: number) => {
        if (!roleId) return;

        this.rolesService
          .delete(roleId)
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe(() => {
            this.snackBarService.open('Role eliminado exitosamente', 'Cerrar', {
              panelClass: ['snackbar-error'],
              duration: 3000,
            });
            this.get();
          });
      });
  }

  showRoleUpdateForm(role: RoleResponse): void {
    this.dialogService
      .open(RoleUpdateDialogComponent, {
        data: role.id,
        width: '600px',
      })
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((updated: boolean) => {
        if (!updated) return;

        this.snackBarService.open('Role actualizado exitosamente', 'Cerrar', {
          panelClass: ['snackbar-warning'],
          duration: 3000,
        });
        this.get();
      });
  }

  showRoleRegisterForm(): void {
    this.dialogService
      .open(RoleRegisterDialogComponent, {
        width: '600px',
      })
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((created: boolean) => {
        if (!created) return;

        this.snackBarService.open('Role creado exitosamente', 'Cerrar', {
          panelClass: ['snackbar-success'],
          duration: 3000,
        });
        this.get();
      });
  }
}
