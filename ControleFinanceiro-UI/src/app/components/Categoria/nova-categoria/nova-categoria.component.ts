import { CategoriasService } from 'src/app/services/categorias.service';
import { TiposService } from './../../../services/tipos.service';
import { Tipo } from './../../../models/Tipo';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nova-categoria',
  templateUrl: './nova-categoria.component.html',
  styleUrls: ['../listagem-categorias/listagem-categorias.component.css'],
})
export class NovaCategoriaComponent implements OnInit {
  formulario: any;
  tipos: Tipo[];

  constructor(
    private tiposService: TiposService,
    private categoriasServices: CategoriasService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.tiposService.PegarTodos().subscribe((resultado) => {
      this.tipos = resultado;
    });

    this.formulario = new FormGroup({
      nome: new FormControl(null),
      icone: new FormControl(null),
      tipoId: new FormControl(null),
    });
  }

  get propriedade() {
    return this.formulario.controls;
  }

  EnviarFormulario(): void {
    const categoria = this.formulario.value;

    this.categoriasServices.NovaCategoria(categoria).subscribe((resultado) => {
      this.router.navigate(['categorias/listagemcategorias']);
    });
  }
}
