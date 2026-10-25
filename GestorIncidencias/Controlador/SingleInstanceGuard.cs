using System;
using System.Threading;

namespace GestorIncidencias.Controlador
{
    public sealed class SingleInstanceGuard : IDisposable
    {
        public const string MutexName = "Global\\GestorIncidencias";
        public const string MensajeInstanciaExistente = "La aplicación GestorIncidencias ya está en ejecución.";

        private readonly Mutex mutex;
        private bool mutexAdquirido;

        public SingleInstanceGuard()
        {
            mutex = new Mutex(false, MutexName);

            try
            {
                mutexAdquirido = mutex.WaitOne(0);
            }
            catch (AbandonedMutexException)
            {
                mutexAdquirido = true;
            }
        }

        public bool EsInstanciaPrincipal
        {
            get { return mutexAdquirido; }
        }

        public void Dispose()
        {
            if (mutexAdquirido)
            {
                mutex.ReleaseMutex();
                mutexAdquirido = false;
            }

            mutex.Dispose();
        }
    }
}
