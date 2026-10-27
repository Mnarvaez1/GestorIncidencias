using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestorIncidencias.Controlador;

namespace GestorIncidencias
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SingleInstanceGuard guard = null;

            try
            {
                guard = new SingleInstanceGuard();

                if (!guard.EsInstanciaPrincipal)
                {
                    MessageBox.Show(
                        SingleInstanceGuard.MensajeInstanciaExistente,
                        "GestorIncidencias",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmIncidencias());
            }
            finally
            {
                if (guard != null)
                {
                    guard.Dispose();
                }
            }
        }
    }
}
