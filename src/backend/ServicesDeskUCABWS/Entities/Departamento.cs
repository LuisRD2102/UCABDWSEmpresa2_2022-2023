﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesDeskUCABWS.Entities
{
    public class Departamento
    {
        public Departamento()
        {

        }

        [Key]
        public Guid id { get; set; }
        [Required]
        [StringLength(150)]
        public string nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime fecha_creacion { get; set; }

        [JsonIgnore]
        public DateTime? fecha_ultima_edicion { get; set; }
        public DateTime? fecha_eliminacion { get; set; }

	    public Guid? id_grupo { get; set; }

	    [ForeignKey("id_grupo")]
	    public Grupo grupo { get; set; }

}
        public HashSet<Tipo_Ticket> Tipo_Ticket { get; set; }
        public HashSet<Cargo> Cargo { get; set; }
        public Grupo Grupo { get; set; }
        [Required]
        public HashSet<Estado> ListaEstados { get; set; }
        public HashSet<Ticket> ListaTickets { get; set; }

        public Departamento(string nombre, string descripcion)
        {
            Id = Guid.NewGuid();
            this.nombre = nombre;
            this.descripcion = descripcion;
            fecha_creacion = DateTime.UtcNow;
            fecha_ultima_edicion = DateTime.UtcNow;
        }
    }
}
