import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';

class Produto {
	public Id: number;
  public Descricao: String;
}

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit {

  produtos: Produto[] = [];

  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json'
		})
	};

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.http.get<Produto[]>('http://localhost:49493/api/produtos/')
			.subscribe(x => {
				this.produtos = x;
			});
  }

  voltar() {
		this.router.navigate(['main']);
	}

  novo() {
		this.router.navigate(['produtos/cadastro']);
	}

}
