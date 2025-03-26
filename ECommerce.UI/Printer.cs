using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.UI
{
    public static class Printer
    {
        public static void BasketPrinter(BasketDto basket)
        {
            int no = 1;
            decimal? sum= 0m;
            if (basket == null)
            {
                Console.WriteLine("Basket is Empty");
                return;
            }
            foreach(var item in basket.BasketItems)
            {
                var ItemPrice = item.Count * item.Product.Price;
                sum += ItemPrice;
                Console.WriteLine($"No: {no++}");
                ProductPrinter(item.Product);
                Console.WriteLine($"Quantity: {item.Count}");
                Console.WriteLine($"Total: {ItemPrice}");
                Console.WriteLine(new string('-',95));
                Console.WriteLine();
            }
        }

        public static void ProductPrinter(ProductDto product, bool isUsedForAll = false)
        {
            if (product == null) return;
            if (!isUsedForAll)
            {
                Console.WriteLine($"|{"ID",-7}|{"Product name",-25}|{"Category",-20}|{"Description",-30}|{"Price",-6}|");
                Console.WriteLine(new string('-', 95));
            }
            Console.WriteLine($"|{product.Id,-7}|{product.Name,-25}|{product.Category.Name,-20}|{product.Description,-30}|{product.Price,-6}|");

        }

        public static void ProductsPrinter(List<ProductDto> products)
        {

            Console.WriteLine($"|{"ID",-7}|{"Product name",-25}|{"Category",-20}|{"Description",-30}|{"Price",-6}|");
            Console.WriteLine(new string('-', 95));
            foreach(var item in products)
            {
                ProductPrinter(item, true);
            }

        }


        public static void OrderPrinter(OrderDto order, bool isUsedForAll = false)
        {
            if (order == null) return;
            if (!isUsedForAll)
            {
                Console.WriteLine($"|{"ID",-7}|{"User ID",-7}|{"Date",-20}|{"Total Price",-11}|{"Status",-10}|");
                Console.WriteLine(new string('-', 61));
            }
            Console.WriteLine($"|{order.Id,-7}|{order.User.Id,-7}|{order.CreatedAt,-20}|{order.TotalPrice,-11}|{order.Status,-10}|");

        }

        public static void DetailedOrderPrinter(OrderDto order)
        {
            if (order == null) return;

            Console.WriteLine($"|{"ID",-7}|{"User Name",-45}|{"Date",-20}|{"Total Price",-11}|{"Status",-10}|");
            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"|{order.Id,-7}|{order.User.FirstName+" "+order.User.LastName,-45}|{order.CreatedAt,-20}|{order.TotalPrice,-11}|{order.Status.ToString(),-10}|");
            Console.WriteLine(new string('-', 110));

            foreach (var item in order.Items)
            {
                OrderDetailPrinter(item);
            }
        }

        public static void DetailedOrdersPrinter(List<OrderDto> orders)
        {
            foreach (var item in orders)
            {
                DetailedOrderPrinter(item);
            }
        }

        public static void OrdersPrinter(List<OrderDto> orders)
        {
            Console.WriteLine($"|{"ID",-7}|{"User ID",-7}|{"Date",-20}|{"Total Price",-11}|{"Status",-10}|");
            Console.WriteLine(new string('-', 61));
            foreach (var item in orders)
            {
                OrderPrinter(item, true);
            }
        }

        public static void OrderDetailPrinter(OrderItemDto item)
        {
            Console.WriteLine($"|{"Id",-4}|{"Order Id",-8}|{"Product Name",-25}|{"Price",-7}|{"Quantity",-8}|{"Total Price",-11}|");
            Console.WriteLine(new string('-',70));
            Console.WriteLine($"|{item.Id,-4}|{item.OrderId,-8}|{item.Product.Name,-25}|{item.Product.Price,-7}|{item.Count,-8}|{item.Product.Price*item.Count,-11}|");
        }

        public static void OrderDetailsPrinter(List<OrderItemDto> items)
        {
            Console.WriteLine($"|{"Id",-4}|{"Order Id",-8}|{"Product Name",-25}|{"Price",-7}|{"Quantity",-8}|{"Total Price",-11}|");
            Console.WriteLine(new string('-', 70));
            foreach(var item in items)
            {
                Console.WriteLine($"|{item.Id,-4}|{item.OrderId,-8}|{item.Product.Name,-25}|{item.Product.Price,-7}|{item.Count,-8}|{item.Product.Price * item.Count,-11}|");
            }
        }
        public static void UserPrinter(UserDto user, bool isUsedForAll = false, bool passwordVisible=false)
        {
            if (user == null) return;
            //string password= passwordVisible ? "" :$"{user.}"
            if (!isUsedForAll)
            {
                Console.WriteLine($"|{"ID",4}|{"Firstname",-20}|{"Lastname",-25}|{"Email",-30}|{"Role",-10}|");
                Console.WriteLine(new string('-', 94));
            }
            Console.WriteLine($"|{user.Id,4}|{user.FirstName,-20}|{user.LastName,-25}|{user.Email,-30}|{user.Role.ToString(),-10}|");
        }

        public static void UsersPrinter(List<UserDto> users)
        {
            if (users == null) return;
            Console.WriteLine($"|{"ID",4}|{"Firstname",-20}|{"Lastname",-25}|{"Email",-30}|{"Role",-10}|");
            Console.WriteLine(new string('-', 94));
            foreach(var item in users)
            {
                UserPrinter(item, true);
            }
        }

        public static void CategoryPrinter(CategoryDto category, bool isUsedForAll = false)
        {
            if (!isUsedForAll)
            {
                Console.WriteLine($"|{"ID",-4}|{"Category",-20}|");
                Console.WriteLine(new string('-', 27));
            }
            Console.WriteLine($"|{category.Id,-4}|{category.Name,-20}|");
            //if (!isUsedForAll)
            //{
            //    foreach(var item in category.Products)
            //    {
            //        ProductPrinter(item);
            //    }
            //}
        }
        public static void CategoriesPrinter(List<CategoryDto> categories)
        {
            Console.WriteLine($"|{"ID",-4}|{"Category",-20}|");
            Console.WriteLine(new string('-', 27));
            foreach (var item in categories)
            {
                CategoryPrinter(item, true);
            }
        }
    }
}
