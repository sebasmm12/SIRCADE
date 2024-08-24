import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { RolesService } from '../../services/roles-service.service';
import { PermissionsService } from '../../services/permissions.service';
import { RolesValidatorService } from '../../services/roles-validator.service';
import { RolePermissionDto } from '../../interfaces/dtos/role-permission.dto';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-role-register-dialog',
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
  templateUrl: './role-register-dialog.component.html',
  styleUrl: './role-register-dialog.component.scss',
})
export class RoleRegisterDialogComponent {
  rolesService = inject(RolesService);
  permissionsService = inject(PermissionsService);
  rolesValidatorService = inject(RolesValidatorService);

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  roleRegisterForm!: FormGroup;
  permissions: RolePermissionDto[] = [];

  loading: boolean = false;

  constructor(public dialogRef: MatDialogRef<RoleRegisterDialogComponent>) {
    this.buildForm();
  }

  ngOnInit(): void {
    this.loading = true;

    this.permissionsService
      .getAll()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((permissions) => {
        this.permissions = permissions;
        this.loading = false;
      });
  }

  buildForm(): void {
    this.roleRegisterForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      permissions: [
        [],
        [this.rolesValidatorService.validateMinimumElements(1, 'permission')],
      ],
    });
  }

  register(): void {
    this.roleRegisterForm.markAllAsTouched();
    this.roleRegisterForm.updateValueAndValidity();

    if (this.roleRegisterForm.invalid) {
      return;
    }

    this.rolesService
      .register(this.roleRegisterForm.value)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.dialogRef.close(true);
      });
  }
}
