import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ExportsService {
  constructor() {}

  downloadExcel(
    exportFunction: () => Observable<string>,
    reportTitle: string
  ): void {
    exportFunction().subscribe((data: string) => {
      let binaryExportInfo = atob(data);

      const blob = new Blob(
        [
          new Uint8Array(binaryExportInfo.length).map((_, index) =>
            binaryExportInfo.charCodeAt(index)
          ),
        ],
        {
          type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
        }
      );

      const url = window.URL.createObjectURL(blob);

      const link = document.createElement('a');

      link.href = url;
      link.download = `${reportTitle}.xlsx`;
      link.click();

      window.URL.revokeObjectURL(url);
    });
  }
}
