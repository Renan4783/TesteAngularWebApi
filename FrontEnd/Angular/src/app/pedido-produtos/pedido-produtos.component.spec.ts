import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PedidoProdutosComponent } from './pedido-produtos.component';

describe('PedidoProdutosComponent', () => {
  let component: PedidoProdutosComponent;
  let fixture: ComponentFixture<PedidoProdutosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PedidoProdutosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PedidoProdutosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
