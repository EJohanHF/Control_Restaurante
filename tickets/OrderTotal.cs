using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tickets
{
    class OrderTotal
    {
        private char[] delimitador = new char[]
		{
			'?'
		};

        public OrderTotal(char delimit)
        {
            this.delimitador = new char[]
			{
				delimit
			};
        }

        public string GetTotalName(string totalItem)
        {
            string[] delimitado = totalItem.Split(this.delimitador);
            return delimitado[0];
        }

        public string GetTotalCantidad(string totalItem)
        {
            string[] delimitado = totalItem.Split(this.delimitador);
            return delimitado[1];
        }

        public string GenerateTotal(string totalName, string price)
        {
            return totalName + this.delimitador[0] + price;
        }
    }
}
