﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using ServicesDeskUCABWS.Data;
using ServicesDeskUCABWS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockQueryable.Moq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServicesDeskUCABWS.BussinesLogic.DAO.GrupoDAO;

namespace UnitTestServicesDeskUCABWS.UnitTest_GrupoH.DataSeed
{
    public static class DataSeed
    {
        public static void SetUpContextDataDepartamentoYGrupo(this Mock<IDataContext> _mockContextDG)
        {
            var dept = new List<Departamento>
            {
                new Departamento
                {
                    id = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),

                    nombre = "Grupo Nuevo",

                    descripcion = "Es un grupo",

                    fecha_creacion = DateTime.Now.Date,

                    fecha_ultima_edicion = null,

                    fecha_eliminacion = null,

                    id_grupo = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c87"),

                    grupo = new Grupo
                    {
                       id = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c87"),

                       nombre = "Seguridad Ambiental",

                       descripcion = "Cuida el ambiente",

                       fecha_creacion = DateTime.Now.Date,

                       fecha_ultima_edicion = null,

                       fecha_eliminacion = null
                    }

                }


            };

            var group = dept.Select(q => q.grupo).ToList();

            _mockContextDG.Setup(c => c.Departamentos).Returns(dept.AsQueryable().BuildMockDbSet().Object);
            _mockContextDG.Setup(c => c.Grupos).Returns(group.AsQueryable().BuildMockDbSet().Object);
            _mockContextDG.Setup(set => set.Departamentos.Add(It.IsAny<Departamento>()));
            _mockContextDG.Setup(e => e.Departamentos.Update(It.IsAny<Departamento>()));
            _mockContextDG.Setup(e => e.Departamentos.Remove(It.IsAny<Departamento>()));

            _mockContextDG.Setup(set => set.Grupos.Add(It.IsAny<Grupo>()));
			_mockContextDG.Setup(e => e.Grupos.Update(It.IsAny<Grupo>()));
			_mockContextDG.Setup(e => e.Grupos.Remove(It.IsAny<Grupo>()));
			_mockContextDG.Setup(set => set.DbContext.SaveChanges());

        }



        public static void SetUpContextDataDepartamento(this Mock<IDataContext> _mockContext)
        {
            var request = new List<Departamento>
            {
                new Departamento
                {  
                    id = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),

                    nombre = "Seguridad Ambiental",

                    descripcion = "Cuida el ambiente",

                    fecha_creacion = DateTime.Now.Date,

                    fecha_ultima_edicion = null,

                    fecha_eliminacion = null,

                    id_grupo = null

                }              

            };

            _mockContext.Setup(c => c.Departamentos).Returns(request.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(set => set.Departamentos.Add(It.IsAny<Departamento>()));
            _mockContext.Setup(e => e.Departamentos.Update(It.IsAny<Departamento>()));
            _mockContext.Setup(e => e.Departamentos.Remove(It.IsAny<Departamento>()));
            _mockContext.Setup(set => set.DbContext.SaveChanges());
        }

        public static void SetUpContextDataGrupo(this Mock<IDataContext> _mockContext)
        {
            var request = new List<Grupo>
            {
                new Grupo
                {
                    id = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c87"),

                    nombre = "Grupo Nuevo",

                    descripcion = "Es un grupo",

                    fecha_creacion = DateTime.Now.Date,

                    fecha_ultima_edicion = null,

                    fecha_eliminacion = null
                },
                new Grupo
				{
					id = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c90"),

					nombre = "Segundo Grupo Nuevo",

					descripcion = "Es un grupo",

					fecha_creacion = DateTime.Now.Date,

					fecha_ultima_edicion = null,

					fecha_eliminacion = DateTime.Now.Date
				}

			};

            _mockContext.Setup(c => c.Grupos).Returns(request.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(set => set.Grupos.Add(It.IsAny<Grupo>()));        
            _mockContext.Setup(set => set.DbContext.SaveChanges());
        }



    }
}