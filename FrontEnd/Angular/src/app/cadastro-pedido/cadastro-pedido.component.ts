import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { NgForOf } from '@angular/common';

class NovoPedido {
  public Numero: number;
  public IdCliente: number;
  public Desconto: number;
  public Valor: number;
  public ValorTotal: number;
  public Produtos: Produto[];
}

class Pedido {
	public Id: number;
  public Numero: number;
}

class Cliente {
	public Id: number;
  public Nome: string;
  public Email: string;
}

class Produto {
  public Id: number;
  public Descricao: string;
  public Valor: number;
}

interface Produto {
  Id: number;
  Descricao: string;
  Valor: number;
}

@Component({
  selector: 'app-cadastro-pedido',
  templateUrl: './cadastro-pedido.component.html',
  styleUrls: ['./cadastro-pedido.component.css']
})
export class CadastroPedidoComponent implements OnInit {

  form: FormGroup;
  produtosForm: FormArray;
  clientes: Cliente[] = [];
  produtos: Produto[] = [];
  pedidos: Pedido[] = [];

  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json'
		})
	};

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private router: Router) {
    this.form = this.formBuilder.group({
      numero: ['', Validators.required],
      idCliente: ['', Validators.required],
      produtos: [[], Validators.required],
      desconto: [''],
      valor: ['', Validators.required],
      valortotal: ['', Validators.required]
		});
   }

  ngOnInit() {
    this.http.get<Cliente[]>('http://localhost:49493/api/clientes/')
			.subscribe(x => {
				this.clientes = x;
      });
      
    this.http.get<Produto[]>('http://localhost:49493/api/produtos/')
			.subscribe(x => {
				this.produtos = x;
      });
      
      this.http.get<Pedido[]>('http://localhost:49493/api/pedidos/')
			.subscribe(x => {
				this.pedidos = x;
      });
  }

  ngAfterViewInit() {
    this.http.get<Pedido[]>('http://localhost:49493/api/pedidos/')
			.subscribe(x => {
        this.pedidos = x;
        this.form.controls['numero'].setValue(this.pedidos.length);
      });

  }

  addValor() {
    //this.form.controls['valor'].setValue('1');
  }

  onSubmit() {

    if (this.form.invalid) {
      return;
    }

    let novoPedido = this.form.value as NovoPedido; 

    this.http.post('http://localhost:49493/api/pedidos/', JSON.stringify(novoPedido), this.httpOptions)
		.subscribe(data => {
      this.router.navigate(['pedidos']);
		}, error => {
			console.log('Error', error);
		});

		
  }

  voltar() {
		this.router.navigate(['pedidos']);
	}
}
