using EF_session.Db;
using EF_session.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client.NativeInterop;
using System.Collections;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_session
{
    internal class Program
    {
        static StoreDbContext db = new StoreDbContext();


        #region Level4Q23
        //Question 23
        public static ProductSearchResult SearchProducts(ProductFilter f)
        {
            IQueryable<Product> Products = db.Products;
            if (f.Text is not null)
            {
                Products = Products.Where(p => p.Name.Contains(f.Text));
            }
            if (f.MaxPrice is not null)
            {
                Products = Products.Where(p => p.Price <= f.MaxPrice);
            }
            if (f.MinPrice is not null)
            {
                Products = Products.Where(p => p.Price >= f.MinPrice);
            }
            if (f.Category is not null)
            {
                Products = Products.Where(p => p.Category.Name == f.Category);
            }
            if (f.InStockOnly)
            {
                Products = Products.Where(p => p.Stock > 0);
            }
            if (f.SortBy is not null)
            {
                if (f.SortBy == "name")
                {
                    if (!f.IsDescending)
                        Products = Products.OrderBy(p => p.Name);
                    else
                        Products = Products.OrderByDescending(p => p.Name);
                }
                else if (f.SortBy == "price")
                {
                    if (!f.IsDescending)
                        Products = Products.OrderBy(p => p.Price);
                    else
                        Products = Products.OrderByDescending(p => p.Price);
                }
                else if (f.SortBy == "rating")
                {
                    if (!f.IsDescending)
                        Products = Products.OrderBy(p => p.Reviews.Average(r => r.Rating));
                    else
                        Products = Products.OrderByDescending(p => p.Reviews.Average(r => r.Rating));
                }
            }
            int totalCount = Products.Count();
            var pagedList = Products.Skip((f.PageNumber - 1) * f.PageSize)
                                    .Take(f.PageSize)
                                    .ToList();
            return new ProductSearchResult
            {
                Products = pagedList,
                TotalCount = totalCount
            };
        } 
        #endregion
        static void Main(string[] args)
        {

            #region Level1


            //1_For every delivered order, return OrderId, CustomerName, and Total, ordered by Total descending

            //var deliveredOrder =db.Orders.Where(o => o.Status == OrderStatus.Delivered).Select(o=>new { o.Id , CustomerName= o.Customer.FullName ,Total=o.Items.Sum(i=>i.Quantity*i.UnitPrice*(1-i.Discount))}).OrderByDescending(o=>o.Total).ToList();
            //foreach (var item in deliveredOrder)
            //{
            //    Console.WriteLine($"Order: {item.Id}, Customer: {item.CustomerName}, Total: {item.Total}");
            //}

            //2.Count how many orders exist per status, with the percentage of the whole.

            //double totalorders = db.Orders.Count();

            //var count = db.Orders.GroupBy(o => o.Status).Select(g => new { g.Key, Count= g.Count(), precentage= (g.Count() /totalorders) * 100 }).ToList();
            //foreach (var item in count)
            //{
            //    Console.WriteLine($"Order: {item.Key}, Count: {item.Count}, precentage: {item.precentage:F2}%");
            //}

            //3.Return products whose price is above the average price of their own category.

            //var productAbove = db.Products.Where(p=>p.Price>db.Products.Where(g=>g.Category==p.Category).Average(g=>g.Price)).Select(p=>new { Name=p.Name,Price=p.Price,Category=p.Category }).ToList();

            //foreach (var item in productAbove)
            //{
            //    Console.WriteLine($"Product: {item.Name}, price: {item.Price}, Category: {item.Category.Name}");
            //}


            //4.Return products that were never ordered.

            //var NonOrderedProducts = db.Products.Where(p =>! p.OrderItems.Any() ).Select(p=>p.Name).ToList();

            //foreach (var item in NonOrderedProducts)
            //{
            //    Console.WriteLine($"Product: {item}");
            //}

            //5.Return customers who registered in the last 30 days and have at least one order.

            //var Customers = db.Customers.Where(c => c.RegisteredAt >= DateTime.UtcNow.AddDays(-30)&& c.Orders.Any()).Select(p => new {p.FullName,p.Email}).ToList();

            //foreach (var item in Customers)
            //{
            //    Console.WriteLine($"Name: {item.FullName},Email: {item.Email}");
            //}

            //6.Return customers who cancelled more than 2 orders, with the number of cancelled orders.

            // var Customers = db.Customers.Where(c => c.Orders.Where(o=>o.Status==OrderStatus.Cancelled).Count()>2).Select(p => new { p.FullName, Count= p.Orders.Where(o => o.Status == OrderStatus.Cancelled).Count() }).ToList();

            //foreach (var item in Customers)
            //{
            //    Console.WriteLine($"Name: {item.FullName},Count: {item.Count}");
            //} 
            #endregion

            #region Level2
            //7. Top 5 customers by total spending: name, order count, total spent, average order value.

            //var customers = db.Customers.Select(c => new { name = c.FullName, orderCount = c.Orders.Count, total= c.Orders.Sum(o => o.Items.Sum(ot => ot.UnitPrice * ot.Quantity * (1 - ot.Discount))) })
            //                            .Select(c=>new{c.name,c.orderCount,c.total, averageOrdervalue =c.orderCount==0?0:c.total/c.orderCount })
            //                            .OrderByDescending(c=>c.total).Take(5).ToList();
            //foreach (var item in customers)
            //{
            //    Console.WriteLine($"Name: {item.name},order count: {item.orderCount}, total spendings: {item.total} ,Average order value: {item.averageOrdervalue} ");
            //}

            //8.Monthly report for 2025: Year, Month, OrderCount, Revenue, ordered by month.

            //var orderReport = db.Orders.Where(o => o.OrderDate.Year == 2025)
            //                           .GroupBy(o => o.OrderDate.Month)
            //                           .Select(om => new { Year = 2025, Month = om.Key, OrderCount = om.Count(), Revenue = om.Sum(o => o.Items.Sum(oi=>oi.UnitPrice * oi.Quantity * (1 - oi.Discount))) })
            //                           .OrderBy(o => o.Month);
            //foreach (var item in orderReport)
            //{
            //    Console.WriteLine($"Year: {item.Year},Monrh: {item.Month}, Order Count: {item.OrderCount} ");
            //}

            //9.Revenue per category, ordered by revenue descending.

            //var CategoriesRevenue = db.Categories.GroupBy(o => o.Name)
            //                                     .Select(oc => new { category = oc.Key, total = oc.Sum(o => o.Products.Sum(om => om.OrderItems.Sum(ot => ot.UnitPrice * ot.Quantity * (1 - ot.Discount)))) })
            //                                     .OrderByDescending(or=>or.total);
            //foreach (var item in CategoriesRevenue)
            //{
            //    Console.WriteLine($"category: {item.category},revenue: {item.total} ");
            //}

            //10.Count customers who never placed an order, grouped by city, ordered by count descending.

            //var Customers = db.Customers.Where(c => c.Orders.Count == 0)
            //                            .GroupBy(g => g.City)
            //                            .Select(co => new {city= co.Key, count=co.Count() })
            //                            .OrderByDescending(oc=>oc.count);
            //foreach (var item in Customers)
            //{
            //    Console.WriteLine($"City: {item.city},CustomerCount: {item.count} ");
            //}

            //11.Products with at least 10 reviews and an average rating of 4.5 or more: name, category, review count, average rating.

            //var products = db.Products.Where(p => p.Reviews.Count >= 10 && p.Reviews.Average(r=>r.Rating)>=4.5)
            //                          .Select(p=>new {p.Name,p.Category, reviewcount = p.Reviews.Count, averagerating = p.Reviews.Average(r=>r.Rating) })
            //                          .ToList();
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"Name: {item.Name}, Category: {item.Category.Name}, review count: {item.reviewcount},average rating={item.averagerating}");
            //}

            //12.For each category, return the top 2 most expensive products.

            //var ExpensiveProducts= db.Categories.Select(c=>new { Category = c.Name, Products = c.Products.OrderByDescending(p => p.Price).Take(2).Select(p => new {p.Name,p.Price }) }).ToList();

            //foreach (var item in ExpensiveProducts)
            //{
            //    Console.WriteLine($" Category: {item.Category}, Top 2 Products:");
            //    foreach (var p in item.Products)
            //    {
            //    Console.WriteLine($"                                 Name:{p.Name}, Price:{p.Price}");
            //    }
            //}

            //13.Products whose stock is lower than the total quantity in Pending orders. Show name, stock, and pending quantity.

            //var Products = db.Products.Select(p => new { p.Name, p.Stock, Quantity = p.OrderItems.Where(pi => pi.Order.Status == OrderStatus.Pending).Sum(pi => pi.Quantity)})
            //    .Where(pl => pl.Stock < pl.Quantity).ToList();
            //foreach (var item in Products)
            //{
            //    Console.WriteLine($"Name: {item.Name}, Stock: {item.Stock}, Pending Quantity: {item.Quantity}");
            //}

            //14.Customers whose total spending is above the average spending of all customers.

            //var AvgcustomerTotal = db.Customers.Average(c => c.Orders.Sum(o => o.Items.Sum(pi => pi.UnitPrice * pi.Quantity * (1 - pi.Discount))));

            //var ExpenCustomers = db.Customers.Select(c => new { c.FullName, total = c.Orders.Sum(p => p.Items.Sum(pi => pi.UnitPrice * pi.Quantity * (1 - pi.Discount))) })                                             
            //                                 .Where(c=>c.total>AvgcustomerTotal);

            //foreach (var item in ExpenCustomers)
            //{
            //    Console.WriteLine($"Name: {item.FullName}, Total: {item.total}");
            //} 
            #endregion

            #region Level3
            //15.For each category, return its best - selling product by quantity.Categories with no sales must still appear.

            //var bestCategory = db.Categories.Select(c=>new { c.Name,BestProduct=c.Products.Select(p=>new{p.Name,Quantity=p.OrderItems.Sum(oi=>oi.Quantity)}).OrderByDescending(o=>o.Quantity).FirstOrDefault() }).ToList();

            //foreach (var category in bestCategory) 
            //{
            //    Console.WriteLine($"Name:{category.Name} , Product:{category.BestProduct} ");

            //}
            //16.Customers who bought every product in the category "Laptops".
            //int count = db.Products.Where(c => c.Category.Name == "Laptops").Count();

            //var Customers = db.Customers.Where(c => c.Orders.SelectMany(o => o.Items)
            //                                                .Where(oi => oi.Product.Category.Name == "Laptops")
            //                                                .Select(i => i.ProductId).Distinct().Count() == count
            //                                  ).Select(c => new { c.FullName, c.Email }).ToList();
            //foreach (var item in Customers)
            //{
            //    Console.WriteLine($"Name:{item.FullName}, Email: {item.Email}  ");

            //}

            //17.Reviews written by a customer for a product they never bought. 
            //var reviews = db.Reviews.Where(r => !r.Customer.Orders.SelectMany(o => o.Items).Any(oi => oi.ProductId == r.ProductId))
            //                        .Select(r => new {Name=r.Customer.FullName,Product=r.Product.Name,r.Rating,r.Comment})
            //                        .ToList();

            //foreach (var review in reviews) 
            //{ 
            //Console.WriteLine($"Name:{review.Name} ,Product Name:{review.Product}, Rating:{review.Rating}, Comment:{review.Comment}");
            //}
            //18.Return all customers with their order count, including customers with zero orders(left join with GroupJoin).
            //var Customers= db.Customers.Select(o=>new{o.FullName,o.Email,OrderCount=o.Orders.Count}).ToList();
            //foreach (var customer in Customers) 
            //{
            //    Console.WriteLine($"Name:{customer.FullName} , Email:{customer.Email} , OrderCount:{customer.OrderCount} ");
            //}

            //19.The top 5 product pairs that appear together in the same order most often.

            //var TopProductPairs = db.Orders.SelectMany(o => o.Items, (order, item1) => new { order, item1 })
            //                               .SelectMany(x => x.order.Items.Where(item2 => item2.ProductId > x.item1.ProductId), (x, item2) => new { product1 = x.item1.Product.Name, product2 = item2.Product.Name })
            //                               .GroupBy(pair => new { pair.product1, pair.product2 })
            //                               .Select(g => new { pair = $"{g.Key.product1},{g.Key.product2}", TimeBought = g.Count() })
            //                               .OrderByDescending(x => x.TimeBought)
            //                               .Take(5).ToList();

            //foreach (var pair in TopProductPairs) 
            //{
            //    Console.WriteLine($"pair: {pair.pair}");
            //}

            //20.For each customer, return their name and their most recent order with each item's product name and quantity, in one query. 

            //var Customers= db.Customers.Select(c => new { CustomerName = c.FullName, RecentOrder = c.Orders.OrderByDescending(o => o.OrderDate)
            //    .Select(x => new { x.Id, x.OrderDate, Pitems=x.Items.Select(oi => new { Pname = oi.Product.Name, oi.Quantity }) }).FirstOrDefault()
            //}).ToList();
            //foreach (var c in Customers)
            //{
            //    Console.WriteLine($"Name: {c.CustomerName} , Order: ");
            //    if(c.RecentOrder != null && c.RecentOrder.Pitems.Any())
            //     {  
            //        foreach (var o in c.RecentOrder.Pitems)
            //        {
            //            Console.WriteLine($"    - Product: {o.Pname}, Quantity: {o.Quantity}");
            //        } 
            //     }
            //    else
            //    {
            //        Console.WriteLine("  No Valid orders found.");
            //    }
            //}

            //21. For each customer, return their favorite category (the category with the highest total quantity bought). 

            //var Customers = db.Customers.Select(c=>new { c.FullName, FavouriteCategory = c.Orders.SelectMany(o => o.Items).GroupBy(oi => oi.Product.Category.Name).Select(g => new {Category=g.Key,TotalQuntity=g.Sum(oi=>oi.Quantity)}).OrderByDescending(o=>o.TotalQuntity).FirstOrDefault() }).ToList();
            //foreach (var customer in Customers)
            //{
            //    if (customer.FavouriteCategory != null)
            //        Console.WriteLine($"Name:{customer.FullName} , FavouriteCategory:{customer.FavouriteCategory}  ");
            //    else Console.WriteLine($"Name: {customer.FullName} | Favourite Category: No Purchases");
            //}

            //22. Rank products by revenue (1, 2, 3 ...) and return Rank, Product, Revenue.

            //    var RankedProducts = db.Products.Select(p => new {p.Name,Revenue= p.OrderItems.Sum(oi=>oi.Quantity * oi.UnitPrice * (1 - oi.Discount)) })
            //                                    .OrderByDescending(o => o.Revenue)
            //                                    .AsEnumerable()
            //                                    .Select((p,index) => new {Rank=index+1,p.Name,p.Revenue}).ToList();


            //    foreach (var r in RankedProducts)
            //    {
            //        Console.WriteLine($"Rank:{r.Rank} | Name:{r.Name} | Revenue:{r.Revenue}");
            //    } 
            #endregion

            #region Level4
            //23.Write SearchProducts(ProductFilter f).It applies only the filters that were provided(text, category, min / max price, in -stock only)
            //, supports sorting by name, price, or rating, and returns a page of results with the total count.
            //var f1 = new ProductFilter
            //{
            //    MinPrice = 50,
            //    MaxPrice = 3000,
            //    InStockOnly = true,
            //    SortBy = "price",
            //    IsDescending = true,
            //    PageNumber = 1,
            //    PageSize = 3
            //};

            //var result1 = SearchProducts(f1);

            //Console.WriteLine($"TotalCount: {result1.TotalCount}");
            //foreach (var p in result1.Products)
            //{
            //    Console.WriteLine($"{p.Name} | {p.Price} | {p.Stock}");
            //}

            //var f2 = new ProductFilter
            //{
            //    Text = "Pro",
            //    SortBy = "rating",
            //    IsDescending = true,
            //    PageNumber = 1,
            //    PageSize = 5
            //};

            //var result2 = SearchProducts(f2);

            //Console.WriteLine($"TotalCount: {result2.TotalCount}");
            //foreach (var p in result2.Products)
            //{
            //    Console.WriteLine($"{p.Name} | {p.Price}");
            //}


            //24.Increase the price by 10 % for products in "Accessories" with stock below 5, without loading them into memory. 
            //db.Products.Where(p => p.Category.Name == "Accessories" && p.Stock < 5).ExecuteUpdate(p => p.SetProperty(p => p.Price, p => p.Price * 1.10m));

            //25.Delete all cancelled orders older than 1 year without loading them into memory.
            //var cutoffDate = DateTime.Today.AddYears(-1);
            //db.Orders.Where(o => o.OrderDate < cutoffDate && o.Status==OrderStatus.Cancelled).ExecuteDelete(); 
            #endregion


            #region Bonus
            //(a)Print the SQL generated for question 8 using ToQueryString().
            //8.Monthly report for 2025: Year, Month, OrderCount, Revenue, ordered by month.


            //var orderReport = db.Orders.Where(o => o.OrderDate.Year == 2025)
            //                           .GroupBy(o => o.OrderDate.Month)
            //                           .Select(om => new { Year = 2025, Month = om.Key, OrderCount = om.Count(), Revenue = om.Sum(o => o.Items.Sum(oi => oi.UnitPrice * oi.Quantity * (1 - oi.Discount))) })
            //                           .OrderBy(o => o.Month).ToQueryString();
            //Console.WriteLine(orderReport);

            //(b)Explain the N+1 problem and show where it would appear if question 20 were written with a foreach loop.

            //Ans:The N +1 problem happens when an application executes N separate queries inside a loop to fetch Child data for each record resulting in N + 1
            // If we first execute var customers = db.Customers.ToList() then in for each loopwe use var recentOrder = db.Orders
            //.Where(o => o.CustomerId == c.Id)
            //.OrderByDescending(o => o.OrderDate)
            //.FirstOrDefault();
            // so EF Core executes a separate SQL query for every single customer in the database  

            //(c)Why can't Select((x, i) => ...) (with an index) be translated to SQL in question 22, and what is the fix?

            //Ans:Because it is from IQueryable that depend on transformation of expression tree to SQL, and SQL doesn't have the definition of index.
            //to fix it , we need to separate with As Enumerable() to convert it to IEnumerable to Execute SQL and grab data to memory so C# can identify the index easily. 
            #endregion
        }




    }
}
