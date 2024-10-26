import {
  Component,
  DestroyRef,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { provideNativeDateTimeAdapter } from '@dhutaryan/ngx-mat-timepicker';
import { MaterialModule } from 'src/app/material.module';
import { UserTypes } from '../../interfaces/enums/user-types.enum';
import { SelectorOptionDto } from 'src/app/shared/interfaces/dtos/selector-option.dto';
import { UsersService } from '../../services/users.service';
import { UsersValidatorService } from '../../services/users-validator.service';
import { RolesService } from 'src/app/roles/services/roles-service.service';
import { RoleResponse } from 'src/app/roles/interfaces/responses/role.response';
import { forkJoin } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserInfoResponse } from '../../interfaces/responses/user-info.response';

@Component({
  selector: 'app-user-update',
  standalone: true,
  imports: [MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './user-update.component.html',
  styleUrl: './user-update.component.scss',
  providers: [provideNativeDateAdapter(), provideNativeDateTimeAdapter()],
})
export class UserUpdateComponent implements OnInit {
  @Input('user-id')
  userId: number;

  @Input('user-type-text')
  userTypeText: string;

  @Input('user-type')
  userType: UserTypes;

  @Input('grades')
  grades: SelectorOptionDto[] = [];

  @Output('update')
  onUpdateEvent: EventEmitter<void> = new EventEmitter<void>();

  usersService = inject(UsersService);
  usersValidatorService = inject(UsersValidatorService);
  rolesService = inject(RolesService);

  formBuilder = inject(FormBuilder);
  destroyRef = inject(DestroyRef);

  personalDataFormGroup: FormGroup;
  armyDataFormGroup: FormGroup;
  loading = false;
  user!: UserInfoResponse;

  maritalStatuses: SelectorOptionDto[] = [
    {
      id: 0,
      name: 'Casado(a)',
    },
    {
      id: 1,
      name: 'Soltero(a)',
    },
    {
      id: 2,
      name: 'Divorciado(a)',
    },
  ];

  situations: SelectorOptionDto[] = [
    {
      id: 0,
      name: 'Actividad',
    },
    {
      id: 1,
      name: 'Disponibilidad',
    },
    {
      id: 2,
      name: 'Retirado',
    },
  ];

  roles: RoleResponse[] = [];
  unities: SelectorOptionDto[] = [];

  constructor() {
    this.buildForm();
  }
  ngOnInit(): void {
    this.loading = true;

    forkJoin({
      user: this.usersService.getById(this.userId),
      roles: this.rolesService.getAll(),
      unities: this.usersService.getUnities(),
    })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((responses) => {
        this.roles = responses.roles;
        this.unities = responses.unities;
        this.user = responses.user;

        this.setFormValues();

        this.excludeClientRole();
        this.disableRole();

        this.loading = false;
      });
  }

  buildForm() {
    this.personalDataFormGroup = this.formBuilder.group({
      birthDate: [null, Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      cellPhone: ['', Validators.required],
      address: [''],
      documentNumber: ['', Validators.required],
      maritalStatus: ['', Validators.required],
      observation: [''],
    });

    this.armyDataFormGroup = this.formBuilder.group({
      nsa: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      grade: [null, Validators.required],
      roleId: [null, Validators.required],
      names: ['', Validators.required],
      paternalLastName: ['', Validators.required],
      maternalLastName: ['', Validators.required],
      unityId: [null, Validators.required],
      situation: ['', Validators.required],
    });
  }

  update(): void {
    this.armyDataFormGroup.markAllAsTouched();
    this.personalDataFormGroup.markAllAsTouched();

    if (this.armyDataFormGroup.invalid || this.personalDataFormGroup.invalid) {
      return;
    }

    const userData: UserInfoResponse = {
      id: this.userId,
      ...this.armyDataFormGroup.value,
      ...this.personalDataFormGroup.value,
      roleId: this.armyDataFormGroup.get('roleId')?.value,
    };

    this.usersService
      .update(userData)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.onUpdateEvent.emit();
      });
  }

  disableRole(): void {
    if (this.userType != UserTypes.client) return;

    const roleId = this.roles.find((role) => role.name === 'Socio')?.id;

    this.armyDataFormGroup.get('roleId')?.setValue(roleId);
    this.armyDataFormGroup.get('roleId')?.disable();
  }

  excludeClientRole(): void {
    if (this.userType == UserTypes.client) return;

    this.roles = this.roles.filter((role) => role.name !== 'Socio');
  }

  setFormValues(): void {
    this.armyDataFormGroup.patchValue({
      nsa: this.user.nsa,
      grade: this.user.grade,
      roleId: this.user.roleId,
      names: this.user.names,
      paternalLastName: this.user.paternalLastName,
      maternalLastName: this.user.maternalLastName,
      unityId: this.user.unityId,
      situation: this.user.situation,
    });

    this.personalDataFormGroup.patchValue({
      birthDate: this.user.birthDate,
      phone: this.user.phone,
      email: this.user.email,
      cellPhone: this.user.cellPhone,
      address: this.user.address,
      documentNumber: this.user.documentNumber,
      maritalStatus: this.user.maritalStatus,
      observation: this.user.observation,
    });
  }
}
