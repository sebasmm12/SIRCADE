import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { RolesService } from '../../services/roles-service.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-role-update-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatProgressSpinnerModule, ReactiveFormsModule, CommonModule],
  templateUrl: './role-update-dialog.component.html',
  styleUrl: './role-update-dialog.component.scss'
})
export class RoleUpdateDialogComponent implements OnInit {

  roleId: number = inject(MAT_DIALOG_DATA);
  rolesService = inject(RolesService);
  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  roleUpdateForm!: FormGroup;


  loading: boolean = false;

  constructor(public dialogRef: MatDialogRef<RoleUpdateDialogComponent>) {
    this.buildForm();
  }


  ngOnInit(): void {
    this.loading = true;

    this.rolesService.getById(this.roleId).pipe(takeUntilDestroyed(this.destroyRef)).subscribe((role) => {
      this.roleUpdateForm.setValue(role);

      this.loading = false;
    });
  }

  buildForm(): void {
    this.roleUpdateForm = this.formBuilder.group({
      id: [0, [Validators.required]],
      name: ['', [Validators.required]],
      permissions: [[], [Validators.minLength(1)]]
    })
  }


}
