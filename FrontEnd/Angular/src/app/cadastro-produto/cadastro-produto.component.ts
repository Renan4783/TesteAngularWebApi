import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';

class NovoProduto {
  public Descricao: string;
  public Valor: number;
}

@Component({
  selector: 'app-cadastro-produto',
  templateUrl: './cadastro-produto.component.html',
  styleUrls: ['./cadastro-produto.component.css']
})
export class CadastroProdutoComponent implements OnInit {

  form: FormGroup;

  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json'
		})
	};

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private router: Router) {
    this.form = this.formBuilder.group({
      descricao: ['', Validators.required],
      valor: ['', Validators.required]
		});
   }

  ngOnInit() {
  }

  onSubmit() {
    if (this.form.invalid) {
			return;
    }

    let novoProduto = this.form.value as NovoProduto;

		this.http.post('http://localhost:49493/api/produtos/', JSON.stringify(novoProduto), this.httpOptions)
			.subscribe(data => {
				this.router.navigate(['produtos']);
			}, error => {
				console.log('Error', error);
			});
  }

  voltar() {
		this.router.navigate(['produtos']);
	}
}
