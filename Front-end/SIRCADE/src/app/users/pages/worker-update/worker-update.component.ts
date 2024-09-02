import { Component, DestroyRef, inject } from '@angular/core';
import { UserUpdateComponent } from '../../components/user-update/user-update.component';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ResultMessageService } from 'src/app/shared/services/result-message.service';
import { UserTypes } from '../../interfaces/enums/user-types.enum';
import { SelectorOptionDto } from 'src/app/shared/interfaces/dtos/selector-option.dto';
import { ResultActionType } from 'src/app/shared/interfaces/enums/result-action-type';

@Component({
  selector: 'app-worker-update',
  standalone: true,
  imports: [UserUpdateComponent],
  templateUrl: './worker-update.component.html',
  styleUrl: './worker-update.component.scss',
})
export class WorkerUpdateComponent {
  router = inject(Router);
  resultMessageService = inject(ResultMessageService);
  activatedRoute = inject(ActivatedRoute);
  destroyRef = inject(DestroyRef);

  userTypes = UserTypes;

  grades: SelectorOptionDto[] = [
    {
      id: 1,
      name: 'F-5',
    },
    {
      id: 2,
      name: 'F-4',
    },
    {
      id: 3,
      name: 'F-3',
    },
    {
      id: 4,
      name: 'F-2',
    },
    {
      id: 5,
      name: 'F-1',
    },
    {
      id: 6,
      name: 'DPA',
    },
    {
      id: 7,
      name: 'DPB',
    },
    {
      id: 8,
      name: 'DPC',
    },
    {
      id: 9,
      name: 'DTA',
    },
    {
      id: 10,
      name: 'VIII',
    },
    {
      id: 11,
      name: 'VII',
    },
    {
      id: 12,
      name: 'VI',
    },
    {
      id: 13,
      name: 'V',
    },
    {
      id: 14,
      name: 'IV',
    },
    {
      id: 15,
      name: 'III',
    },
    {
      id: 16,
      name: 'II',
    },
    {
      id: 17,
      name: 'I',
    },
    {
      id: 18,
      name: '14',
    },
    {
      id: 19,
      name: '13',
    },
    {
      id: 20,
      name: '12',
    },
    {
      id: 21,
      name: '11',
    },
    {
      id: 22,
      name: '10',
    },
    {
      id: 23,
      name: '9',
    },
    {
      id: 24,
      name: '8',
    },
    {
      id: 25,
      name: '7',
    },
    {
      id: 26,
      name: '6',
    },
    {
      id: 27,
      name: '5',
    },
    {
      id: 28,
      name: '4',
    },
    {
      id: 29,
      name: '3',
    },
    {
      id: 30,
      name: '2',
    },
    {
      id: 31,
      name: '1',
    },
    {
      id: 32,
      name: 'STA',
    },
    {
      id: 33,
      name: 'STB',
    },
    {
      id: 33,
      name: 'STC',
    },
    {
      id: 33,
      name: 'STD',
    },
    {
      id: 33,
      name: 'STE',
    },
    {
      id: 34,
      name: 'SAA',
    },
    {
      id: 35,
      name: 'SAB',
    },
    {
      id: 36,
      name: 'SPD',
    },
    {
      id: 37,
      name: 'SPE',
    },
    {
      id: 38,
      name: 'SPC',
    },
    {
      id: 39,
      name: 'SPB',
    },
    {
      id: 40,
      name: 'SPA',
    },
    {
      id: 41,
      name: 'SPF',
    },
    {
      id: 42,
      name: 'STF',
    },
    {
      id: 43,
      name: 'SAC',
    },
    {
      id: 44,
      name: 'SAE',
    },
  ];

  userId: number = 0;

  ngOnInit(): void {
    this.activatedRoute.params
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((params) => {
        this.userId = params['id'];
      });
  }

  goToWorkersList() {
    this.resultMessageService.showMessage(
      'Personal actualizado con éxito',
      ResultActionType.Update
    );
    this.router.navigate(['personal']);
  }
}
