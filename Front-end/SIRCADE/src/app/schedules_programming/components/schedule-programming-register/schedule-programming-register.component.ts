import { CommonModule } from '@angular/common';
import {
  Component,
  DestroyRef,
  inject,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatNativeDateModule,
  provideNativeDateAdapter,
} from '@angular/material/core';
import {
  MatDatepicker,
  MatDatepickerModule,
} from '@angular/material/datepicker';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import {
  MatTimepickerModule,
  provideNativeDateTimeAdapter,
} from '@dhutaryan/ngx-mat-timepicker';
import { SportFieldInfoResponse } from 'src/app/sport-fields/interfaces/responses/sport-field-info.response';
import { SportFieldsService } from 'src/app/sport-fields/services/sport-fields.service';
import { SchedulesProgrammingValidatorService } from '../../services/schedules-programming-validator.service';
import { ProgrammingTypesService } from '../../services/programming-types.service';
import { forkJoin, map, Observable, startWith } from 'rxjs';
import { ProgrammingTypeInfoResponse } from '../../interfaces/responses/programming-type-info.response';
import { ScheduleProgrammingRequest } from '../../interfaces/requests/schedule-programming.request';
import {
  addDays,
  addHours,
  addMinutes,
  addYears,
  setDate,
  setHours,
} from 'date-fns';
import { SchedulesProgrammingService } from '../../services/schedules-programming.service';
import { AccountsService } from 'src/app/auth/services/accounts.service';
import { HttpErrorResponse } from '@angular/common/http';
import {
  MatAutocompleteModule,
  MatAutocompleteSelectedEvent,
} from '@angular/material/autocomplete';
import { ClientsService } from '../../services/clients.service';
import { ClientInfoResponse } from '../../interfaces/responses/client-info.response';
import { OverlappedSchedulesProgrammingQueries } from '../../interfaces/queries/overlapped-schedules-programming.queries';
import { ScheduleProgrammingConfirmationComponent } from '../schedule-programming-confirmation/schedule-programming-confirmation.component';

@Component({
  selector: 'app-schedule-programming-register',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatInputModule,
    MatSelectModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatTimepickerModule,
    MatNativeDateModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
  ],
  templateUrl: './schedule-programming-register.component.html',
  styleUrl: './schedule-programming-register.component.scss',
  providers: [provideNativeDateAdapter(), provideNativeDateTimeAdapter()],
})
export class ScheduleProgrammingRegisterComponent implements OnInit {
  sportFieldsService = inject(SportFieldsService);
  programmingTypesService = inject(ProgrammingTypesService);
  clientsService = inject(ClientsService);
  schedulesProgrammingService = inject(SchedulesProgrammingService);
  accountsService = inject(AccountsService);
  dialogService = inject(MatDialog);

  scheduleProgrammingValidator = inject(SchedulesProgrammingValidatorService);

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  scheduleProgrammingForm!: FormGroup;
  sportFields: SportFieldInfoResponse[] = [];
  programmingTypes: ProgrammingTypeInfoResponse[] = [];
  clients: ClientInfoResponse[] = [];

  loading: boolean = false;
  test: number = 5;
  generalErrorMessage = '';
  clientCtrl = new FormControl('');
  clientId: number | null = null;
  filteredClients!: Observable<ClientInfoResponse[]>;
  totalOverlapped: number = 0;

  @ViewChild('picker')
  datePicker: MatDatepicker<Date>;

  constructor(
    public dialogRef: MatDialogRef<ScheduleProgrammingRegisterComponent>
  ) {
    this.buildForm();
  }

  ngOnInit(): void {
    this.scheduleProgrammingForm.get('endHour')?.disable();

    this.loading = true;

    forkJoin({
      sportFields: this.sportFieldsService.getAll(),
      programmingTypes: this.programmingTypesService.getAll(),
      clients: this.clientsService.getAll(),
    })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(({ sportFields, programmingTypes, clients }) => {
        this.programmingTypes = programmingTypes;
        this.sportFields = sportFields;
        this.clients = clients;

        this.filteredClients = this.clientCtrl.valueChanges.pipe(
          startWith(null),
          map((value: string | null) =>
            this.clients.filter((client) => client.label.includes(value ?? ''))
          )
        );

        this.disableType();

        this.loading = false;
      });

    this.scheduleProgrammingForm
      .get('startHour')
      ?.valueChanges.pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        if (this.scheduleProgrammingForm.get('startHour')?.invalid) {
          this.scheduleProgrammingForm.get('endHour')?.disable();
          this.scheduleProgrammingForm.get('endHour')?.reset();
          return;
        }

        this.scheduleProgrammingForm.get('endHour')?.enable();
      });
  }

  buildForm(): void {
    this.scheduleProgrammingForm = this.formBuilder.group(
      {
        sportFieldId: [null, Validators.required],
        startDate: [null, Validators.required],
        startHour: [
          null,
          [
            Validators.required,
            this.scheduleProgrammingValidator.validateHour(),
          ],
        ],
        endHour: [
          null,
          [
            Validators.required,
            this.scheduleProgrammingValidator.validateHour(),
          ],
        ],
        clientId: [null],
        type: [null, Validators.required],
        comment: ['', [Validators.required]],
      },
      {
        validators: [this.scheduleProgrammingValidator.validateEndHour()],
      }
    );
  }

  todayDateFilter(date: Date | null): boolean {
    let today = new Date();
    today.setHours(0, 0, 0, 0);

    let dateToCompare = date || new Date();

    return (
      dateToCompare.getTime() >= today.getTime() &&
      dateToCompare.getFullYear() == today.getFullYear()
    );
  }

  confirmRegister(): void {
    this.scheduleProgrammingForm.markAllAsTouched();
    this.scheduleProgrammingForm.updateValueAndValidity();

    if (this.scheduleProgrammingForm.invalid) return;

    let request: ScheduleProgrammingRequest = {
      sportFieldId: this.scheduleProgrammingForm.get('sportFieldId')?.value,
      clientId: this.scheduleProgrammingForm.get('clientId')?.value,
      comment: this.scheduleProgrammingForm.get('comment')?.value,
      type: this.scheduleProgrammingForm.get('type')?.value.id,
      startDate: this.getDate('startDate', 'startHour'),
      endDate: this.getDate('startDate', 'endHour'),
    };

    if (this.scheduleProgrammingForm.get('type')?.value.name == 'Reserva') {
      request.clientId = this.clientId;
    }

    if (this.accountsService.User?.role == 'Socio') {
      request.clientId = this.accountsService.User?.id;
    }

    let overlappedScheduleProgrammingQueries: OverlappedSchedulesProgrammingQueries =
      {
        sportFieldId: request.sportFieldId,
        startDate: request.startDate.toISOString(),
        endDate: request.endDate.toISOString(),
      };

    if (this.scheduleProgrammingForm.get('type')?.value.name == 'Reserva') {
      this.register(request);
      return;
    }

    this.schedulesProgrammingService
      .getTotalOverlapped(overlappedScheduleProgrammingQueries)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((total) => {
        this.totalOverlapped = total;
        if (this.totalOverlapped > 0) {
          this.showConfirmation(request);
          return;
        }

        this.register(request);
      });
  }

  showConfirmation(request: ScheduleProgrammingRequest): void {
    this.dialogService
      .open(ScheduleProgrammingConfirmationComponent, {
        width: '600px',
        data: this.totalOverlapped,
      })
      .afterClosed()
      .subscribe((result) => {
        if (!result) return;
        this.register(request);
      });
  }

  register(request: ScheduleProgrammingRequest): void {
    this.schedulesProgrammingService
      .register(request)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (error: HttpErrorResponse) => {
          this.generalErrorMessage = error.error;
        },
      });
  }

  getDate(startDateKey: string, hourKey: string): Date {
    let date: Date = this.scheduleProgrammingForm.get(startDateKey)?.value;

    date = setHours(date, -1 * date.getUTCHours());

    const hour: Date = this.scheduleProgrammingForm.get(hourKey)?.value;

    date = addHours(date, hour.getHours());
    date = addMinutes(date, hour.getMinutes());

    return date;
  }

  disableType(): void {
    if (this.accountsService.User?.role != 'Socio') return;

    this.scheduleProgrammingForm
      .get('clientId')
      ?.setValue(this.accountsService.User?.id);

    this.scheduleProgrammingForm.get('type')?.disable();

    const type = this.programmingTypes.find(
      (programmingType) => programmingType.name == 'Reserva'
    );

    this.scheduleProgrammingForm.get('type')?.setValue(type);
  }

  get type(): ProgrammingTypeInfoResponse | null {
    return this.scheduleProgrammingForm.get('type')?.value;
  }

  mapClient(client: ClientInfoResponse): string {
    return client.label;
  }

  setClient(event: MatAutocompleteSelectedEvent): void {
    this.clientId = event.option.value.id;
  }

  canShowClients(): boolean {
    return (
      this.type?.name == 'Reserva' && this.accountsService.User?.role != 'Socio'
    );
  }
}
