import { CommonModule } from '@angular/common';
import {
  AfterViewInit,
  Component,
  DestroyRef,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SearchTextComponent } from 'src/app/shared/components/search-text/search-text.component';
import { ResultMessageService } from 'src/app/shared/services/result-message.service';
import { UserResponse } from '../../interfaces/responses/user.response';
import { UsersService } from '../../services/users.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';
import { users } from '../../../pages/apps/email/user-data';
import { UsersQueries } from '../../interfaces/queries/users.queries';
import { Router } from '@angular/router';
import { UserStatusUpdateDialogComponent } from '../user-status-update-dialog/user-status-update-dialog.component';
import { ResultActionType } from 'src/app/shared/interfaces/enums/result-action-type';

@Component({
  selector: 'app-users-list',
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
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss',
})
export class UsersListComponent implements OnInit, AfterViewInit {
  dialogService = inject(MatDialog);
  resultMessageService = inject(ResultMessageService);
  usersService = inject(UsersService);

  destroyRef = inject(DestroyRef);

  users: UserResponse[] = [];
  userColumns: string[] = ['nsa', 'grade', 'fullName', 'unity', 'actions'];
  totalUsers: number = 0;
  pageSize: number = 10;
  searchText: string = '';
  loading: boolean = true;
  editionButtonText = 'Editar';

  @Input('roles')
  roles: string[] = [];

  @Input('user-type')
  userType: string = '';

  @Input('activated-button-text')
  activatedButtonText: string = '';

  @Input('deactivated-button-text')
  deactivatedButtonText: string = '';

  @Output('register')
  onRegisterEvent: EventEmitter<void> = new EventEmitter<void>();

  @Output('update')
  onUpdateEvent: EventEmitter<number> = new EventEmitter<number>();

  @ViewChild(MatPaginator)
  paginator: MatPaginator = Object.create(null);

  ngOnInit(): void {
    this.editionButtonText += ` ${this.userType}`;

    this.get();
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        switchMap(() => {
          this.loading = true;

          const usersQueries: UsersQueries = {
            page: this.paginator.pageIndex,
            pageSize: this.paginator.pageSize,
            search: this.searchText,
            roles: this.roles,
          };

          return this.usersService.get(usersQueries);
        })
      )
      .subscribe((response) => {
        this.users = response.data;
        this.totalUsers = response.totalElements;
        this.loading = false;
      });
  }

  search(searchText: string): void {
    this.searchText = searchText;
    this.get();
  }

  get(): void {
    this.loading = true;

    this.usersService
      .get({
        page: 0,
        pageSize: this.pageSize,
        search: this.searchText,
        roles: this.roles,
      })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.users = response.data;
        this.totalUsers = response.totalElements;
        this.paginator.firstPage();
        this.loading = false;
      });
  }

  goToRegister(): void {
    this.onRegisterEvent.emit();
  }

  showUpdateStatusConfirmation(userResponse: UserResponse): void {
    this.dialogService
      .open(UserStatusUpdateDialogComponent, {
        data: userResponse,
      })
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(({ id, role }) => {
        if (id) {
          this.resultMessageService.showMessage(
            `El estado del ${role} ha sido actualizado`,
            ResultActionType.Update
          );
          this.get();
        }
      });
  }

  goToEdit(userId: number): void {
    this.onUpdateEvent.emit(userId);
  }
}
