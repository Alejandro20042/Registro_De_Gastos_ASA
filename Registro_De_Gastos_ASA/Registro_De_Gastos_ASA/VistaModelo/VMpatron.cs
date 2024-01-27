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
        #region VARIABLES
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
        #endregion

        #region CONSTRUCTOR
        public VMpatron()
        {
            ddDatos = new DDatos();
            _ingresos = new ObservableCollection<IngresosModel>();
            _gastos = new ObservableCollection<GastosModel>();

            AgregarGastosCommand = new Command(async () => await AgregarGastos());
           // RetirarMontoCommand = new Command(async () => await RetirarMonto());
            CargarDatosIniciales();
        }
        #endregion

        #region OBJETOS
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
        #endregion

        #region PROCESOS
        private async Task RetirarMonto()
        {

            if (Guid.TryParse(IdRetiro, out Guid id) && double.TryParse(MontoRetiro, out double monto))
            {
                string resultadoMensaje = await ddDatos.RetirarMonto(id, monto);

                // Mostrar cuadro de diálogo con una marca de verificación
                await MostrarCuadroDialogoConPalomita("Retiro exitoso", $"Monto retirado: {monto}", true);

                IdRetiro = string.Empty;
                MontoRetiro = string.Empty;
            }
            else
            {

                // Mostrar cuadro de diálogo con una "X"
                await MostrarCuadroDialogoConPalomita("Error", "ID o monto incorrecto", false);
            }
        }

        private async Task AgregarGastos()
        {
            string descripcionGasto = NuevaDescripcion;
            double montoGasto = NuevoMonto;

            DDatos objeto = new DDatos();

            if (!string.IsNullOrWhiteSpace(descripcionGasto) && montoGasto > 0)
            {
                // Se hace un nuevo objeto con los valores obtenidos.
                GastosModel nuevoGasto = new GastosModel
                {
                    DescripcionGastos = descripcionGasto,
                    MontoGastos = montoGasto
                };

                objeto.Gastos(nuevoGasto);

                _gastos.Add(nuevoGasto);

                NuevaDescripcion = string.Empty;
                NuevoMonto = 0.0;

                await MostrarDialogo("Gasto agregado", $"Descripción: {descripcionGasto}, Monto: {montoGasto}");
            }
            else
            {
                UltimoGastoAgregado = string.Empty;
            }
        }
        private async Task MostrarDialogo(string titulo, string mensaje)
        {
            // Símbolo de palomita 
            string palomita = "\u2714";

            string mensajeConPalomita = $"{palomita} {mensaje}";

            await Application.Current.MainPage.DisplayAlert(titulo, mensajeConPalomita, "Aceptar");
        }

        private async Task MostrarCuadroDialogoConPalomita(string titulo, string mensaje, bool exitoso)
        {
            string simbolo = "\u2716";
            string mensajeConSimbolo = $"{simbolo} {mensaje}";

            await Application.Current.MainPage.DisplayAlert(titulo, mensajeConSimbolo, "Aceptar");
        }


        private async void CargarDatosIniciales()
        {
            IngresosV = await ddDatos.MostrarIngresos();
            GastosV = await ddDatos.MostrarGastos();
        }
        #endregion

        #region COMANDOS
        private Command _agregarGastosCommand;
        private Command _retirarMontoCommand;

        public Command AgregarGastosCommand
        {
            get { return _agregarGastosCommand; }
            set { SetValue(ref _agregarGastosCommand, value); }
        }

        public ICommand AgregarGastoCommand => new Command(async () => await AgregarGastos());
        public ICommand RetirarMontoCommand1 => new Command(async () => await RetirarMonto());


        //public Command RetirarMontoCommand
        //{
        //    get { return _retirarMontoCommand ?? (_retirarMontoCommand = new Command(async () => await RetirarMonto())); }
        //    set { SetValue(ref _retirarMontoCommand, value); }
        //}

        #endregion

    }
}
