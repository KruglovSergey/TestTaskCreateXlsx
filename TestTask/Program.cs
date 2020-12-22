using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using TestTask.Models;

namespace TestTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ApplicationContext db = new ApplicationContext();
            DBInitializer.Initialize(db);

            IQueryable<Orders> orders = db.Orders
                .Include(x => x.Shops)
                .Include(x => x.OrderComponents)
                   .ThenInclude(x => x.ProductLibrary);

            CreateOrdersXlsx(orders);

            Console.WriteLine($"Заказы (таблицы .xlsx) создались в директории: ~/bin/Debug/");
            Console.ReadKey();
        }

        public static void CreateOrdersXlsx(IQueryable<Orders> orders)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(new FileInfo(Path.GetFullPath("Template.xlsx")));
            var sheet = package.Workbook.Worksheets[0];

            //Условие, что выбираем заказы, где заказа пустой, и проставлена дата согласования.
            var filteredOrders = orders.Where(x => x.DraftOrder == null && x.DateOrderAgreed != null).ToList();

            if (filteredOrders != null)
            {
                foreach (var order in filteredOrders)
                {
                    var orderNumber = order.Id;
                    var shopNumber = order.ShopsId;
                    var address = order.Shops.Address;

                    // #1
                    sheet.Cells["B2"].Value = $"Заказ № {orderNumber}";
                    // #2 и #3
                    sheet.Cells["B4"].Value = $"Магазин № {shopNumber} по адресу {address}";
                    var productName = order.OrderComponents.ProductLibrary.Name;
                    var unit = order.OrderComponents.ProductLibrary.Unit;
                    var price = order.OrderComponents.ProductLibrary.Price;
                    var amount = order.OrderComponents.Amount;

                    var checkPrice = double.TryParse(price, out double priceDouble);

                    var director = order.Shops.Director;

                    //#4
                    sheet.Cells["B7"].Value = $"{productName}";
                    //#5
                    sheet.Cells["C7"].Value = $"{unit}";
                    //#6
                    sheet.Cells["D7"].Value = $"{price}";
                    //#7
                    sheet.Cells["E7"].Value = $"{amount}";
                    //#8
                    sheet.Cells["F7"].Value = $"{amount * priceDouble}";

                    sheet.Cells["A8"].Value = "Итого";
                    sheet.Cells["F8"].Formula = " =F7";
                    sheet.Cells["A9"].Value = "НДС";
                    sheet.Cells["F9"].Formula = $"=F8*0.2";
                    sheet.Cells["F10"].Value = "Итого с НДС";
                    sheet.Cells["F10"].Formula = $"=F8+F9";

                    sheet.Cells["C12"].Value = $"Директор({director})";

                    package.SaveAs(new FileInfo(Path.GetFullPath($"Заказ № {orderNumber}.xlsx")));
                }
            }
            else
            {
                Console.WriteLine("Список заказов пуст");
                Console.ReadKey();
            }
        }
    }
}