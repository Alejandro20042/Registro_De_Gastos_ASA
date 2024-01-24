using System;
using System.Collections.Generic;
using System.Text;

namespace Registro_De_Gastos_ASA.Modelo
{
        public class IngresosModel
        {
            public Guid Id { get; set; }
            public string DescripcionIngresos { get; set; }
            public double MontoIngresos { get; set; }
        }

    
}
