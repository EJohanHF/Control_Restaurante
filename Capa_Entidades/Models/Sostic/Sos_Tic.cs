using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Sostic
{
    public class Sos_Tic
    {
            public int ID { get; set; }
            public string RAZON_SOCIAL { get; set; }
            public string RUC { get; set; }
            public string TELEFONO1 { get; set; }
            public string TELEFONO2 { get; set; }
            public string CORREO1 { get; set; }
            public string CORREO2 { get; set; }
            public Byte[] LOGO_SOSTIC { get; set; }
            public Byte[] LOGO_SOSFOOD { get; set; }

            public Sos_Tic()
            {

            }
            public Sos_Tic(int ID, string RAZON_SOCIAL, string RUC, string TELEFONO1, string TELEFONO2, string CORREO1, string CORREO2, Byte[] LOGO_SOSTIC, Byte[] LOGO_SOSFOOD)
            {
                this.ID = ID;
                this.RAZON_SOCIAL = RAZON_SOCIAL;
                this.RUC = RUC;
                this.TELEFONO1 = TELEFONO1;
                this.TELEFONO2 = TELEFONO2;
                this.CORREO1 = CORREO1;
                this.CORREO2 = CORREO2;
                this.LOGO_SOSTIC = LOGO_SOSTIC;
                this.LOGO_SOSFOOD = LOGO_SOSFOOD;
            }
    }
}
