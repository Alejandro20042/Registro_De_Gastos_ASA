using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Registro_De_Gastos_ASA.Conexion
{
     class Cconexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://registrodegastos-47f16-default-rtdb.firebaseio.com/");

    }
}
