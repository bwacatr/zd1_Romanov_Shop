using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd1_Romanov_Shop
{
    class Product
    {
        private decimal price;
        private string name;
        private int amount;

        public Product(string Name, decimal Price, int amount) // Конструктор с именем и ценой
        {
            name = Name;
            price = Price;
            this.amount = amount;
        }

        public decimal Price // свойство цены
        {
            get { return price; }
            set { price = value; }
        }

        public string Name // свойство названия
        {
            get { return name; }
            set { name = value; }
        }

        public int Amount // свойство кол-ва товара
        {
            get { return amount; }
            set { amount = value; }
        }


    }
}
