using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public partial class MessageBoxDialog : Form
    {
        public MessageBoxDialog()
        {
            InitializeComponent();
        }

        private void MessageBoxDialog_Load(object sender, EventArgs e)
        {

        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
