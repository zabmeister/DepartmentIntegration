using System;
using System.Windows.Threading;

namespace SmartClient
{
    public static class GuiUtils
    {
        public static void Syncronize(Action action)
        {
            Dispatcher.CurrentDispatcher.Invoke(action);
        }
    }
}
