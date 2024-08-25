import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { RolesService } from '../../services/roles-service.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { PermissionsService } from '../../services/permissions.service';
import { forkJoin } from 'rxjs';
import { RolePermissionDto } from '../../interfaces/dtos/role-permission.dto';
import { RolesValidatorService } from '../../services/roles-validator.service';

@Component({
  selector: 'app-role-update-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
  ],
  templateUrl: './role-update-dialog.component.html',
  styleUrl: './role-update-dialog.component.scss',
})
export class RoleUpdateDialogComponent implements OnInit {
  roleId: number = inject(MAT_DIALOG_DATA);

  rolesService = inject(RolesService);
  permissionsService = inject(PermissionsService);
  rolesValidatorService = inject(RolesValidatorService);

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  roleUpdateForm!: FormGroup;
  permissions: RolePermissionDto[] = [];

  loading: boolean = false;

  constructor(public dialogRef: MatDialogRef<RoleUpdateDialogComponent>) {
    this.buildForm();
  }

  ngOnInit(): void {
    this.loading = true;

    forkJoin({
      permissions: this.permissionsService.getAll(),
      role: this.rolesService.getById(this.roleId),
    })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(({ permissions, role }) => {
        this.permissions = permissions;
        this.roleUpdateForm.setValue(role);
        this.loading = false;
      });
  }

  buildForm(): void {
    this.roleUpdateForm = this.formBuilder.group({
      id: [0, [Validators.required]],
      name: ['', [Validators.required]],
      permissions: [
        [],
        [this.rolesValidatorService.validateMinimumElements(1, 'permiso')],
      ],
    });
  }

  update(): void {
    this.roleUpdateForm.markAllAsTouched();
    this.roleUpdateForm.updateValueAndValidity();

    if (this.roleUpdateForm.invalid) {
      return;
    }

    this.rolesService
      .update(this.roleUpdateForm.value)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.dialogRef.close(true);
      });
  }
}
