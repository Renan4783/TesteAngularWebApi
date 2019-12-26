import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';

class Pedido {
	public Id: number;
  public Numero: number;
  public Cliente: Cliente;
  public ValorTotal: number;
}

class Produto {
	public Id: number;
  public Descricao: String;
  public Valor: number;
}

class Cliente {
	public Id: number;
  public Nome: string;
  public Email: string;
}

@Component({
  selector: 'app-pedido-produtos',
  templateUrl: './pedido-produtos.component.html',
  styleUrls: ['./pedido-produtos.component.css']
})
export class PedidoProdutosComponent implements OnInit {

  pedido: Pedido = new Pedido();
  produtos: Produto[] = [];
  idPedido: number;

  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json'
		})
	}; 

  constructor(private http: HttpClient, private router: Router, private activatedRoute: ActivatedRoute) {
		this.idPedido = this.activatedRoute.snapshot.params['id'];
	}

  ngOnInit() {

    this.http.get<Pedido>('http://localhost:49493/api/pedidos/' + this.idPedido)
			.subscribe(x => this.pedido = x);

    this.http.get<Produto[]>('http://localhost:49493/api/pedidos/' + this.idPedido + '/produtos')
			.subscribe(x => {
				this.produtos = x;
			});
  }

  voltar() {
		this.router.navigate(['pedidos']);
  }
  
}
