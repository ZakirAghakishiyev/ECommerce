using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.EfCore;
using ECommerce.Infrastructure.EfCore.Context;
using System;

namespace ECommerce.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set up global exception handling
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;

            try
            {
                CmdInterface entering = new CmdInterface();

                AppDbContext appDbContext = new();
                ICategoryRepository categoryRepository = new CategoryRepository(appDbContext);
                ICategoryService categoryService = new CategoryManager(categoryRepository);

                IUserRepository UserRepository = new UserRepository(appDbContext);
                IUserService UserService = new UserManager(UserRepository);

                IOrderRepository OrderRepository = new OrderRepository(appDbContext);
                IOrderService OrderService = new OrderManager(OrderRepository);

                IOrderItemRepository OrderItemRepository = new OrderItemRepository(appDbContext);
                IOrderItemService OrderItemService = new OrderItemManager(OrderItemRepository);

                IProductRepository ProductRepository = new ProductRepository(appDbContext);
                IProductService ProductService = new ProductManager(ProductRepository);

                IBasketService basketService = new BasketManager();


                //OrderService.Get(x=>x.Id==1);
                // Execute main application
                entering.App(UserService, ProductService, OrderService, categoryService);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine(ex.Message);
            }
        }

        // Global exception handler
        private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Console.WriteLine("A critical error occurred in the application:");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
            else
            {
                Console.WriteLine("An unknown critical error occurred.");
            }
        }
    }
}
