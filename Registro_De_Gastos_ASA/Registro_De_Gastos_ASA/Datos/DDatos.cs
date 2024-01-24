    using Registro_De_Gastos_ASA.Conexion;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Firebase.Database;
    using Registro_De_Gastos_ASA.Modelo;
    using Firebase.Database.Query;
    using System.Linq;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Xamarin.Essentials;

namespace Registro_De_Gastos_ASA.Datos
    {
        public class DDatos
        {
            public async Task AgregarIngresos(IngresosModel parametros)
            {
                await Cconexion.firebase
                    .Child("Ingresos")
                    .PostAsync(new IngresosModel()
                    {
                        Id = Guid.NewGuid(),
                        DescripcionIngresos = parametros.DescripcionIngresos,
                        MontoIngresos = parametros.MontoIngresos,


                    }
                    ); 
            }

            public async Task Gastos(GastosModel parametros)
            {
                await Cconexion.firebase
                    .Child("Gastos")
                    .PostAsync(new GastosModel()
                    {
                        Id= Guid.NewGuid(),
                        DescripcionGastos = parametros.DescripcionGastos,
                        MontoGastos = parametros.MontoGastos

                    }
                    );
            }
        public async Task RetirarMonto(Guid id, double monto)
        {
            var gasto = (await Cconexion.firebase
                .Child("Gastos")
                .OnceAsync<GastosModel>())
                .FirstOrDefault(x => x.Object.Id == id)?.Object;

            if (gasto != null)
            {
                if (gasto.MontoGastos >= monto)
                {
                    gasto.MontoGastos -= monto;

                    await Cconexion.firebase
                        .Child("Gastos")
                        .Child(gasto.Id.ToString())
                        .PutAsync(gasto);
                }
                else
                {
                    
                }
            }
            
        }

        public async Task<ObservableCollection<GastosModel>> MostrarGastos()
            {
                var data = await Task.Run(() => Cconexion.firebase
                  .Child("Gastos")
                  .AsObservable<GastosModel>()
                  .AsObservableCollection()
                  );
                return data;

            }
            public async Task<ObservableCollection<IngresosModel>> MostrarIngresos()
            {
                var data = await Task.Run(() => Cconexion.firebase
                   .Child("Ingresos")
                   .AsObservable<IngresosModel>()
                   .AsObservableCollection()
                   );
                return data;

            }
        }

    }

