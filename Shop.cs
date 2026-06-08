using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd1_Romanov_Shop
{
    class Shop
    {
        private decimal gain;
        public Dictionary<Product, int> products;

        public decimal Gain
        {
            get { return gain; }
            set { gain = value; }
        }


        public Shop() // конструктор магазина, создающий словарь
        {
            products = new Dictionary<Product, int>();

        }

        public void AddProduct(Product product, int count) // метод, добавляющий продукт и его кол-во в словарь
        {
            products.Add(product, count);
        }

        public void CreateProduct(string name, decimal price, int count)  // метод, создающий и добавляющий продукт и его кол-во в словарь
        {
            products.Add(new Product(name, price, count), count);
        }

        public void Sell(Product product) // метод, продающий одну единицу продукта
        {
            if (products.ContainsKey(product))
            {
                if (products[product] == 0)
                {
                    Console.WriteLine("Нет в наличии!");
                }
                else
                {
                    gain += product.Price;
                    products[product]--;
                    product.Amount--;
                }
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }
        }

        public void Sell(string productName) // перегрузка метода Sell, которая находит и продает одну единицу продукта
        {
            Product ToSell = FindByName(productName);
            if (ToSell != null)
            {
                this.Sell(ToSell);
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }




        }

        public Product FindByName(string name) // метод, который находит продукт по его названию
        {
            foreach (var item in products.Keys)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }

        public List<Product> FindMultipleByName(string name) // метод, который находит продукты по названию и вставляет их в список, который позже возвращает
        {
            List<Product> items = new List<Product>();
            foreach (var item in products.Keys)
            {
                if (item.Name == name)
                {
                    items.Add(item);

                }
            }
            return items;
        }
    }
}
