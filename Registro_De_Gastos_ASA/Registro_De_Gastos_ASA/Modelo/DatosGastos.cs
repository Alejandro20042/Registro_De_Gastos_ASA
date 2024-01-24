using System;
using System.Collections.Generic;
using System.Text;

namespace Registro_De_Gastos_ASA.Modelo
{
    public class GastosModel
    {
        public Guid Id { get; set; }
        public string DescripcionGastos { get; set; }
        public double MontoGastos { get; set; }
    }
}
