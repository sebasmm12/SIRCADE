import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { UserUpdateComponent } from '../../components/user-update/user-update.component';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ResultMessageService } from 'src/app/shared/services/result-message.service';
import { UserTypes } from '../../interfaces/enums/user-types.enum';
import { SelectorOptionDto } from 'src/app/shared/interfaces/dtos/selector-option.dto';
import { ResultActionType } from 'src/app/shared/interfaces/enums/result-action-type';

@Component({
  selector: 'app-client-update',
  standalone: true,
  imports: [UserUpdateComponent],
  templateUrl: './client-update.component.html',
  styleUrl: './client-update.component.scss',
})
export class ClientUpdateComponent implements OnInit {
  router = inject(Router);
  resultMessageService = inject(ResultMessageService);
  activatedRoute = inject(ActivatedRoute);
  destroyRef = inject(DestroyRef);

  userTypes = UserTypes;

  grades: SelectorOptionDto[] = [
    {
      id: 1,
      name: 'Teniente General',
    },
    {
      id: 2,
      name: 'Mayor General',
    },
    {
      id: 3,
      name: 'Coronel',
    },
    {
      id: 4,
      name: 'Comandante',
    },
    {
      id: 5,
      name: 'Mayor',
    },
    {
      id: 6,
      name: 'Capitán',
    },
    {
      id: 7,
      name: 'Teniente',
    },
    {
      id: 8,
      name: 'Alferez',
    },
  ];

  userId: number = 0;

  ngOnInit(): void {
    this.activatedRoute.params
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((params) => {
        console.log('I am here');
        this.userId = params['id'];
      });
  }

  goToClientsList() {
    this.resultMessageService.showMessage(
      'Socio actualizado con éxito',
      ResultActionType.Update
    );
    this.router.navigate(['socios']);
  }
}
