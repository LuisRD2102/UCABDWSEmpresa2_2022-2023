﻿using System;

namespace ServiceDeskUCAB.Models.DTO.Flujo_AprobacionDTO
{
    public class FlujoAprobacionDTOCreate
    {
        // public string IdTipoTicket { get; set; }
        public string IdTipoCargo { get; set; }
        public int? OrdenAprobacion { get; set; }
        public int? Minimo_aprobado_nivel { get; set; }
        public int? Maximo_Rechazado_nivel { get; set; }

    }
}