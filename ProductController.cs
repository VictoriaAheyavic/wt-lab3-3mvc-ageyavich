using System;
using System.Collections.Generic;
using System.Text;
using wt_lab3_3mvc_ageyavich.Models;
using wt_lab3_3mvc_ageyavich.Views;

namespace wt_lab3_3mvc_ageyavich.Controllers
{
    public class ProductController
    {
        private readonly List<Product> products;
        private readonly ProductView view;
        private int _nextId = 4;

        public ProductController()
        {
            // Начальные данные
            products = new List<Product>
        {
            new Product
            {
                Id = 1, Name = "Ноутбук", Category = "Компьютеры",
                Price = 2500, Stock = 5, IsAvailable = true,
                Description = "Мощный ноутбук для работы"
            },
            new Product
            {
                Id = 2, Name = "Мышь", Category = "Аксессуары",
                Price = 80, Stock = 30, IsAvailable = true,
                Description = "Беспроводная мышь"
            },
            new Product
            {
                Id = 3, Name = "Клавиатура", Category = "Аксессуары",
                Price = 150, Stock = 0, IsAvailable = false,
                Description = "Механическая клавиатура"
            }
        };

            view = new ProductView();
        }

        // ===================================================
        // ДЕЙСТВИЯ
        // ===================================================

        /// <summary>
        /// Показать все товары.
        /// </summary>
        public void Index()
        {
            view.ShowHeader("Все товары");
            view.ShowProducts(products);
        }

        /// <summary>
        /// Показать товар по Id.
        /// </summary>
        public void Show(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                view.ShowMessage("Товар не найден.");
                return;
            }

            view.ShowProduct(product);
        }

        /// <summary>
        /// Добавить товар.
        /// </summary>
        public void Add(Product product)
        {
            product.Id = _nextId++;
            products.Add(product);
            view.ShowMessage($"Товар «{product.Name}» успешно добавлен (Id={product.Id}).");
        }

        /// <summary>
        /// Удалить товар по Id.
        /// </summary>
        public void Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                view.ShowMessage("Товар не найден.");
                return;
            }

            products.Remove(product);
            view.ShowMessage($"Товар «{product.Name}» успешно удалён.");
        }

        /// <summary>
        /// ДОПОЛНИТЕЛЬНАЯ ОПЕРАЦИЯ: поиск по части названия.
        /// </summary>
        public void SearchByName(string part)
        {
            var found = products
                .Where(p => p.Name.Contains(part, StringComparison.OrdinalIgnoreCase))
                .ToList();

            view.ShowHeader($"Поиск по названию «{part}»");
            view.ShowProducts(found);
        }

        /// <summary>
        /// ДОПОЛНИТЕЛЬНАЯ ОПЕРАЦИЯ: фильтр по категории.
        /// </summary>
        public void ShowByCategory(string category)
        {
            var found = products
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            view.ShowHeader($"Категория «{category}»");
            view.ShowProducts(found);
        }

        /// <summary>
        /// ДОПОЛНИТЕЛЬНАЯ ОПЕРАЦИЯ: статистика.
        /// </summary>
        public void ShowStatistics()
        {
            if (products.Count == 0)
            {
                view.ShowMessage("Нет товаров для подсчёта.");
                return;
            }

            int total = products.Count;
            int available = products.Count(p => p.IsAvailable);
            decimal totalSum = products.Sum(p => p.Price * p.Stock);
            decimal avg = products.Average(p => p.Price);
            decimal max = products.Max(p => p.Price);
            decimal min = products.Min(p => p.Price);

            view.ShowStatistics(total, available, totalSum, avg, max, min);
        }

        /// <summary>
        /// ДОПОЛНИТЕЛЬНАЯ ОПЕРАЦИЯ: только доступные товары.
        /// </summary>
        public void ShowAvailable()
        {
            var available = products.Where(p => p.IsAvailable).ToList();
            view.ShowHeader("Только доступные товары");
            view.ShowProducts(available);
        }

        // ===================================================
        // МЕНЮ И ОБРАБОТКА ПОЛЬЗОВАТЕЛЬСКОГО ВВОДА
        // ===================================================

        /// <summary>
        /// Главный цикл меню.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== МЕНЮ ===");
                Console.WriteLine("1. Показать все товары");
                Console.WriteLine("2. Найти товар по ID");
                Console.WriteLine("3. Добавить товар");
                Console.WriteLine("4. Удалить товар");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("6. Товары по категории");
                Console.WriteLine("7. Только доступные товары");
                Console.WriteLine("8. Статистика по товарам");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                var command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        Index();
                        break;

                    case "2":
                        ShowProductByUserInput();
                        break;

                    case "3":
                        AddProductByUserInput();
                        break;

                    case "4":
                        DeleteProductByUserInput();
                        break;

                    case "5":
                        SearchByNameByUserInput();
                        break;

                    case "6":
                        ShowByCategoryByUserInput();
                        break;

                    case "7":
                        ShowAvailable();
                        break;

                    case "8":
                        ShowStatistics();
                        break;

                    case "0":
                        Console.WriteLine("До свидания!");
                        return;

                    default:
                        view.ShowMessage("Неизвестная команда.");
                        break;
                }
            }
        }

        // ---------------------------------------------------

        private void ShowProductByUserInput()
        {
            Console.Write("Введите ID товара: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }

            Show(id);
        }

        private void AddProductByUserInput()
        {
            Console.WriteLine("--- Добавление товара ---");

            Console.Write("Название: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Категория: ");
            string category = Console.ReadLine() ?? "";

            Console.Write("Цена: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                view.ShowMessage("Некорректная цена.");
                return;
            }

            Console.Write("Остаток на складе: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                view.ShowMessage("Некорректный остаток.");
                return;
            }

            Console.Write("Описание: ");
            string description = Console.ReadLine() ?? "";

            var product = new Product
            {
                Name = name,
                Category = category,
                Price = price,
                Stock = stock,
                IsAvailable = stock > 0,
                Description = description
            };

            Add(product);
        }

        private void DeleteProductByUserInput()
        {
            Console.Write("Введите ID товара для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }

            Delete(id);
        }

        private void SearchByNameByUserInput()
        {
            Console.Write("Введите часть названия: ");
            string part = Console.ReadLine() ?? "";
            SearchByName(part);
        }

        private void ShowByCategoryByUserInput()
        {
            Console.Write("Введите категорию: ");
            string category = Console.ReadLine() ?? "";
            ShowByCategory(category);
        }
    }
}
