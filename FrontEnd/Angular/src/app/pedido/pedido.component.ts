import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';

class Pedido {
	public Id: number;
  public Numero: number;
  public Cliente: Cliente;
  public Data: Date;
  public ValorTotal: number;
}

class Cliente {
	public Id: number;
  public Nome: string;
  public Email: string;
}

@Component({
  selector: 'app-pedido',
  templateUrl: './pedido.component.html',
  styleUrls: ['./pedido.component.css']
})
export class PedidoComponent implements OnInit {

  pedidos: Pedido[] = [];

  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json'
		})
	};

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.http.get<Pedido[]>('http://localhost:49493/api/pedidos/')
			.subscribe(x => {
				this.pedidos = x;
			});
  }

  voltar() {
		this.router.navigate(['main']);
	}

  verProdutos(pedido: Pedido) {
		this.router.navigate(['pedidos/' + pedido.Id + '/produtos']);
	}

  novo() {
		this.router.navigate(['pedidos/cadastro']);
	}

}
