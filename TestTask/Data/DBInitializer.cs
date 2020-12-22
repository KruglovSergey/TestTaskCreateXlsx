using System;
using System.Linq;

namespace TestTask.Models
{
    internal class DBInitializer
    {
        /// <summary>
        /// Создаем объекты в БД
        /// </summary>
        public static void Initialize(ApplicationContext context)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //создаем объекты, если их нет
                if (!db.Orders.Any())
                {
                    Orders orders = new Orders { Id = 1, DraftOrder = null, ShopsId = 1, OrderAgreed = true, DateOrderAgreed = DateTime.Now };
                    Orders orders1 = new Orders { Id = 2, DraftOrder = "Черновик", ShopsId = 1, OrderAgreed = false, DateDraftOrder = DateTime.Now };
                    Orders orders2 = new Orders { Id = 3, DraftOrder = null, ShopsId = 1, OrderAgreed = true, DateOrderAgreed = DateTime.Now };

                    ProductLibrary productLibrary = new ProductLibrary { Id = 1, Name = "Кабачки", Unit = "кг", Price = "100" };
                    ProductLibrary productLibrary1 = new ProductLibrary { Id = 2, Name = "Баклажаны", Unit = "кг", Price = "110" };
                    ProductLibrary productLibrary2 = new ProductLibrary { Id = 3, Name = "Капуста белокочанная", Unit = "кг", Price = "120" };

                    OrderComponents orderComponents = new OrderComponents { Id = 1, ProductCode = 1, Amount = 2, OrderId = 1 };
                    OrderComponents orderComponents1 = new OrderComponents { Id = 2, ProductCode = 2, Amount = 4, OrderId = 2 };
                    OrderComponents orderComponents2 = new OrderComponents { Id = 3, ProductCode = 3, Amount = 6, OrderId = 3 };

                    Shops shops = new Shops { Id = 1, Name = "Магазин №1", Address = "г. Москва, ул. Красная Площадь, дом 1", Director = "Ульянов И.И." };

                    // добавляем объекты
                    db.ProductLibraries.Add(productLibrary);
                    db.ProductLibraries.Add(productLibrary1);
                    db.ProductLibraries.Add(productLibrary2);

                    db.OrderComponents.Add(orderComponents);
                    db.OrderComponents.Add(orderComponents1);
                    db.OrderComponents.Add(orderComponents2);

                    db.Orders.Add(orders);
                    db.Orders.Add(orders1);
                    db.Orders.Add(orders2);

                    db.Shops.Add(shops);
                }

                // сохраняем
                db.SaveChanges();
            }
        }
    }
}