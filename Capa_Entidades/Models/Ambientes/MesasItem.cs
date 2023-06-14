using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Ambientes
{
    public class MesasItem
    {
        public int ID { get; set; }
        public string M_NOM { get; set; }
        public int M_EST { get; set; }
        public int M_ID_AMB { get; set; }
        public string A_NOM { get; set; }
        public string M_X { get; set; }
        public int M_ATENDIDA { get; set; }
        public int M_WIDTH { get; set; }
        public int M_HEIGHT { get; set; }
        public string M_TEXTO { get; set; }
        public int M_TIPO { get; set; }
        public Boolean M_ACT { get; set; }
        public DateTime M_F_CREATE { get; set; } = DateTime.Now;
        public DateTime M_F_MODIFICACION { get; set; }
        public int M_ID_PADRE { get; set; }
        public string color { get; set; }
        public string NOMBRE_PADRE { get; set; }
        public string M_NOMBRE_TIPO { get; set; }
        public string ESTADO_MESA { get; set; }


        public int ID_TM { get; set; }
        public string TM_DESCR { get; set; }
        public bool TM_ACT { get; set; }
        public DateTime TM_F_CREATE { get; set; }
        public DateTime TM_F_MODIFICACION { get; set; }

        public int NroColumnas { get; set; }
        public int A_X { get; set; }
        public int A_Y { get; set; }
        public int A_TOP { get; set; }
        public int A_BOTTOM { get; set; }
        public int PED_ID_CLIENTE { get; set; }
        public string C_NOMINA { get; set; }
        public string nomllevar { get; set; }
        public string EMPL_NOM { get; set; }
        public int mesa { get; set; }
        public MesasItem()
        {

        }
        public MesasItem(int ID, String M_NOM, int M_EST, int M_ID_AMB,string A_NOM, string M_X, int M_ATENDIDA, int M_WIDTH,
            int M_HEIGHT, string M_TEXTO, int M_TIPO, Boolean M_ACT, DateTime M_F_CREATE, string color, int M_ID_PADRE, 
            DateTime M_F_MODIFICACION, string NOMBRE_PADRE,string M_NOMBRE_TIPO, string ESTADO_MESA, int PED_ID_CLIENTE, string C_NOMINA, string nomllevar,string EMPL_NOM,int mesa)
        {
            this.ID = ID;
            this.M_NOM = M_NOM;
            this.M_EST = M_EST;
            this.M_ID_AMB = M_ID_AMB;
            this.A_NOM = A_NOM;
            this.M_X = M_X;
            this.M_ATENDIDA = M_ATENDIDA;
            this.M_WIDTH = M_WIDTH;
            this.M_HEIGHT = M_HEIGHT;
            this.M_TEXTO = M_TEXTO;
            this.M_TIPO = M_TIPO;
            this.M_ACT = M_ACT;
            this.M_F_CREATE = M_F_CREATE;
            if (M_EST == 1)
            {
                this.color = "red";
            }
            else if (M_EST == 2)
            {
                this.color = "skyblue";
            }
            else if (M_EST == 0)
            {
                this.color = "green";
            }
            this.M_ID_PADRE = M_ID_PADRE;
            this.M_F_MODIFICACION = M_F_MODIFICACION;
            this.NOMBRE_PADRE = NOMBRE_PADRE;
            this.M_NOMBRE_TIPO = M_NOMBRE_TIPO;
            this.ESTADO_MESA = ESTADO_MESA;
            this.PED_ID_CLIENTE = PED_ID_CLIENTE;
            this.C_NOMINA = C_NOMINA;
            this.nomllevar = nomllevar;
            this.EMPL_NOM = EMPL_NOM;
            this.mesa = mesa;
        }
    }
}
