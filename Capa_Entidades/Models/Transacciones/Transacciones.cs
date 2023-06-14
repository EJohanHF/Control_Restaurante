using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Transacciones
{
   public class Transacciones
    {
        public int ID { get; set; }
        public string TIPO_OPERACION { get; set; }
        public string NOM_TABLA { get; set; }
        public string ID_CAMPO { get; set; }
        public string NOM_CAMPO { get; set; }
        public string VALOR_ORIGINAL { get; set; }
        public string VALOR_NUEVO { get; set; }
        public DateTime FECH_HORA_TRAN { get; set; }

        public string IDUSU { get; set; }
        public string USU_NOM { get; set; }

        public string IDROL { get; set; }
        public string NOM_ROL { get; set; }

        public string NOM_EQUIPO { get; set; }
        public string ACCION { get; set; }
        public string IP_LOCAL { get; set; }





        //    _tran.ID = Convert.ToInt32(row["ID"]);

        //                _tran.TIPO_OPERACION = row["TIPO_OPERACION"].ToString();
        //    _tran.NOM_TABLA = row["NOM_TABLA"].ToString();

        //    _tran.ID_CAMPO = row["ID_CAMPO"].ToString();

        //    _tran.NOM_CAMPO = row["NOM_CAMPO"].ToString();
        //    _tran.VALOR_ORIGINAL = row["VALOR_ORIGINAL"].ToString();
        //    _tran.VALOR_NUEVO = row["VALOR_NUEVO"].ToString();
        //    _tran.FECH_HORA_TRAN = Convert.ToDateTime(row["FECH_HORA_TRAN"]);

        //                _tran.IDUSU = row["IDUSU"].ToString();
        //    _tran.USU_NOM = row["USU_NOM"].ToString();

        //    _tran.IDROL = row["IDROL"].ToString();
        //    _tran.NOM_ROL = row["NOM_ROL"].ToString();

        //    _tran.NOM_EQUIPO = row["NOM_EQUIPO"].ToString();
        //    _tran.ACCION = row["ACCION"].ToString();
        //    _tran.IP_LOCAL = row["IP_LOCAL"].ToString();
    }
}
