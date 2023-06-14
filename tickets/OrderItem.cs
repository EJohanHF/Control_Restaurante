using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tickets
{
    class OrderItem
    {
        private char[] delimitador = new char[]
		{
			'?'
		};

        public OrderItem(char delimit)
        {
            this.delimitador = new char[]
			{
				delimit
			};

        }
        public string GenerateItem2(string cantidad, string itemName, string punitario, string price)
        {
            return string.Concat(new object[]
            {
                cantidad,
                this.delimitador[0],
                itemName,
                this.delimitador[0],
                punitario,
                this.delimitador[0],
                price
            });
        }
        public string GetItemPriceUni(string orderItem)
        {
            string[] delimitado = orderItem.Split(this.delimitador);
            return delimitado[1];
        }
        public string GetItemCantidad(string orderItem)
        {
            string[] delimitado = orderItem.Split(this.delimitador);
            return delimitado[0];
        }

        public string GetItemName(string orderItem)
        {
            string[] delimitado = orderItem.Split(this.delimitador);
            return delimitado[1];
        }
        public string GetItemNameTicket(string orderItem)
        {
            string[] delimitado = orderItem.Split(this.delimitador);
            return delimitado[0];
        }

        public string GetItemPrice(string orderItem)
        {
            string[] delimitado = orderItem.Split(this.delimitador);
            return delimitado[2];
        }

        public string GenerateItem(string cantidad, string itemName, string price)
        {
            return string.Concat(new object[]
			{
				cantidad,
				this.delimitador[0],
				itemName,
				this.delimitador[0],
				price
			});
        }
        public string GenerateItemRptPI(string carta, string cantidad)
        {
            return string.Concat(new object[]
            {
                carta,
                this.delimitador[0],
                cantidad
            });
        }
        public string GenerateItemSinCorte(string cantidad, string itemName, string price)
        {
            return string.Concat(new object[]
			{
				cantidad,
                this.delimitador[0],
                itemName,
                this.delimitador[0],
                price
			});
        }
    }
}
