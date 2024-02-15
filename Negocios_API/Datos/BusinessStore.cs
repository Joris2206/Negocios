using Negocios_API.Models.Dto;

namespace Negocios_API.Datos
{
    public class BusinessStore
    {
        public static List<BusinessDto> businessList = new List<BusinessDto>
        {
            new BusinessDto {Id=1, NombreNegocio="Capri", Direccion="LindaVista", RUC=123456767},
            new BusinessDto {Id=2, NombreNegocio="Serta", Direccion="LindaVista", RUC=122334455}

        };
    }
}
