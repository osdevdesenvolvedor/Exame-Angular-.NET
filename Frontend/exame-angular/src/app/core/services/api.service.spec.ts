import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ApiService } from './api.service';
import { environment } from '../../../environments/environment';

describe('ApiService', () => {
  let service: ApiService; let http: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({ imports: [HttpClientTestingModule] });
    service = TestBed.inject(ApiService); http = TestBed.inject(HttpTestingController);
  });
  it('deve montar URL de listar', () => {
    service.listar(9, 2025).subscribe();
    const req = http.expectOne(`${environment.apiBaseUrl}/movimentos?mes=9&ano=2025`);
    expect(req.request.method).toBe('GET');
  });
});
