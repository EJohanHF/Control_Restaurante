using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{

    public class MEnsaje
    {
        private readonly NotificationManager _notificationManager = new NotificationManager();
        private readonly Random _random = new Random();
        public void Mensaje(string titulo, string mensaje, int tipo)
        {
            //Tipos
            // 1 => Correcto
            // 2 => Advertencia
            // 3 => Error
            // 4 => Neutral
            var content = new NotificationContent
            {
                Title = titulo,
                Message = mensaje,
                Type = (NotificationType)tipo
            };
            _notificationManager.Show(content);
        }
    }
}
