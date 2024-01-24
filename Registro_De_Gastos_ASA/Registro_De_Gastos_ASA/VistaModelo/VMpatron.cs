using Registro_De_Gastos_ASA.Datos;
using Registro_De_Gastos_ASA.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Registro_De_Gastos_ASA.VistaModelo
{
    public class VMpatron : BaseViewModel
    {
        private ObservableCollection<IngresosModel> _ingresos;
        private ObservableCollection<GastosModel> _gastos;
        private string _nuevaDescripcion;
        private double _nuevoMonto;
        private string _resultado;
        private string _ultimoGastoAgregado;
        private string _idRetiro;
        private string _montoRetiro;
        private string _ultimoRetiroAgregado;
        private DDatos ddDatos;

        public VMpatron()
        {
            ddDatos = new DDatos();
            _ingresos = new ObservableCollection<IngresosModel>();
            _gastos = new ObservableCollection<GastosModel>();

            AgregarGastosCommand = new Command(AgregarGastos);
            RetirarMontoCommand = new Command(async () => await RetirarMonto());

            // Cargar datos iniciales al inicializar el ViewModel
            CargarDatosIniciales();
        }
        private async void CargarDatosIniciales()
        {
            IngresosV = await ddDatos.MostrarIngresos();
            GastosV = await ddDatos.MostrarGastos();
        }

        public string IdRetiro
        {
            get { return _idRetiro; }
            set { SetValue(ref _idRetiro, value); }
        }

        public string MontoRetiro
        {
            get { return _montoRetiro; }
            set { SetValue(ref _montoRetiro, value); }
        }

        public string UltimoRetiroAgregado
        {
            get { return _ultimoRetiroAgregado; }
            set { SetProperty(ref _ultimoRetiroAgregado, value); }
        }

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
            get { return _nuevaDescripcion; }
            set { SetValue(ref _nuevaDescripcion, value); }
        }

        public double NuevoMonto
        {
            get { return _nuevoMonto; }
            set { SetValue(ref _nuevoMonto, value); }
        }

        public ObservableCollection<IngresosModel> IngresosV
        {
            get { return _ingresos; }
            set { SetValue(ref _ingresos, value); }
        }

        public ObservableCollection<GastosModel> GastosV
        {
            get { return _gastos; }
            set { SetValue(ref _gastos, value); }
        }

        private async Task RetirarMonto()
        {
            if (string.IsNullOrEmpty(IdRetiro) || string.IsNullOrEmpty(MontoRetiro))
            {
                UltimoRetiroAgregado = "Ingrese el Id y el monto";
                return;
            }

            if (Guid.TryParse(IdRetiro, out Guid id) && double.TryParse(MontoRetiro, out double monto))
            {
                try
                {
                    // Llamar al método de retirar monto desde tu DDatos
                    await ddDatos.RetirarMonto(id, monto);

                    // Actualizar el mensaje de éxito
                    UltimoRetiroAgregado = "Monto retirado correctamente";

                    // Limpiar los campos después de realizar la operación
                    IdRetiro = string.Empty;
                    MontoRetiro = string.Empty;
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir durante el retiro
                    UltimoRetiroAgregado = $"Error al retirar monto: {ex.Message}";
                }
            }
            else
            {
                UltimoRetiroAgregado = "Formato de Id o monto incorrecto";
            }
        }

        private void AgregarGastos()
        {
            string descripcionGasto = NuevaDescripcion;
            double montoGasto = NuevoMonto;

            DDatos objeto = new DDatos();

            if (!string.IsNullOrWhiteSpace(descripcionGasto) && montoGasto > 0)
            {
                GastosModel nuevoGasto = new GastosModel
                {
                    DescripcionGastos = descripcionGasto,
                    MontoGastos = montoGasto
                };

                objeto.Gastos(nuevoGasto);

                _gastos.Add(nuevoGasto);

                NuevaDescripcion = string.Empty;
                NuevoMonto = 0.0;

                UltimoGastoAgregado = $"Ingreso agregado: {descripcionGasto}, Monto: {montoGasto}";
            }
            else
            {
                UltimoGastoAgregado = string.Empty;
            }
        }

        private Command _agregarGastosCommand;
        private Command _retirarMontoCommand;

        public Command AgregarGastosCommand
        {
            get { return _agregarGastosCommand; }
            set { SetValue(ref _agregarGastosCommand, value); }
        }

        public ICommand AgregarGastoCommand => new Command(AgregarGastos);

        public Command RetirarMontoCommand
        {
            get { return _retirarMontoCommand ?? (_retirarMontoCommand = new Command(async () => await RetirarMonto())); }
            set { SetValue(ref _retirarMontoCommand, value); }
        }
        /****************************/



    }
}
