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
import { SelectorOptionDto } from 'src/app/shared/interfaces/dtos/selector-option.dto';
import { UsersService } from '../../services/users.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UsersValidatorService } from '../../services/users-validator.service';
import { RolesService } from 'src/app/roles/services/roles-service.service';
import { RoleResponse } from 'src/app/roles/interfaces/responses/role.response';
import { forkJoin } from 'rxjs';
import { UserTypes } from '../../interfaces/enums/user-types.enum';
import { UserRegisterRequest } from '../../interfaces/requests/user-register.request';

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.scss',
  providers: [provideNativeDateAdapter(), provideNativeDateTimeAdapter()],
})
export class UserRegisterComponent implements OnInit {
  @Input('user-type-text')
  userTypeText: string;

  @Input('user-type')
  userType: UserTypes;

  @Input('grades')
  grades: SelectorOptionDto[] = [];

  @Output('register')
  onRegisterEvent: EventEmitter<void> = new EventEmitter<void>();

  usersService = inject(UsersService);
  usersValidatorService = inject(UsersValidatorService);
  rolesService = inject(RolesService);

  formBuilder = inject(FormBuilder);
  destroyRef = inject(DestroyRef);

  personalDataFormGroup: FormGroup;
  armyDataFormGroup: FormGroup;
  loading = false;

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
      roles: this.rolesService.getAll(),
      unities: this.usersService.getUnities(),
    })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((responses) => {
        this.roles = responses.roles;
        this.unities = responses.unities;

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
      cellphone: ['', Validators.required],
      address: [''],
      documentNumber: ['', Validators.required],
      maritalStatus: ['', Validators.required],
      observation: [''],
    });

    this.armyDataFormGroup = this.formBuilder.group({
      nsa: ['', Validators.required],
      grade: [null, Validators.required],
      roleId: [null, Validators.required],
      names: ['', Validators.required],
      paternalLastName: ['', Validators.required],
      maternalLastName: ['', Validators.required],
      unityId: [null, Validators.required],
      situation: ['', Validators.required],
    });
  }

  register(): void {
    this.armyDataFormGroup.markAllAsTouched();
    this.personalDataFormGroup.markAllAsTouched();

    if (this.armyDataFormGroup.invalid || this.personalDataFormGroup.invalid) {
      return;
    }

    const userData: UserRegisterRequest = {
      ...this.armyDataFormGroup.value,
      ...this.personalDataFormGroup.value,
      associated: true,
      roleId: this.armyDataFormGroup.get('roleId')?.value,
    };

    this.usersService
      .register(userData)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.onRegisterEvent.emit();
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
}
