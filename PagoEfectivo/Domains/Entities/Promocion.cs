using System;
using System.Collections.Generic;
using System.Globalization;

namespace Domains.Entities
{
    public class Promocion
    {
        public int Id { get; set; }
        public string CodigoGenerado { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int PromocionEstadoId { get; set; }
        public virtual PromocionEstado PromocionEstado { get; set; }

        public string GenerarCodigo()
        {
            var longitud = 10;
            var miGuid = Guid.NewGuid();
            var token = Convert.ToBase64String(miGuid.ToByteArray());
            token = token.Replace("=", "").Replace("+", "");
            return token.Substring(0, longitud);
        }
        public  List<string> ValidateInsert()
        {
            var validate = new List<string>();
            if (String.IsNullOrEmpty(Nombre))
            {
                validate.Add("Debe ingresar el nombre.");
            }
            if (String.IsNullOrEmpty(Email))
            {
                validate.Add("Debe ingresar el email.");
            }
            if (String.IsNullOrEmpty(CodigoGenerado))
            {
                validate.Add("El código no se generó correctamente");
            }
            return validate;
        }
        public List<string> ValidateCanjear()
        {
            var validate = new List<string>();
            if (String.IsNullOrEmpty(CodigoGenerado))
            {
                validate.Add("Debe ingresar el CodigoGenerado.");
            }
            return validate;
        }

    }
}
