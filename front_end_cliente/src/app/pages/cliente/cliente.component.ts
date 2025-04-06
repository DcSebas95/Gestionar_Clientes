import { Component, inject, Input, OnInit } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { Message } from 'primeng/message';
import { ButtonModule } from 'primeng/button';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ClientesService } from '../../services/clientes.service';
import { Router } from '@angular/router';
import { Cliente } from '../../models/clientes';

@Component({
  selector: 'app-cliente',
  imports: [Message, InputTextModule, ButtonModule],
  templateUrl: './cliente.component.html',
  styleUrl: './cliente.component.css'
})
//OnInti se usa para que se ejecute cuando la aplicacion inicie
export class ClienteComponent implements OnInit {

  @Input('id') Cliente_id!: number;
  private ClienteServicio = inject(ClientesService);
  public formBuild = inject(FormBuilder)

  public formCliente: FormGroup = this.formBuild.group({
    nit: [''],
    nombre: [''],
    apellido: [''],
    email: [''],
    telefono: [''],
    direccion: ['']
  })
  //ruta del constructor
  constructor(private router: Router) { }

  ngOnInit(): void {
    if (this.Cliente_id != 0) {
      this.ClienteServicio.Obtener(this.Cliente_id).subscribe({
        next: (data) => {
          this.formCliente.patchValue({
            nit: data.nit,
            nombre: data.nombre,
            apellido: data.apellido,
            email: data.email,
            telefono: data.telefono,
            direccion: data.direccion,
          })
        },
        error: (err) => {
          console.log(err.message);
        }
      })
    }
  }

  guardar() {
    const objeto: Cliente = {
      cliente_id: this.Cliente_id,
      nit: this.formCliente.value.nit,
      nombre: this.formCliente.value.nombre,
      apellido: this.formCliente.value.apellido,
      email: this.formCliente.value.email,
      telefono: this.formCliente.value.telefono,
      direccion: this.formCliente.value.direccion
    }

    if (this.Cliente_id == 0) {
      this.ClienteServicio.Agregar(objeto).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.router.navigate(["/"]);
          } else {
            alert("Error al crear")
          }
        },
        error: (err) => {
          console.log(err.message);
        }
      })

    } else {
      this.ClienteServicio.Editar(objeto).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.router.navigate(["/"]);
          } else {
            alert("Error al editar")
          }
        },
        error: (err) => {
          console.log(err.message);
        }
      })
    }
  }
  volver() {
    this.router.navigate(["/"]);
  }

}


