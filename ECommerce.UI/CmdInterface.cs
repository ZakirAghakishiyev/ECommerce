using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace ECommerce.UI;

public class CmdInterface

{
    public void App(IUserService userService, IProductService productService, IOrderService orderService, ICategoryService categoryService)
    {
        var user=Enterance(userService);
        if (user.Role == 0)
        {
            Admin(userService, productService, orderService, categoryService);   
        }
        else
        {
            Customer(productService, orderService, user);
        }

    }

    public void Admin(IUserService userService, IProductService productService, IOrderService orderService, ICategoryService categoryService)
    {
        while (true)
        {
            Console.WriteLine("1. View All Users");
            Console.WriteLine("2. Manage Products");
            Console.WriteLine("3. View Orders");
            Console.WriteLine("4. Manage Categories");
            Console.WriteLine("5. Logout");

            bool isValid = int.TryParse(Console.ReadLine(), out int input);
            if (!isValid || input > 5)
            {
                Console.WriteLine("Invalid Input");
                continue;
            }

            switch (input)
            {
                case 1:
                    ManageUsers(userService);
                    break;
                case 2:
                    ManageProducts(productService);
                    break;
                case 3:
                    ViewOrders(orderService);
                    break;
                case 4:
                    ManageCategories(categoryService);
                    break;
                case 5:
                    return;
            }
        }
    }

    public void ManageUsers(IUserService userService)
    {
        while (true)
        {
            Console.WriteLine("1. View All Users");
            Console.WriteLine("2. Delete User");
            Console.WriteLine("3. Add User");
            Console.WriteLine("4. Back to Admin Menu");

            bool isValid = int.TryParse(Console.ReadLine(), out int input);
            if (!isValid || input > 3)
            {
                Console.WriteLine("Invalid Input");
                continue;
            }

            if (input == 1)
            {
                var users = userService.GetAll(null, true);
                Printer.UsersPrinter(users);
            }
            else if (input == 2)
            {
                Console.Write("Enter User ID to Delete: ");
                isValid = int.TryParse(Console.ReadLine(), out int userId);
                if (!isValid || userService.GetById(userId) == null)
                {
                    Console.WriteLine("Invalid User ID");
                    continue;
                }
                userService.Remove(userId);
                Console.WriteLine("User Deleted Successfully");
            }
            else if (input == 3)
            {
                Console.Write("Firstname: ");
                string firstname = Console.ReadLine();
                Console.Write("Lastname: ");
                string lastname = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                Console.WriteLine("Select role (0-Seller, 1-Customer): ");
                Roles role = (Roles)int.Parse(Console.ReadLine());

                userService.Add(new UserCreateDto { Email = email, Password = password, FirstName=firstname, LastName=lastname, Role=role });
                Console.WriteLine("User Added Successfully");
            }
            else break;
        }
    }

    public void ManageCategories(ICategoryService categoryService)
    {
        while (true)
        {
            Console.WriteLine("1. View All Categories");
            Console.WriteLine("2. Add Category");
            Console.WriteLine("3. Delete Category");
            Console.WriteLine("4. Back to Admin Menu");

            bool isValid = int.TryParse(Console.ReadLine(), out int input);
            if (!isValid || input > 4)
            {
                Console.WriteLine("Invalid Input");
                continue;
            }

            if (input == 1)
            {
                var categories = categoryService.GetAll(null,false);
                Printer.CategoriesPrinter(categoryService.GetAll(null,false));
            }
            else if (input == 2)
            {
                Console.Write("Enter Category Name: ");
                string categoryName = Console.ReadLine();
                categoryService.Add(new CategoryCreateDto { Name = categoryName });
                Console.WriteLine("Category Added Successfully");
            }
            else if (input == 3)
            {
                Console.Write("Enter Category ID to Delete: ");
                isValid = int.TryParse(Console.ReadLine(), out int categoryId);
                if (!isValid || categoryService.GetById(categoryId) == null)
                {
                    Console.WriteLine("Invalid Category ID");
                    continue;
                }
                categoryService.Remove(categoryId);
                Console.WriteLine("Category Deleted Successfully");
            }
            else break;
        }
    }

    public void ManageProducts(IProductService productService)
    {
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. Update Product");
        Console.WriteLine("3. Remove Product");

        bool isValid = int.TryParse(Console.ReadLine(), out int choice);
        if (!isValid || choice > 3)
        {
            Console.WriteLine("Invalid Input");
            return;
        }

        switch (choice)
        {
            case 1:
                Console.Write("Product Name: ");
                string name = Console.ReadLine();
                Console.Write("Description: ");
                string description= Console.ReadLine();
                Console.Write("Category ID: ");
                int category = int.Parse(Console.ReadLine());
                Console.Write("Price: ");
                decimal price = decimal.Parse(Console.ReadLine());
                productService.Add(new ProductCreateDto { Name = name, CategoryId = category, Price = price, Description=description });
                Console.WriteLine("Product added successfully.");
                break;

            case 2:
                Console.Write("Product ID to update: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                var product = productService.GetById(productId);
                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    return;
                }
                Console.Write("New Price: ");
                product.Price = decimal.Parse(Console.ReadLine());
                productService.Update(new ProductUpdateDto { Id=product.Id, CategoryId=product.Category.Id, Description=product.Description, Name=product.Name, Price=product.Price});
                Console.WriteLine("Product updated successfully.");
                break;

            case 3:
                Console.Write("Product ID to remove: ");
                int removeId = int.Parse(Console.ReadLine());
                productService.Remove(removeId);
                Console.WriteLine("Product removed successfully.");
                break;
        }
    }

    public void ViewOrders(IOrderService orderService)
    {
        var orders = orderService.GetAll(null, false);
        if (orders == null || !orders.Any())
        {
            Console.WriteLine("No orders found.");
            return;
        }
        Printer.OrdersPrinter(orders);
        Console.Write("Enter Order ID to update status: ");
        int orderId = Convert.ToInt32(Console.ReadLine());
        var selectedOrder = orderService.GetById(orderId);
        if (selectedOrder == null)
        {
            Console.WriteLine("Invalid Order ID.");
            return;
        }
        Console.Write("New Status (Pending, Shipped, Delivered): ");
        selectedOrder.Status = (Status)Enum.Parse(typeof(Status), Console.ReadLine(), true);
        orderService.Update(new OrderUpdateDto { Id=selectedOrder.Id, Status=selectedOrder.Status});
        Console.WriteLine("Order status updated.");
    }




    public void Customer(IProductService productService, IOrderService orderService, UserDto user)
    {
        var basket = new BasketDto();
        while (true)
        {
            Console.WriteLine("1. Search Product");
            Console.WriteLine("2. Add to Basket");
            Console.WriteLine("3. View Basket");
            Console.WriteLine("4. View My Orders");
            Console.WriteLine("5. View Discount Availability");
            Console.WriteLine("6. Logout");


            bool isValid = int.TryParse(Console.ReadLine(), out int input);
            if (!isValid || input > 6)
            {
                Console.WriteLine("Invalid Input");
                continue;
            }

            if (input == 1)
            {
                Console.WriteLine("1. Search by Category");
                Console.WriteLine("2. Search by Name");
                Console.WriteLine("3. Show All");
                isValid = int.TryParse(Console.ReadLine(), out int inputForSearch);
                if (!isValid || inputForSearch > 3)
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }
                if (inputForSearch == 1)
                {
                    Console.Write("Category Name: ");
                    string category = Console.ReadLine();
                    List<ProductDto> products = productService.GetAll(x => x.Category.Name == category, true);
                    if (products == null || !products.Any())
                    {
                        Console.WriteLine("Invalid input!!!");
                        continue;
                    }
                    Printer.ProductsPrinter(products);
                }
                else if(inputForSearch==2)
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    List<ProductDto> products = productService.GetAll(x => x.Name == name, true);
                    if (products == null || !products.Any())
                    {
                        Console.WriteLine("Invalid input!!!");
                        continue;
                    }
                    Printer.ProductsPrinter(products);
                }
                else
                {
                    Printer.ProductsPrinter(productService.GetAll(null,false));
                }
            }
            else if (input == 2) AddToBasket(productService, basket);
               
            else if (input == 3) ViewBasket(user, basket, orderService);
            
            else if (input == 4) Printer.DetailedOrdersPrinter(orderService.GetAll(x => x.User.Id == user.Id, false));
            
            else if (input == 5) CheckDiscountAvailability(user);
            
            else if (input == 6) return;
        }
    }


    public void AddToBasket(IProductService productService, BasketDto basket)
    {
        Console.Write("Product ID: ");
        bool isProduct = int.TryParse(Console.ReadLine(), out int productId);
        if (!isProduct || productService.GetById(productId) == null)
        {
            Console.WriteLine("Invalid product ID.");
            return;
        }

        Console.Write("Quantity: ");
        isProduct = int.TryParse(Console.ReadLine(), out int count);
        if (!isProduct || count <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        var product = productService.GetById(productId);
        var existingItem = basket.BasketItems.FirstOrDefault(b => b.Product.Id == productId);
        if (existingItem != null)
        {
            existingItem.Count += count;
        }
        else
        {
            basket.BasketItems.Add(new BasketItemDto { Product = product, Count = count });
        }

        Console.WriteLine($"{count} x {product.Name} added to basket.");
        Printer.BasketPrinter(basket);
    }

    public void OrderCompletion(UserDto user, BasketDto basket, IOrderService orderService)
    {
        if (!basket.BasketItems.Any())
        {
            Console.WriteLine("Your basket is empty. Please add items before proceeding.");
            return;
        }

        orderService.Add(ConvertBasketToOrder(basket, user));
        basket.BasketItems.Clear();
        Console.WriteLine("Order has been successfully completed!");
    }

    public void CheckDiscountAvailability(UserDto user)
    {
        Console.WriteLine("You get 20% discount for your 1st and every 5th order.");

        int orderCount = user.Orders.Count();
        if (orderCount == 0 || orderCount % 5 == 4)
        {
            Console.WriteLine("You will get a discount on your upcoming order!");
        }
        else
        {
            Console.WriteLine($"You will receive a discount in {5 - orderCount % 5} orders.");
        }
    }


    public void ViewBasket(UserDto user, BasketDto basket, IOrderService orderService)
    {
        if (!basket.BasketItems.Any())
        {
            Console.WriteLine("Your basket is empty.");
            return;
        }

        Printer.BasketPrinter(basket);
        Console.WriteLine("1. Buy");
        Console.WriteLine("2. Back to Home");
        Console.Write("Choose an option: ");

        bool isValid = int.TryParse(Console.ReadLine(), out int input);
        if (!isValid || input > 2 || input < 1)
        {
            Console.WriteLine("Invalid Input. Please enter 1 or 2.");
            return;
        }

        if (input == 1)
        {
            OrderCompletion(user, basket, orderService);
            Printer.DetailedOrdersPrinter(orderService.GetAll(x => x.UserId == user.Id, false));
        }
    }

    public UserDto Enterance(IUserService userService)
    {
        UserDto user = null;
        while(user==null)
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");

            bool isValid = int.TryParse(Console.ReadLine(), out int input);
            if (!isValid || input > 2)
            {
                Console.WriteLine("Invalid Input");
                continue;
            }
       
            if (input == 1)
            {
                user = Login(userService);
            }
            else
            {
                user = Register(userService);
            }
        }
        return user;
    }

    public UserDto Login(IUserService userService)
    {
        UserDto? user = new();
        bool firstTime = true;
        do
        {
            if (firstTime)
            {
                Console.WriteLine("Please enter your Username and Password");
                firstTime = false;
            }
            else Console.WriteLine("Wrong Username or Password, Try Again");
            Console.Write("Firstname: ");
            string firstname = Console.ReadLine();
            Console.Write("Lastname: ");
            string lastname = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            user = userService.Get(x => x.LastName == lastname && x.FirstName == firstname && x.Password == password);
        } while (user == null);
        Console.WriteLine($"Welcome {user.FirstName}");
        return user;
    }

    public UserDto Register(IUserService userService)
    {
        Console.Write("Firstname: ");
        string firstname = Console.ReadLine();
        Console.Write("Lastname: ");
        string lastname = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        bool isValid = false;
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        string email;
        bool firstTime= true;
        do
        {
            if(!firstTime) Console.WriteLine("Invalid Email!!!");
            firstTime = false;
            Console.Write("Email: ");
            email = Console.ReadLine();
            isValid = Regex.IsMatch(email, pattern);
        } while (!isValid);

        Roles role = Roles.Customer;
        var user = new UserDto { Email = email, FirstName = firstname, LastName = lastname, Role = role };
        userService.Add(new UserCreateDto { Email = email, FirstName = firstname, LastName = lastname, Password = password, Role = role });
        Console.WriteLine("Account has been successfully created");
        return user;
    }

    public static decimal? PriceCalculator(BasketDto basket)
    {
        decimal? sum = 0m;
        foreach(var item in basket.BasketItems)
        {
            var ItemPrice = item.Count * item.Product.Price;
            sum += ItemPrice;
        }
        return sum;
    }

    public static OrderCreateDto ConvertBasketToOrder(BasketDto basket, UserDto user)
    {
        if (basket == null || basket.BasketItems == null || !basket.BasketItems.Any())
        {
            throw new ArgumentException("Basket is empty or null.");
        }
        decimal discount = 1m;
        if (user.Orders.Count() == 0 || user.Orders.Count() % 5 == 4) discount = 0.8m;
        return new OrderCreateDto
        {
            UserId = user.Id,
            Items = basket.BasketItems.Select(b => new OrderItemDto
            {
                Product = b.Product,
                Count = b.Count
            }).ToList(),
            Status = Status.Pending,
            TotalPrice = PriceCalculator(basket) * discount
        };
        
    }

}
