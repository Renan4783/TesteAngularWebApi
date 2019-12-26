import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
  }

  abrirClientes() {
		this.router.navigate(['clientes']);
  }
  
  abrirPedidos() {
		this.router.navigate(['pedidos']);
  }

  abrirProdutos() {
		this.router.navigate(['produtos']);
  }
}
