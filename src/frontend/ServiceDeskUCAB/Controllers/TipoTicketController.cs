﻿using Microsoft.AspNetCore.Mvc;
using ServiceDeskUCAB.Models;
using ServiceDeskUCAB.Models.Response;
using ServiceDeskUCAB.Models.TipoTicketsModels;
using ServiceDeskUCAB.Models.ViewModel;
using ServiceDeskUCAB.Servicios;

namespace ServiceDeskUCAB.Controllers
{
    public class TipoTicketController : Controller
    {
       
       

        private readonly IServicio_API _servicioApi;



        public TipoTicketController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }


        public async Task<IActionResult> VistaTipo()
        {

            TipoNuevoViewModel tipoNuevoViewModel = new TipoNuevoViewModel();

            tipoNuevoViewModel.ListaTipo = await _servicioApi.Lista();
            tipoNuevoViewModel.tipo = new Tipo();
            tipoNuevoViewModel.tipoCargoNuevo = new();
            tipoNuevoViewModel.ListaDepartamento = await _servicioApi.ListaDepa();
            tipoNuevoViewModel.ListaCargos = await _servicioApi.ListaCargos();

            int i = 0;
            foreach (var r in tipoNuevoViewModel.ListaCargos)
            {
                
                r.posicion = i;
                i++;
            }

            ViewBag.Error = "Mensaje de Error no personalizado";
            
            return View(tipoNuevoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCambios (Tipo tipoTicket, List<string> departamentos, List<string> cargos2, List<int> ordenaprobacion, List<int> minimo_aprobado_nivel, List<int> maximo_rechazado_nivel)
        {
            ApplicationResponse<Tipo_TicketDTOUpdate> respuesta;

            var TipoTicketDTO = new Tipo_TicketDTOUpdate();
            TipoTicketDTO.Id = tipoTicket.Id;
            TipoTicketDTO.Departamento = departamentos;
            TipoTicketDTO.nombre = tipoTicket.nombre;
            TipoTicketDTO.Minimo_Aprobado = tipoTicket.Minimo_Aprobado;
            TipoTicketDTO.Maximo_Rechazado = tipoTicket.Maximo_Rechazado;
            TipoTicketDTO.descripcion = tipoTicket.descripcion;
            TipoTicketDTO.tipo = tipoTicket.tipo;
            TipoTicketDTO.Flujo_Aprobacion = new List<FlujoAprobacionDTOCreate>();
            var ListaCargos = await _servicioApi.ListaCargos();

            var ListanuevoCargo = ListaCargos.Where(x => cargos2.Contains(x.Id.ToString())).ToList();

            int i = 0;
            if (TipoTicketDTO.tipo == "Modelo_Jerarquico")
            {
                foreach (var cargo in ListanuevoCargo)
                {
                    TipoTicketDTO.Flujo_Aprobacion.Add(new FlujoAprobacionDTOCreate()
                    {
                        IdTipoCargo = cargo.Id.ToString(),
                        OrdenAprobacion = ordenaprobacion[i],
                        Minimo_aprobado_nivel = minimo_aprobado_nivel[i],
                        Maximo_Rechazado_nivel = maximo_rechazado_nivel[i]
                    });
                    i++;
                }
            }
            if (TipoTicketDTO.tipo == "Modelo_Paralelo")
            {
                foreach (var cargo in ListanuevoCargo)
                {
                    TipoTicketDTO.Flujo_Aprobacion.Add(new FlujoAprobacionDTOCreate()
                    {
                        IdTipoCargo = cargo.Id.ToString()
                    });
                    i++;
                }
            }

            if (TipoTicketDTO.tipo == "Modelo_No_Aprobacion")
            {
                TipoTicketDTO.Flujo_Aprobacion = null;
            }
            respuesta = await _servicioApi.Actualizar(TipoTicketDTO);

            if (respuesta != null)
            {
                if (respuesta.Success)
                {
                    return RedirectToAction("VistaTipo");
                }
                else
                {
                    ViewBag.Error = respuesta.Message;
                    return NoContent();
                }
            }

            else
                return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Tipo tipoTicket, List<string> departamentos,List<string> cargos2, List<int> ordenaprobacion, List<int> minimo_aprobado_nivel, List<int> maximo_rechazado_nivel)
        {
            ApplicationResponse<Tipo_TicketDTOCreate> respuesta;

            var TipoTicketDTO = new Tipo_TicketDTOCreate();
            TipoTicketDTO.Departamento = departamentos;
            TipoTicketDTO.nombre = tipoTicket.nombre;
            TipoTicketDTO.Minimo_Aprobado = tipoTicket.Minimo_Aprobado;
            TipoTicketDTO.Maximo_Rechazado = tipoTicket.Maximo_Rechazado;
            TipoTicketDTO.descripcion = tipoTicket.descripcion;
            TipoTicketDTO.tipo = tipoTicket.tipo;
            TipoTicketDTO.Flujo_Aprobacion = new List<FlujoAprobacionDTOCreate>();
            var ListaCargos=await _servicioApi.ListaCargos();

            var ListanuevoCargo = ListaCargos.Where(x=>cargos2.Contains(x.Id.ToString())).ToList();
            
            int i = 0;
            if (TipoTicketDTO.tipo == "Modelo_Jerarquico")
            {
                foreach (var cargo in ListanuevoCargo)
                {
                    TipoTicketDTO.Flujo_Aprobacion.Add(new FlujoAprobacionDTOCreate()
                    {
                        IdTipoCargo = cargo.Id.ToString(),
                        OrdenAprobacion = ordenaprobacion[i],
                        Minimo_aprobado_nivel = minimo_aprobado_nivel[i],
                        Maximo_Rechazado_nivel = maximo_rechazado_nivel[i]
                    });
                    i++;
                }
            }
            if (TipoTicketDTO.tipo == "Modelo_Paralelo")
            {
                foreach (var cargo in ListanuevoCargo)
                {
                    TipoTicketDTO.Flujo_Aprobacion.Add(new FlujoAprobacionDTOCreate()
                    {
                        IdTipoCargo = cargo.Id.ToString()
                    });
                    i++;
                }
            }

            if (TipoTicketDTO.tipo == "Modelo_No_Aprobacion")
            {
                TipoTicketDTO.Flujo_Aprobacion = null;
            }
            respuesta = await _servicioApi.Guardar(TipoTicketDTO);

            if (respuesta != null)
            {
                if (respuesta.Success)
                {
                    return RedirectToAction("VistaTipo");
                } else
                {
                    return NoContent();
                    //return RedirectToAction("agregarModal", new { message = (string)respuesta.Message});
                }
            }
                
            else
                return NoContent();

        }
        public async Task<IActionResult> Eliminar(Guid id)
        {
            Console.WriteLine(id);

            var respuesta = await _servicioApi.Eliminar(id);

            if (respuesta)
                return RedirectToAction("VistaTipo");
            else
                return NoContent();
        }



    }
}
