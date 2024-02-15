using Negocios_API.Models.Dto;

namespace Negocios_API.Datos
{
    public class OwnerStore
    {
        public static List<OwnerDto> ownerList = new List<OwnerDto> 
        {
            new OwnerDto {Id=1, NombrePropietario="Joseph Enmanuel Pineda Aguilera", Correo="josephpineda1210@gmail.com", Clave="abcd1234"},
            new OwnerDto {Id=2, NombrePropietario="Joseph Enmanuel Pineda Aguilera", Correo="josephpineda12@gmail.com", Clave="abcd1234"}

        };
    }
}
