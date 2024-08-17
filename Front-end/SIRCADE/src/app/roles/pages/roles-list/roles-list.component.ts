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
    SearchTextComponent,
  ],
  templateUrl: './roles-list.component.html',
  styleUrl: './roles-list.component.scss',
})
export class RolesListComponent implements OnInit, AfterViewInit {
  rolesService: RolesService = inject(RolesService);
  destroyRef = inject(DestroyRef);

  roles: RoleResponse[] = [];
  roleColumns: string[] = ['name', 'totalPermissions', 'actions'];
  totalRoles: number = 0;
  pageSize: number = 1;
  searchText: string = '';

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
      });
  }

  search(searchText: string): void {
    this.searchText = searchText;
    this.get();
  }

  get(): void {
    this.rolesService
      .get({ page: 0, pageSize: this.pageSize, search: this.searchText })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.roles = response.data;
        this.totalRoles = response.totalElements;
        this.paginator.firstPage();
      });
  }
}
