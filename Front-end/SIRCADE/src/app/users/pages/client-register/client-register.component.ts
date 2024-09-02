import { Component, inject } from '@angular/core';
import { UserRegisterComponent } from '../../components/user-register/user-register.component';
import { UserTypes } from '../../interfaces/enums/user-types.enum';
import { SelectorOptionDto } from 'src/app/shared/interfaces/dtos/selector-option.dto';
import { Router } from '@angular/router';
import { ResultMessageService } from 'src/app/shared/services/result-message.service';
import { ResultActionType } from 'src/app/shared/interfaces/enums/result-action-type';

@Component({
  selector: 'app-client-register',
  standalone: true,
  imports: [UserRegisterComponent],
  templateUrl: './client-register.component.html',
  styleUrl: './client-register.component.scss',
})
export class ClientRegisterComponent {
  router = inject(Router);
  resultMessageService = inject(ResultMessageService);

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

  goToClientsList() {
    this.resultMessageService.showMessage(
      'Socio registrado con éxito',
      ResultActionType.Register
    );
    this.router.navigate(['socios']);
  }
}
