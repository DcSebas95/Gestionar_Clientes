//Aqui realizo las solicitudes http para mi api

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { appsetting } from '../settings/appsettings';
import { Cliente } from '../models/clientes';
import { ResponseApi } from '../models/ReponseApi';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {
  private http = inject(HttpClient);
  private apiURL: string = appsetting.api_URL + 'Cliente';
  constructor() { }

  lista() {
    return this.http.get<Cliente[]>(this.apiURL)
  }

  Obtener(cliente_id: number) {
    return this.http.get<Cliente>(`${this.apiURL}/${cliente_id}`);
  }

  Agregar(Objeto: Cliente) {
    return this.http.post<ResponseApi>(this.apiURL, Objeto)
  }

  Editar(Objeto: Cliente) {
    return this.http.put<ResponseApi>(this.apiURL, Objeto)
  }

  Eliminar(nit: number) {
    return this.http.delete<ResponseApi>(`${this.apiURL}/${nit}`);
  }
}

