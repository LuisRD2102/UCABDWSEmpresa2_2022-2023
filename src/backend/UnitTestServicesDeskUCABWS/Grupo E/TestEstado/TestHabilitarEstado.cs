﻿using AutoMapper;
using Moq;
using ServicesDeskUCABWS.BussinesLogic.DAO.EstadoDAO;
using ServicesDeskUCABWS.BussinesLogic.DTO.EstadoDTO;
using ServicesDeskUCABWS.BussinesLogic.Exceptions;
using ServicesDeskUCABWS.BussinesLogic.Mapper;
using ServicesDeskUCABWS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestServicesDeskUCABWS.DataSeed;

namespace UnitTestServicesDeskUCABWS.Grupo_E.TestEstado
{

    [TestClass]
    public class TestHabilitarEstado
    {
        Mock<IDataContext> context;
        private readonly EstadoService EstadoDAO;
        private IMapper mapper;


        public TestHabilitarEstado()
        {
            context = new Mock<IDataContext>();

            var myprofile = new List<Profile>
            {
                new Mappers()
            };
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(myprofile));
            mapper = new Mapper(configuration);

            EstadoDAO = new EstadoService(context.Object, mapper);
            context.SetupDbContextData();
        }
        //Test camino feliz para hacer el habilitar estado
        [TestMethod]
        public void CaminoFelizHabilitarEstado()
        {
            //arrange
            var data = new EstadoDTOUpdate
            {
                Id = new Guid("B74DF138-BA05-45A8-B890-E424CA60210C"),

            };
            //Act
            context.Setup(a => a.DbContext.SaveChanges());

            var result = EstadoDAO.HabilitarEstado(data.Id);

            Assert.AreEqual(data.GetType(), result.GetType());
        }

        //Test para la excepcion Exception para habilitar estado  
        [TestMethod]
        public void EntrarEnExceptionControlDesHabilitarEstadoTest()
        {
            //arrage
            var data = new EstadoDTOUpdate
            {
                Id = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),

            };

            //assert
            Assert.ThrowsException<ExceptionsControl>(() => EstadoDAO.HabilitarEstado(data.Id));

        }
    }
}







