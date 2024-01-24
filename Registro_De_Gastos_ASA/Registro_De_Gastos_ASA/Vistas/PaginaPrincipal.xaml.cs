using Registro_De_Gastos_ASA.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registro_De_Gastos_ASA.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipal : ContentPage
    {
        public PaginaPrincipal()
        {
            InitializeComponent();
            BindingContext = new VMpatron();
        }
    }
}