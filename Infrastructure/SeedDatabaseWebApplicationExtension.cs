namespace Groceries.Infrastructure;

public static class SeedDatabaseWebApplicationExtension
{
    public static void SeedDatabase(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        IRepository<Category> categoryRepository = scope.ServiceProvider.GetRequiredService<IRepository<Category>>();
        IRepository<Product> productRepository = scope.ServiceProvider.GetRequiredService<IRepository<Product>>();

        if (!categoryRepository.Entities.Any())
        {
            foreach (Category category in _categorySeeds)
            {
                categoryRepository.Add(category);
            }
        }

        if (!productRepository.Entities.Any())
        {
            foreach (Product product in _productSeeds)
            {
                productRepository.Add(product);
            }
        }
    }

    private static readonly Category[] _categorySeeds = new Category[]
    {
        new() { Name = "Фрукти та овочі" },
        new() { Name = "Напої" },
        new() { Name = "Горіхи та сухофрукти" }
    };

    private static readonly Product[] _productSeeds = new Product[]
    {
        new()
        {
            Name = "Томат",
            Category = _categorySeeds[0],
            Description = "Обожнюємо його за кетчуп, що рятує будь-яку неприємну страву.",
            Price = 36.12m
        },
        new()
        {
            Name = "Огірок",
            Category = _categorySeeds[0],
            Description = "З гіркою попкою, гостими колючками та терпкою шкіркою. Саме такі ми не продаємо.",
            Price = 51.86m
        },
        new()
        {
            Name = "Яблуко",
            Category = _categorySeeds[0],
            Description = "Впало далеко від яблуні, прямісінько в наші холодильники.",
            Price = 21.83m
        },
        new()
        {
            Name = "Пекінская капуста",
            Category = _categorySeeds[0],
            Description = "Приїхала до нас з Пекіну!",
            Price = 43.23m
        },
        new()
        {
            Name = "Банан",
            Category = _categorySeeds[0],
            Description = "Чи ви знали, що у банані міститься радіоактивний ізотоп калію?",
            Price = 24.15m
        },
        new()
        {
            Name = "Полуниця",
            Category = _categorySeeds[0],
            Description = "Купувати в три дорога не в сезон - самий смак.",
            Price = 169.98m
        },
        new()
        {
            Name = "Кола",
            Category = _categorySeeds[1],
            Description = "Чого більше не пізнають вороги на сході.",
            Price = 18.99m
        },
        new()
        {
            Name = "Фанта",
            Category = _categorySeeds[1],
            Description = "На смак як жумальна гумка зі смаком апельсину. Чи навпаки?",
            Price = 19.95m
        },
        new()
        {
            Name = "Спрайт",
            Category = _categorySeeds[1],
            Description = "Добре підходить щоб змити жир з мікрохвильовки.",
            Price = 17.49m
        },
        new()
        {
            Name = "Енергетик",
            Category = _categorySeeds[1],
            Description = "Дуже популярний в ніч до захисту диплома. П'ють разом з кавою.",
            Price = 32.89m
        },
        new()
        {
            Name = "Яблучний сік",
            Category = _categorySeeds[1],
            Description = "Той ще імпостор. Візміть будь-який сік іншого фрукту, ви знайдете там велику долю яблучного сока.",
            Price = 25.45m
        },
        new()
        {
            Name = "Мінералка",
            Category = _categorySeeds[1],
            Description = "Трошки солона, дуже газована",
            Price = 16.78m
        },
        new()
        {
            Name = "Банановий нектар",
            Category = _categorySeeds[1],
            Description = "Цікаво, чи є тут яблучний сік?",
            Price = 31.12m
        },
        new()
        {
            Name = "Горіх макадами",
            Category = _categorySeeds[2],
            Description = "Деякі вважають, що макадами - це якась країна.",
            Price = 87.85m
        },
        new()
        {
            Name = "Родзинки",
            Category = _categorySeeds[2],
            Description = "З родзинками дуже смачні булочки виходять",
            Price = 45.85m
        },
        new()
        {
            Name = "Кольорові цукати з ананасу",
            Category = _categorySeeds[2],
            Description = "Випічка стає кольоровою, як веселка",
            Price = 56.63m
        },
        new()
        {
            Name = "Фінік",
            Category = _categorySeeds[2],
            Description = "У виконанні Сема Клафліна",
            Price = 98.86m
        },
        new()
        {
            Name = "Бананові чіпси",
            Category = _categorySeeds[2],
            Description = "Лише перші дві скоринки цікаві та маєть вкус",
            Price = 32.17m
        }
    };
}