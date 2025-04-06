import { Component, inject } from '@angular/core';


import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ClientesService } from '../../services/clientes.service';
import { Cliente } from '../../models/clientes';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicio',
  imports: [CardModule, TableModule, ButtonModule],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {

  private ClienteServcio = inject(ClientesService)
  public listaClientes: Cliente[] = [];
  public displayedColumns: string[] = ['cliente_id', 'NIT', 'NOMBRE', 'APELLIDO', 'EMAIL', 'TELEFONO', 'DIRECCION', 'ACCION'];

  obtenerCliente() {
    this.ClienteServcio.lista().subscribe({
      next: (data) => {
        console.log(data);
        if (data.length > 0) {
          this.listaClientes = data;
        }
      }, error: (err) => {
        console.log(err.message)
      }
    })
  }
  constructor(private router: Router) {


  }

  ngOnInit(): void {
    this.obtenerCliente();
  }

  nuevo() {
    this.router.navigate(['/cliente', 0]);
  }

  editar(objeto: Cliente) {
    this.router.navigate(['/cliente', objeto.cliente_id]);
  }

  eliminar(objeto: Cliente) {
    if (confirm("Desea eliminar el cliente" + objeto.nombre)) {
      this.ClienteServcio.Eliminar(objeto.cliente_id).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.obtenerCliente();
          } else {
            alert("No se puedo eliminar")
          }
        }, error: (err) => {
          console.log(err.message)
        }
      });
    }
  }
}
