using Registro_De_Gastos_ASA.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Registro_De_Gastos_ASA.VistaModelo
{
    public class VMpatron : BaseViewModel
    {
        #region VARIABLES
        private List<IngresosModel> _ingresos;
        private List<GastosModel> _gastos;
        private string _nuevaDescripcion;
        private double _nuevoMonto;
        private string _resultado;
        private string _ultimoGastoAgregado;
        #endregion

        #region OBJETOS

        public string UltimoGastoAgregado
        {
            get { return _ultimoGastoAgregado; }
            set { SetProperty(ref _ultimoGastoAgregado, value); }
        }
        public string Resultado
        {
            get { return _resultado; }
            set { SetProperty(ref _resultado, value); }
        }

        public string NuevaDescripcion
        {
            get {  return _nuevaDescripcion;}
            set { SetValue(ref _nuevaDescripcion, value); }
        }

        public double NuevoMonto
        {
            get { return _nuevoMonto; }
            set { SetValue(ref _nuevoMonto, value); }
        }

        public List<IngresosModel> IngresosV
        {
            get { return _ingresos; }
            set { SetValue(ref _ingresos, value); }
        }

        public List<GastosModel> GastosV
        {
            get { return _gastos; }
            set { SetValue(ref _gastos, value); }
        }
        #endregion

        #region CONSTRUCTOR
        public VMpatron()
        {
            _ingresos = new List<IngresosModel>();
            _gastos = new List<GastosModel>();

            AgregarGastosCommand = new Command(AgregarGastos);
        }
        #endregion

        #region PROCESOS
        private void AgregarGastos()
        {
            // Obtener la descripción y el monto del gasto desde las propiedades
            string descripcionGasto = NuevaDescripcion;
            double montoGasto = NuevoMonto;

            // Saber si la Descripcion no está vacía
            if (!string.IsNullOrWhiteSpace(descripcionGasto) && montoGasto > 0)
            {
                GastosModel nuevoGasto = new GastosModel
                {
                    DescripcionGastos = descripcionGasto,
                    MontoGastos = montoGasto
                };

                _gastos.Add(nuevoGasto);

                NuevaDescripcion = string.Empty;
                NuevoMonto = 0.0;

                // Establecer la propiedad con la información del último gasto agregado
                UltimoGastoAgregado = $"Gasto agregado: {descripcionGasto}, Monto: {montoGasto}";
            }
            else
            {
                UltimoGastoAgregado = string.Empty;
            }
        }

       
        #endregion

        #region COMANDOS
        private Command _agregarGastosCommand;
        public Command AgregarGastosCommand { get { return _agregarGastosCommand; } 
            set { SetValue(ref _agregarGastosCommand, value); }}
        #endregion
    }
}
