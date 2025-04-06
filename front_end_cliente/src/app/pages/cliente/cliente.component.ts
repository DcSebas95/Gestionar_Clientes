import { Component } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { Message } from 'primeng/message';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-cliente',
  imports: [Message, InputTextModule, ButtonModule],
  templateUrl: './cliente.component.html',
  styleUrl: './cliente.component.css'
})
export class ClienteComponent {

}
