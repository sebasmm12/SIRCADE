import {
  AfterViewInit,
  Component,
  DestroyRef,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SportFieldsService } from '../../services/sport-fields.service';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CommonModule } from '@angular/common';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SearchTextComponent } from 'src/app/shared/components/search-text/search-text.component';
import { SportFieldResponse } from '../../interfaces/responses/sport-field.response';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';
import { SportFieldsQueries } from '../../interfaces/queries/sport-fields.queries';
import { SportFieldRegisterDialogComponent } from '../../components/sport-field-register-dialog/sport-field-register-dialog.component';
import { SportFieldUpdateDialogComponent } from '../../components/sport-field-update-dialog/sport-field-update-dialog.component';

@Component({
  selector: 'app-sport-fields-list',
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
  templateUrl: './sport-fields-list.component.html',
  styleUrl: './sport-fields-list.component.scss',
})
export class SportFieldsListComponent implements OnInit, AfterViewInit {
  sportFieldsService = inject(SportFieldsService);
  dialogService = inject(MatDialog);
  snackBarService = inject(MatSnackBar);

  destroyRef = inject(DestroyRef);

  sportFields: SportFieldResponse[] = [];
  sportFieldColumns: string[] = ['name', 'type', 'actions'];
  totalSportFields: number = 0;
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

          const sportFieldsQueries: SportFieldsQueries = {
            page: this.paginator.pageIndex,
            pageSize: this.paginator.pageSize,
            search: this.searchText,
          };

          return this.sportFieldsService.get(sportFieldsQueries);
        })
      )
      .subscribe((response) => {
        this.sportFields = response.data;
        this.totalSportFields = response.totalElements;
        this.loading = false;
      });
  }

  search(searchText: string): void {
    this.searchText = searchText;
    this.get();
  }

  get(): void {
    this.loading = true;

    this.sportFieldsService
      .get({ page: 0, pageSize: this.pageSize, search: this.searchText })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.sportFields = response.data;
        this.totalSportFields = response.totalElements;
        this.paginator.firstPage();
        this.loading = false;
      });
  }

  showSportFieldUpdateForm(sportField: SportFieldResponse): void {
    this.dialogService
      .open(SportFieldUpdateDialogComponent, {
        data: sportField.id,
        width: '600px',
      })
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((updated: boolean) => {
        if (!updated) return;

        this.snackBarService.open(
          'Cancha deportiva actualizada exitosamente',
          'Cerrar',
          {
            panelClass: ['snackbar-warning'],
            duration: 3000,
          }
        );
        this.get();
      });
  }

  showSportFieldRegisterForm(): void {
    this.dialogService
      .open(SportFieldRegisterDialogComponent, {
        width: '600px',
      })
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((created: boolean) => {
        if (!created) return;

        this.snackBarService.open(
          'Cancha deportiva creada exitosamente',
          'Cerrar',
          {
            panelClass: ['snackbar-success'],
            duration: 3000,
          }
        );
        this.get();
      });
  }
}
