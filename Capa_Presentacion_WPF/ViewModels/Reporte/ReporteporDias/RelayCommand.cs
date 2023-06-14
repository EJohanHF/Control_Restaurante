using LiveCharts;
using System;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias
{
    internal class RelayCommand<T> : ICommand
    {
        private object onDrillDownCommand;

        public RelayCommand(object onDrillDownCommand)
        {
            this.onDrillDownCommand = onDrillDownCommand;
        }

        public RelayCommand(Action<ChartPoint> onDrillDownCommand)
        {
            this.onDrillDownCommand = onDrillDownCommand;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}