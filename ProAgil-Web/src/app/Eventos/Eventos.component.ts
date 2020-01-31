import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventoService } from '../Services/Evento.service';
import { Evento } from '../models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-Eventos',
  templateUrl: './Eventos.component.html',
  styleUrls: ['./Eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: Evento[];

  imgLargura = 50;
  imgMargem = 2;
  mostrarImagem = false;
  eventosFilrtados: Evento[];
  textoFiltro: string;
  modalRef: BsModalRef;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService
    ) {

  }

  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template);
  }
  private _filtrosLista: string;
  public get filtroLista(): string {
    return this._filtrosLista;
  }
  public set filtroLista(v: string) {
    this._filtrosLista = v;
    console.log('v', v);
    this.eventosFilrtados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  ngOnInit() {
    this.getEventos();
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }
  getEventos(){
    this.eventoService.getAllEventos()
      .subscribe((_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFilrtados = this.eventos;
        console.log('_eventos', _eventos);
      }, error => {
        console.log(error);
      });
  }
  filtrarEventos(filtro: string): Evento[] {
    filtro = filtro.toLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLowerCase().indexOf(filtro) !== -1
    );
  }

}
