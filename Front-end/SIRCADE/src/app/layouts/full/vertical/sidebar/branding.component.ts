import { Component } from '@angular/core';
import { CoreService } from 'src/app/services/core.service';

@Component({
  selector: 'app-branding',
  standalone: true,
  template: `
    <div class="branding">
      @if(options.theme === 'light') {
      <a href="/">
        <img
          src="./assets/images/logos/dark-logo.svg"
          class="align-middle m-2"
          alt="logo"
        />
      </a>
      } @if(options.theme === 'dark') {
      <a href="/">
        <img
          src="./assets/images/layout/layout_icon.png"
          class="align-middle layout-logo"
          width="220"
          height="100"
          alt="logo"
        />
      </a>
      }
    </div>
  `,
  styles: [
    `
      .layout-logo {
        margin-left: 8px;
      }
    `,
  ],
})
export class BrandingComponent {
  options = this.settings.getOptions();

  constructor(private settings: CoreService) {}
}
