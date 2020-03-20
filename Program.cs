using System;
using static System.Console;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using EttPrivatRepoAdministrator.Models;
using Newtonsoft.Json;
//using Newtonsoft.Json;

namespace EttPrivatRepoAdministrator
{
    class Program
    {
        static readonly HttpClient httpClient = new HttpClient();
        static List<Product> productList2 = new List<Product>();
        static Product[] productList = new Product[10];
        static void Main(string[] args)
        {
            //setting headers...
            //Accept: application/json
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            //User-Agent: FreakyFashionAdministrator
            httpClient.DefaultRequestHeaders.Add("User-Agent", "EttPrivatRepoAdministrator");
            httpClient.BaseAddress = new Uri("https://localhost:5001/api/");

            bool ShouldNotExit = true;

            while (ShouldNotExit)
            {
                Clear();
                WriteLine("1. Products");
                WriteLine("2. Categories");
                WriteLine("3. Exit");
                //ConsoleKeyInfo keyPressed = ReadKey(true);
                var KeyPressed = ReadKey(true);
                bool shouldAbort = false;
                bool shouldAbort2 = false;


                switch (KeyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        do
                        {
                            Clear();
                            WriteLine("1. List products");
                            WriteLine("2. Add product");
                            KeyPressed = ReadKey(true);
                            switch (KeyPressed.Key)
                            {
                                case ConsoleKey.D1:
                                case ConsoleKey.NumPad1:
                                    do
                                    {
                                        var response = httpClient.GetAsync("products").Result;
                                        //var responseCate = httpClient.GetAsync("categories").Result;
                                        //.GetAwaiter()
                                        //.GetResult();

                                        var products = Enumerable.Empty<Product>();
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var json = response.Content.ReadAsStringAsync().Result;
                                            //.GetAwaiter()
                                            //.GetResult();
                                            //Deserialize
                                            products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                                        }
                                        Clear();
                                        WriteLine("ID   |   Name");
                                        WriteLine("------------------------------------------------------------------------");
                                        foreach (var product in products)
                                        {
                                            WriteLine($"{product.Id}    |    {product.Name}");
                                        }
                                        WriteLine(" ");
                                        WriteLine("(V)iew (E)dit (D)elete");
                                        bool validKeyPressed;
                                        do
                                        {
                                            KeyPressed = ReadKey(true);
                                            validKeyPressed = KeyPressed.Key == ConsoleKey.V ||
                                                              KeyPressed.Key == ConsoleKey.E ||
                                                              KeyPressed.Key == ConsoleKey.D ||
                                                              KeyPressed.Key == ConsoleKey.Escape;
                                        } while (!validKeyPressed);
                                        //KeyPressed = ReadKey(true);
                                        bool hasInvalidInput = false;
                                        switch (KeyPressed.Key)
                                        {
                                            case ConsoleKey.V:
                                                Clear();
                                                WriteLine("ID   |   Name");
                                                WriteLine("------------------------------------------------------------------------");
                                                foreach (var product in products)
                                                {
                                                    WriteLine($"{product.Id}    |    {product.Name}");
                                                }
                                                WriteLine(" ");
                                                Write("View (ID): ");
                                                string Id = ReadLine();

                                                do
                                                {
                                                    if (Id != "")
                                                    {
                                                        foreach (var product in products)
                                                        {
                                                            if (product.Id == int.Parse(Id))
                                                            {
                                                                Clear();
                                                                Write("ID: ");
                                                                WriteLine(product.Id);
                                                                WriteLine(" ");
                                                                Write("Name: ");
                                                                WriteLine(product.Name);
                                                                WriteLine(" ");
                                                                Write("Description: ");
                                                                WriteLine(product.Description);
                                                                WriteLine(" ");
                                                                Write("Price: ");
                                                                WriteLine(product.Price);
                                                                WriteLine(" ");
                                                                Write("Categories: ");
                                                                var categoryName = "";
                                                                foreach (var category in product.Categories)
                                                                {
                                                                    categoryName += " " + category.Name + ",";
                                                                }
                                                                categoryName = categoryName.TrimEnd(',');
                                                                Write(categoryName);
                                                                WriteLine(" ");

                                                                //Write("Categories: ");
                                                                //product.Categories.ForEach(x => Write($"{x.Name}, "));
                                                                //WriteLine(" ");


                                                            }
                                                        }
                                                    }
                                                    KeyPressed = ReadKey(true);
                                                    switch (KeyPressed.Key)
                                                    {
                                                        case ConsoleKey.Escape:
                                                            hasInvalidInput = true;
                                                            shouldAbort2 = false;
                                                            break;
                                                    }
                                                } while (!hasInvalidInput);
                                                break;
                                            /////////////// Edit
                                            case ConsoleKey.E:
                                                UpdateProduct(products);
                                                break;
                                            /////////////
                                            case ConsoleKey.D:
                                                DeleteProduct(products);
                                                break;
                                            /////////////
                                            case ConsoleKey.Escape:
                                                shouldAbort2 = true;
                                                break;
                                        }
                                    } while (!shouldAbort2);

                                    break;
                                case ConsoleKey.D2:
                                case ConsoleKey.NumPad2:
                                    AddProduct();

                                    break;
                                case ConsoleKey.Escape:
                                    shouldAbort = true;
                                    break;

                            }




                        } while (!shouldAbort);

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        do
                        {
                            Clear();
                            WriteLine("1. List categories");
                            WriteLine("2. Add category");
                            WriteLine("3. Add product to category");

                            KeyPressed = ReadKey(true);
                            switch (KeyPressed.Key)
                            {
                                case ConsoleKey.D1:
                                case ConsoleKey.NumPad1:
                                    do
                                    {
                                        var response = httpClient.GetAsync("categories").Result;
                                        //.GetAwaiter()
                                        //.GetResult();

                                        var categories = Enumerable.Empty<Category>();
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var json = response.Content.ReadAsStringAsync().Result;
                                            //.GetAwaiter()
                                            //.GetResult();
                                            //Deserialize
                                            categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(json);
                                        }
                                        Clear();
                                        WriteLine("ID   |   Name");
                                        WriteLine("------------------------------------------------------------------------");
                                        foreach (var category in categories)
                                        {
                                            WriteLine($"{category.Id}    |    {category.Name}");
                                        }
                                        WriteLine(" ");
                                        WriteLine("(V)iew (E)dit (D)elete");
                                        bool validKeyPressed;
                                        do
                                        {
                                            KeyPressed = ReadKey(true);
                                            validKeyPressed = KeyPressed.Key == ConsoleKey.V ||
                                                              KeyPressed.Key == ConsoleKey.E ||
                                                              KeyPressed.Key == ConsoleKey.D ||
                                                              KeyPressed.Key == ConsoleKey.Escape;
                                        } while (!validKeyPressed);
                                        //KeyPressed = ReadKey(true);
                                        bool hasInvalidInput = false;
                                        switch (KeyPressed.Key)
                                        {
                                            case ConsoleKey.V:
                                                Clear();
                                                WriteLine("ID   |   Name");
                                                WriteLine("------------------------------------------------------------------------");
                                                foreach (var category in categories)
                                                {
                                                    WriteLine($"{category.Id}    |    {category.Name}");
                                                }
                                                WriteLine(" ");
                                                Write("View (ID): ");
                                                string Id = ReadLine();
                                                ///////                                                
                                                //KeyPressed = ReadKey(true);
                                                do
                                                {
                                                    if (Id != "")
                                                    {
                                                        foreach (var category in categories)
                                                        {
                                                            if (category.Id == int.Parse(Id))
                                                            {
                                                                Clear();
                                                                Write("ID: ");
                                                                WriteLine(category.Id);
                                                                WriteLine(" ");
                                                                Write("Name: ");
                                                                WriteLine(category.Name);
                                                                WriteLine(" ");

                                                                Write("Image URL: ");
                                                                WriteLine(category.ImageUrl);
                                                                WriteLine(" ");
                                                            }
                                                        }
                                                    }
                                                    KeyPressed = ReadKey(true);
                                                    switch (KeyPressed.Key)
                                                    {
                                                        case ConsoleKey.Escape:
                                                            hasInvalidInput = true;
                                                            shouldAbort2 = false;
                                                            break;
                                                    }
                                                } while (!hasInvalidInput);
                                                break;
                                            case ConsoleKey.E:
                                                UpdateCategory(categories);
                                                break;
                                            case ConsoleKey.D:
                                                DeleteCategory(categories);

                                                break;
                                            case ConsoleKey.Escape:
                                                shouldAbort2 = true;
                                                break;
                                        }
                                    } while (!shouldAbort2);

                                    break;
                                case ConsoleKey.D2:
                                case ConsoleKey.NumPad2:
                                    AddCategory();
                                    break;
                                case ConsoleKey.D3:
                                case ConsoleKey.NumPad3:
                                    AddProductToCategory();
                                    break;
                                case ConsoleKey.Escape:
                                    shouldAbort = true;
                                    break;
                            }
                        } while (!shouldAbort);

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ShouldNotExit = false;
                        break;
                }

            }
        }

        private static void DeleteCategory(IEnumerable<Category> categories)
        {
            Clear();
            WriteLine("ID   |   Name");
            WriteLine("------------------------------------------------------------------------");
            foreach (var category in categories)
            {
                WriteLine($"{category.Id}    |    {category.Name}");
            }
            WriteLine(" ");
            Write("Delete (ID): ");
            string IdDelete = ReadLine();

            if (IdDelete != "")
            {
                foreach (var category in categories)
                {
                    if (category.Id == int.Parse(IdDelete))
                    {
                        Clear();
                        Write("ID: ");
                        WriteLine(category.Id);
                        WriteLine(" ");
                        Write("Name: ");
                        WriteLine(category.Name);
                        WriteLine(" ");
                        Write("ImageUrl: ");
                        WriteLine(category.ImageUrl);
                        WriteLine(" ");                        
                       
                        ////Yes or No
                        WriteLine("========================================================================");
                        Write("Delete category? (Y)es (N)o");
                        WriteLine(" ");
                        ConsoleKeyInfo keyPressed;
                        bool isValidKey = false;
                        do
                        {
                            keyPressed = ReadKey(true);
                            isValidKey = keyPressed.Key == ConsoleKey.Y ||
                                         keyPressed.Key == ConsoleKey.N ||
                                         keyPressed.Key == ConsoleKey.Escape;

                        } while (!isValidKey);

                        if (keyPressed.Key == ConsoleKey.Y)
                        {
                            var response = httpClient.DeleteAsync($"/api/categories/{IdDelete}").Result;
                            Clear();
                            if (response.IsSuccessStatusCode)
                            {
                                WriteLine("category deleted");
                            }
                            else
                            {
                                WriteLine("Field to delete category");
                            }
                            Thread.Sleep(2000);

                        }
                        else if (keyPressed.Key == ConsoleKey.N || keyPressed.Key == ConsoleKey.Escape)
                        {
                            break;
                        }

                        Clear();
                    }
                }

            }

        }

        private static void UpdateCategory(IEnumerable<Category> categories)
        {
            Clear();
            WriteLine("ID   |   Name");
            WriteLine("------------------------------------------------------------------------");
            foreach (var category1 in categories)
            {
                WriteLine($"{category1.Id}    |    {category1.Name}");
            }
            WriteLine(" ");
            Write("Edit (ID): ");
            string IdEdit = ReadLine();
            Clear();
            var response = httpClient.GetAsync($"/api/categories/{IdEdit}").Result;

            if (!response.IsSuccessStatusCode)// 00-299 status code = all is well
            {
                WriteLine("category not found");
                Thread.Sleep(2000);
                return;
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var category = JsonConvert.DeserializeObject<Category>(json);
            WriteLine($"ID:{category.Id}");
            WriteLine($"Name:{category.Name}");           
            WriteLine($"ImageUrl:{category.ImageUrl}");            
            WriteLine(" ");
            WriteLine("========================================================================");
            WriteLine($"ID:{category.Id}");
            Write("Name: ");
            var name = ReadLine();         
            Write("Image URL: ");
            var imageURL = ReadLine();
            WriteLine("Is this correct ? (Y)es(N)o: ");
            Write(" ");
            ConsoleKeyInfo keyPressed;
            bool isValidKey = false;
            do
            {
                keyPressed = ReadKey(true);
                isValidKey = keyPressed.Key == ConsoleKey.Y ||
                             keyPressed.Key == ConsoleKey.N ||
                             keyPressed.Key == ConsoleKey.Escape;

            } while (!isValidKey);

            if (keyPressed.Key == ConsoleKey.Y)
            {
                var newCategory = new Category(category.Id, name, imageURL);
                var serializeNewCategory = JsonConvert.SerializeObject(newCategory);
                var content = new StringContent(serializeNewCategory, Encoding.UTF8, "application/json");
                response = httpClient.PutAsync($"categories/{category.Id}", content).Result;
                Clear();
                if (response.IsSuccessStatusCode)
                {
                    WriteLine("Category updated");
                }
                else
                {
                    WriteLine("Field to update category");
                }
                Thread.Sleep(2000);
            }
            else if (keyPressed.Key == ConsoleKey.N || keyPressed.Key == ConsoleKey.Escape)
            {
                return;
            }
            Clear();
        }

        private static void UpdateProduct(IEnumerable<Product> products)
        {
            Clear();
            WriteLine("ID   |   Name");
            WriteLine("------------------------------------------------------------------------");
            foreach (var product1 in products)
            {
                WriteLine($"{product1.Id}    |    {product1.Name}");
            }
            WriteLine(" ");
            Write("Edit (ID): ");
            string IdEdit = ReadLine();
            Clear();
            var response = httpClient.GetAsync($"/api/products/{IdEdit}").Result;

            if (!response.IsSuccessStatusCode)// 00-299 status code = all is well
            {
                WriteLine("product not found");
                Thread.Sleep(2000);
                return;
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<Product>(json);
            WriteLine($"ID:{product.Id}");
            WriteLine($"Name:{product.Name}");
            WriteLine($"Description:{product.Description}");
            WriteLine($"Price:{product.Price}");
            WriteLine($"ImageUrl:{product.ImageUrl}");
            Write("Categories: ");
            var categoryName = "";
            foreach (var category in product.Categories)
            {
                categoryName += " " + category.Name + ",";
            }
            categoryName = categoryName.TrimEnd(',');
            Write(categoryName);
            WriteLine(" ");            
            WriteLine("========================================================================");
            WriteLine($"ID:{product.Id}");
            Write("Name: ");
            var name = ReadLine();
            Write("Description: ");
            var description = ReadLine();
            Write("Price: ");
            var price = ReadLine();
            Write("Image URL: ");
            var imageURL = ReadLine();
            WriteLine("Is this correct ? (Y)es(N)o: ");
            Write(" ");
            ConsoleKeyInfo keyPressed;
            bool isValidKey = false;
            do
            {
                keyPressed = ReadKey(true);
                isValidKey = keyPressed.Key == ConsoleKey.Y ||
                             keyPressed.Key == ConsoleKey.N ||
                             keyPressed.Key == ConsoleKey.Escape;

            } while (!isValidKey);

            if (keyPressed.Key == ConsoleKey.Y)
            {
                var newProduct = new Product(product.Id, name, description, int.Parse(price), imageURL);
                var serializeNewProduct = JsonConvert.SerializeObject(newProduct);
                var content = new StringContent(serializeNewProduct, Encoding.UTF8, "application/json");
                response = httpClient.PutAsync($"products/{product.Id}", content).Result;
                Clear();
                if (response.IsSuccessStatusCode)
                {
                    WriteLine("Product updated");
                }
                else
                {
                    WriteLine("Field to update product");
                }
                Thread.Sleep(2000);
            }
            else if (keyPressed.Key == ConsoleKey.N || keyPressed.Key == ConsoleKey.Escape)
            {
                return;
            }
            Clear();    

        }

        private static void DeleteProduct(IEnumerable<Product> products)
        {
            Clear();
            WriteLine("ID   |   Name");
            WriteLine("------------------------------------------------------------------------");
            foreach (var product in products)
            {
                WriteLine($"{product.Id}    |    {product.Name}");
            }
            WriteLine(" ");
            Write("Delete (ID): ");
            string IdDelete = ReadLine();

            if (IdDelete != "")
            {
                foreach (var product in products)
                {
                    if (product.Id == int.Parse(IdDelete))
                    {
                        Clear();
                        Write("ID: ");
                        WriteLine(product.Id);
                        WriteLine(" ");
                        Write("Name: ");
                        WriteLine(product.Name);
                        WriteLine(" ");
                        Write("Description: ");
                        WriteLine(product.Description);
                        WriteLine(" ");
                        Write("Price: ");
                        WriteLine(product.Price);
                        WriteLine(" ");
                        Write("Categories: ");
                        var categoryName = "";
                        foreach (var category in product.Categories)
                        {
                            categoryName += " " + category.Name + ",";
                        }
                        categoryName = categoryName.TrimEnd(',');
                        Write(categoryName);
                        WriteLine(" ");
                        ////Yes or No
                        WriteLine("========================================================================");
                        Write("Delete product? (Y)es (N)o");
                        WriteLine(" ");
                        ConsoleKeyInfo keyPressed;
                        bool isValidKey = false;
                        do
                        {
                            keyPressed = ReadKey(true);
                            isValidKey = keyPressed.Key == ConsoleKey.Y ||
                                keyPressed.Key == ConsoleKey.N ||
                                         keyPressed.Key == ConsoleKey.Escape;

                        } while (!isValidKey);

                        if (keyPressed.Key == ConsoleKey.Y)
                        {
                            var response = httpClient.DeleteAsync($"/api/products/{IdDelete}").Result;
                            Clear();
                            if (response.IsSuccessStatusCode)
                            {
                                WriteLine("product deleted");
                            }
                            else
                            {
                                WriteLine("Field to delete product");
                            }
                            Thread.Sleep(2000);
                           
                        }
                        else if (keyPressed.Key == ConsoleKey.N || keyPressed.Key == ConsoleKey.Escape)
                        {
                            break;
                        }

                        Clear();
                    }
                }
              
            }
        }

        private static void AddProductToCategory()
        {
            Clear();
            Write("Product ID: ");
            var productId = ReadLine();
            WriteLine(" ");
            Write("Category ID: ");
            var categoryId = ReadLine();
            WriteLine(" ");
            Write("Is this correct ? (Y)es(N)o");
            WriteLine(" ");
            ConsoleKeyInfo keyPressed;
            bool isValidKey = false;
            do
            {
                keyPressed = ReadKey(true);
                isValidKey = keyPressed.Key == ConsoleKey.Y ||
                             keyPressed.Key == ConsoleKey.N;

            } while (!isValidKey);

            if (keyPressed.Key == ConsoleKey.Y)
            {
                //var product = context.Product.FirstOrDefault(x => x.Id == productId);
                var categoryProduct = new CategoryProduct(int.Parse(categoryId), int.Parse(productId));
                var serialiedCategoryProduct = JsonConvert.SerializeObject(categoryProduct);
                var content = new StringContent(serialiedCategoryProduct, Encoding.UTF8, "application/json"); //MIME

                var response = httpClient.PostAsync("CategoriesProducts", content).Result;
                Clear();
                if (response.IsSuccessStatusCode)
                {
                    WriteLine("Product To Category added");
                }
                else
                {
                    WriteLine("Faild to add Product To Category");
                }
                Thread.Sleep(2000);
            }
            else if (keyPressed.Key == ConsoleKey.N)
            {
                AddProductToCategory();
            }

            Clear();
        }

        private static void AddCategory()
        {
            Clear();
            Write("Name: ");
            var name = ReadLine();
            WriteLine(" ");
            Write("Image URL: ");
            var imageURL = ReadLine();
            WriteLine(" ");
            Write("Is this correct ? (Y)es(N)o");
            WriteLine(" ");
            ConsoleKeyInfo keyPressed;
            bool isValidKey = false;
            do
            {
                keyPressed = ReadKey(true);
                isValidKey = keyPressed.Key == ConsoleKey.Y ||
                             keyPressed.Key == ConsoleKey.N;

            } while (!isValidKey);

            if (keyPressed.Key == ConsoleKey.Y)
            {
                var category = new Category(name, imageURL);
                var serialiedCategory = JsonConvert.SerializeObject(category);
                var content = new StringContent(serialiedCategory, Encoding.UTF8, "application/json"); //MIME
                var response = httpClient.PostAsync("categories", content).Result;
                Clear();
                if (response.IsSuccessStatusCode)
                {
                    WriteLine("Category added");
                }
                else
                {
                    WriteLine("Faild to add category");
                }
                Thread.Sleep(2000);
            }
            else if (keyPressed.Key == ConsoleKey.N)
            {
                AddCategory();
            }

            Clear();
        }

        private static void AddProduct()
        {
            Clear();
            Write("Name: ");
            var name = ReadLine();
            WriteLine(" ");
            Write("Description: ");
            var description = ReadLine();
            WriteLine(" ");
            Write("Article Number: ");
            var articleNumber = ReadLine();
            WriteLine(" ");
            Write("Price: ");
            var price = ReadLine();
            WriteLine(" ");
            Write("Image URL: ");
            var imageURL = ReadLine();
            WriteLine(" ");
            Write("Is this correct ? (Y)es(N)o");
            WriteLine(" ");
            ConsoleKeyInfo keyPressed;
            bool isValidKey = false;
            do
            {
                keyPressed = ReadKey(true);
                isValidKey = keyPressed.Key == ConsoleKey.Y ||
                             keyPressed.Key == ConsoleKey.N;

            } while (!isValidKey);

            if (keyPressed.Key == ConsoleKey.Y)
            {
                var product = new Product(name, description, int.Parse(price), imageURL);
                var serialiedProduct = JsonConvert.SerializeObject(product);
                var content = new StringContent(serialiedProduct, Encoding.UTF8, "application/json"); //MIME
                var response = httpClient.PostAsync("products", content).Result;
                Clear();
                if (response.IsSuccessStatusCode)
                {
                    WriteLine("Product added");
                }
                else
                {
                    WriteLine("Faild to add product");
                }
                Thread.Sleep(2000);
            }
            else if (keyPressed.Key == ConsoleKey.N)
            {
                AddProduct();
            }

            Clear();

        }
    }
}
