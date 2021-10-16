using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.Dto.Promociones;
using ApplicationServices.Services;
using ApplicationServices.UnitTests.UtilTest;
using Domains.Entities;
using Domains.Repositories;
using Moq;
using Xunit;

namespace ApplicationServices.UnitTests.Promociones
{
    [Collection("TestInit_Collection")]
    public class PromocionServicesUnitTest : BaseTest
    {
        #region GetAll
        [Trait("Category", "GetAll")]
        [Fact]
        public async Task Cuando_se_quiere_obtener_todas_las_promociones_Devuelve_una_lista()
        {
            //Arrange
            var listPromociones = new List<Promocion>
            {
                new Promocion
                {
                    Id = 1,
                    CodigoGenerado = Guid.NewGuid().ToString(),
                    Nombre =  Guid.NewGuid().ToString(),
                    Email = Guid.NewGuid().ToString(),
                },
                new Promocion
                {
                    Id = 2,
                    CodigoGenerado = Guid.NewGuid().ToString(),
                    Nombre =  Guid.NewGuid().ToString(),
                    Email = Guid.NewGuid().ToString(),
                },
                new Promocion
                {
                    Id = 3,
                    CodigoGenerado = Guid.NewGuid().ToString(),
                    Nombre =  Guid.NewGuid().ToString(),
                    Email = Guid.NewGuid().ToString(),
                },
            };
            var mapper = ConfiguraionMapper();
            var mock = new Mock<IPromocionRepository>();
            var mockEstado = new Mock<IPromocionEstadoRepository>();
            mock.Setup(x => x.GetAll()).ReturnsAsync(listPromociones);
            var service = new PromocionServices(mock.Object, mapper, mockEstado.Object);
            //Act
            var data = await service.GetAll();
            //Assert
            Assert.Equal(3, data.DataList.Count);
        }

        #endregion

        #region GetByCodigo
        [Trait("Category", "GetByCodigo")]
        [Fact]
        public async Task Cuando_se_quiere_obtener_una_promocion_por_codigo_devuelve_un_objeto()
        {
            //Arrange
            var codigo = Guid.NewGuid().ToString();
            var promocion = new Promocion
            {
                Id = 1,
                CodigoGenerado = codigo,
                Nombre = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
            };
            var mapper = ConfiguraionMapper();
            var mock = new Mock<IPromocionRepository>();
            var mockEstado = new Mock<IPromocionEstadoRepository>();
            mock.Setup(x => x.GetByCodigo(It.IsAny<string>())).ReturnsAsync(promocion);
            var service = new PromocionServices(mock.Object, mapper, mockEstado.Object);
            //Act
            var data = await service.GetByCodigo(codigo);
            //Assert
            Assert.NotNull(data.Data);
            Assert.Equal(codigo, data.Data.CodigoGenerado);
        }

        [Trait("Category", "GetByCodigo")]
        [Fact]
        public async Task Cuando_se_quiere_obtener_una_promocion_por_codigo_devuelve_nulo()
        {
            //Arrange
            var messageError = "No se encontró la promoción por código";
            var mapper = ConfiguraionMapper();
            var mock = new Mock<IPromocionRepository>();
            var mockEstado = new Mock<IPromocionEstadoRepository>();
            mock.Setup(x => x.GetByCodigo(It.IsAny<string>()));
            var service = new PromocionServices(mock.Object, mapper, mockEstado.Object);
            //Act
            var data = await service.GetByCodigo("123");
            //Assert
            Assert.Null(data.Data);
            Assert.NotNull(data.Messages);
            Assert.Equal(messageError, data.Messages.First());
        }
        #endregion

        #region Insert
        [Trait("Category", "Insert")]
        [Fact]
        public async Task Cuando_se_quiere_insertar_una_nueva_Promocion_devuelve_ok()
        {
            //Arrange
            var promocion = new PromocionDto()
            {
                Nombre = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                PromocionEstadoId = PromocionEstadoDto.GENERADO
            };
            var mapper = ConfiguraionMapper();
            var mock = new Mock<IPromocionRepository>();
            var mockEstado = new Mock<IPromocionEstadoRepository>();
            mockEstado.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new PromocionEstado());
            mock.Setup(x => x.ValidateDuplicate(It.IsAny<string>())).ReturnsAsync(false);
            mock.Setup(x => x.Insert(It.IsAny<Promocion>()));
            var service = new PromocionServices(mock.Object, mapper, mockEstado.Object);
            //Act
            var data = await service.Insert(promocion);
            //Assert
            Assert.NotNull(data);
            Assert.True(data.Success);
        }

        [Trait("Category", "Insert")]
        [Fact]
        public async Task Cuando_se_quiere_insertar_una_nueva_Promocion_y_el_email_ya_estaba_registrado_devuelve_error()
        {
            //Arrange
            var messageError = "Ya existe un código para el usuario ingresado!";
            var promocion = new PromocionDto()
            {
                Nombre = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                PromocionEstadoId = PromocionEstadoDto.GENERADO
            };
            var mapper = ConfiguraionMapper();
            var mock = new Mock<IPromocionRepository>();
            var mockEstado = new Mock<IPromocionEstadoRepository>();
            mockEstado.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new PromocionEstado());
            mock.Setup(x => x.ValidateDuplicate(It.IsAny<string>())).ReturnsAsync(true);
            mock.Setup(x => x.Insert(It.IsAny<Promocion>()));
            var service = new PromocionServices(mock.Object, mapper, mockEstado.Object);
            //Act
            var data = await service.Insert(promocion);
            //Assert
            Assert.NotNull(data);
            Assert.False(data.Success);
            Assert.Equal(messageError, data.Messages.First());
        }

        [Trait("Category", "Insert")]
        [Fact]
        public async Task Cuando_se_quiere_insertar_una_nueva_Promocion_y_el_estado_no_existe_devuelve_error()
        {
            //Arrange
            var messageError = "No se encontró el estado de la promoción!";
            var promocion = new PromocionDto()
            {
                Nombre = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                PromocionEstadoId = PromocionEstadoDto.GENERADO
            };
            var mapper = ConfiguraionMapper();
            var mock = new Mock<IPromocionRepository>();
            var mockEstado = new Mock<IPromocionEstadoRepository>();
            mockEstado.Setup(x => x.Get(It.IsAny<int>()));
            var service = new PromocionServices(mock.Object, mapper, mockEstado.Object);
            //Act
            var data = await service.Insert(promocion);
            //Assert
            Assert.NotNull(data);
            Assert.False(data.Success);
            Assert.Equal(messageError, data.Messages.First());
        }

        [Trait("Category", "Insert")]
        [Fact]
        public async Task Cuando_se_quiere_insertar_una_nueva_Promocion_y_al_validar_la_Promocion_no_esta_el_Email_devuelve_error()
        {
            //Arrange
            var messageError = "Debe ingresar el email.";
            var promocion = new PromocionDto()
            {
                Nombre = Guid.NewGuid().ToString(),
                Email = "",
                PromocionEstadoId = PromocionEstadoDto.GENERADO
            };
            var mapper = ConfiguraionMapper();
            var mock = new Mock<IPromocionRepository>();
            var mockEstado = new Mock<IPromocionEstadoRepository>();
            mockEstado.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new PromocionEstado());
            mock.Setup(x => x.ValidateDuplicate(It.IsAny<string>())).ReturnsAsync(false);
            var service = new PromocionServices(mock.Object, mapper, mockEstado.Object);
            //Act
            var data = await service.Insert(promocion);
            //Assert
            Assert.NotNull(data);
            Assert.False(data.Success);
            Assert.Equal(messageError, data.Messages.First());
        }
        #endregion
    }
}
