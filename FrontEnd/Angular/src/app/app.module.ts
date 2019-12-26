import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialImports } from './MaterialImports';

import { AppComponent } from './app.component';
import { CadastroContatoComponent } from './cadastro-contato/cadastro-contato.component';
import { ContatosComponent } from './contatos/contatos.component';
import { MensagensComponent } from './mensagens/mensagens.component';
import { CadastroMensagemComponent } from './cadastro-mensagem/cadastro-mensagem.component';
import { ClientesComponent } from './clientes/clientes.component';
import { PedidoComponent } from './pedido/pedido.component';
import { ProdutoComponent } from './produto/produto.component';
import { CadastroClienteComponent } from './cadastro-cliente/cadastro-cliente.component';
import { CadastroProdutoComponent } from './cadastro-produto/cadastro-produto.component';
import { CadastroPedidoComponent } from './cadastro-pedido/cadastro-pedido.component';
import { MainComponent } from './main/main.component';
import { PedidoProdutosComponent } from './pedido-produtos/pedido-produtos.component';



const appRoutes: Routes = [
	{ path: 'contatos', component: ContatosComponent },
	{ path: 'contatos/cadastro', component: CadastroContatoComponent },
	{ path: 'contatos/:id/mensagens', component: MensagensComponent },
	{ path: 'contatos/:id/mensagens/cadastro', component: CadastroMensagemComponent },

	{ path: 'clientes', component: ClientesComponent },
	{ path: 'clientes/cadastro', component: CadastroClienteComponent },

	{ path: 'pedidos', component: PedidoComponent },
	{ path: 'pedidos/cadastro', component: CadastroPedidoComponent },
	{ path: 'pedidos/:id/produtos', component: PedidoProdutosComponent },

	{ path: 'produtos', component: ProdutoComponent },
	{ path: 'produtos/cadastro', component: CadastroProdutoComponent },

	{ path: 'main', component: MainComponent },
	{ path: '', redirectTo: '/main', pathMatch: 'full' }
];

@NgModule({
	declarations: [
		AppComponent,
		CadastroContatoComponent,
		ContatosComponent,
		MensagensComponent,
		CadastroMensagemComponent,
		ClientesComponent,
		PedidoComponent,
		ProdutoComponent,
		CadastroClienteComponent,
		CadastroProdutoComponent,
		CadastroPedidoComponent,
		MainComponent,
		PedidoProdutosComponent
	],
	imports: [
		BrowserModule,
		RouterModule.forRoot(appRoutes),
		HttpClientModule,
		BrowserAnimationsModule,
		MaterialImports,
		FormsModule,
		ReactiveFormsModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
