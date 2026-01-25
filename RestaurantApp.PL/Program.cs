using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.BLL.Services;
using RestaurantApp.BLL.Interfaces;
using RestaurantApp.BLL.Mapper;
using RestaurantApp.BLL.Dtos.MenuItemDto;
using RestaurantApp.BLL.Dtos.OrderDto;
using RestaurantApp.DAL.Data;

// ╔════════════════════════════════════════════════════════════╗
// ║        RESTAURANT MANAGEMENT SYSTEM - CONFIGURATION       ║
// ╚════════════════════════════════════════════════════════════╝

var serviceProvider = ConfigureServices();
var menuItemService = serviceProvider.GetRequiredService<IMenuItemService>();
var orderService = serviceProvider.GetRequiredService<IOrderService>();


await RunApplicationAsync(menuItemService, orderService);

// ╔════════════════════════════════════════════════════════════╗
// ║                   SERVICE CONFIGURATION                    ║
// ╚════════════════════════════════════════════════════════════╝

IServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();
    services.AddLogging();
    // Database Configuration
    services.AddDbContext<RestaurantAppDbContext>(options =>
        options.UseSqlServer("Server=.;Database=RestaurantApp;Trusted_Connection=true;"));

    // AutoMapper Configuration
    services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

    // Service Registration
    services.AddScoped<IMenuItemService, MenuItemServices>();
    services.AddScoped<IOrderService, OrderService>();

    return services.BuildServiceProvider();
}

// ╔════════════════════════════════════════════════════════════╗
// ║                   MAIN APPLICATION FLOW                    ║
// ╚════════════════════════════════════════════════════════════╝

async Task RunApplicationAsync(IMenuItemService menuItemService, IOrderService orderService)
{
    Console.Clear();
    DisplayApplicationHeader();

    bool isRunning = true;

    while (isRunning)
    {
        DisplayMainMenu();
        string choice = Console.ReadLine()?.Trim() ?? "";

        try
        {
            switch (choice)
            {
                case "1":
                    await MenuItemMenuAsync(menuItemService);
                    break;
                case "2":
                    await OrderMenuAsync(orderService);
                    break;
                case "3":
                    isRunning = false;
                    DisplayExitMessage();
                    break;
                default:
                    DisplayError("Səhv seçim! Lütfən düzgün bir seçim yapın.");
                    break;
            }
        }
        catch (Exception ex)
        {
            DisplayError($"Xəta baş verdi: {ex.Message}");
        }
    }
}

// ╔════════════════════════════════════════════════════════════╗
// ║              MENU ITEM OPERATIONS MENU                     ║
// ╚════════════════════════════════════════════════════════════╝

async Task MenuItemMenuAsync(IMenuItemService service)
{
    bool backToMain = false;

    while (!backToMain)
    {
        Console.Clear();
        DisplaySectionHeader("MENU İTEM ÖPERASİYONLARI");

        Console.WriteLine("1. Yeni Menu İtem Elavə Et");
        Console.WriteLine("2. Kateqoriyaya Gore Axtar");
        Console.WriteLine("3. Qiymət Araligina Gore Axtar");
        Console.WriteLine("4. Ad/Kateqoriyaya Gore Axtar");
        Console.WriteLine("5. Menu İtemi Redaktə Et");
        Console.WriteLine("6. Menu İtemi Sil");
        Console.WriteLine("7. Geri Qay\n");

        string choice = Console.ReadLine()?.Trim() ?? "";

        try
        {
            switch (choice)
            {
                case "1":
                    await AddMenuItemAsync(service);
                    break;
                case "2":
                    await GetMenuItemsByCategoryAsync(service);
                    break;
                case "3":
                    await GetMenuItemsByPriceAsync(service);
                    break;
                case "4":
                    await SearchMenuItemsAsync(service);
                    break;
                case "5":
                    await EditMenuItemAsync(service);
                    break;
                case "6":
                    await RemoveMenuItemAsync(service);
                    break;
                case "7":
                    backToMain = true;
                    break;
                default:
                    DisplayError("Sehv secim!");
                    break;
            }
        }
        catch (Exception ex)
        {
            DisplayError($"Xeta: {ex.Message}");
        }

        if (!backToMain && choice != "7")
        {
            Console.WriteLine("\nDavam etmek ucun ise basin...");
            Console.ReadKey();
        }
    }
}

async Task AddMenuItemAsync(IMenuItemService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Yeni Menu İtem Elave Et          │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Ad: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Qiymet: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
    {
        DisplayError("Qiymet reqem olmalidir!");
        return;
    }

    Console.Write("Kateqoriya: ");
    string category = Console.ReadLine() ?? "";

    var dto = new MenuItemCreateDto { Name = name, Price = price, Category = category };
    await service.AddMenuItemAsync(dto);

    DisplaySuccess("Menu İtem ugurla elave edildi!");
}

async Task GetMenuItemsByCategoryAsync(IMenuItemService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Kateqoriyaya Gore Axtar          │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Kateqoriya adi: ");
    string category = Console.ReadLine() ?? "";

    var items = await service.GetByCategoryAsync(category);
    DisplayMenuItems(items);
}

async Task GetMenuItemsByPriceAsync(IMenuItemService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Qiymet Araligina Gore Axtar      │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Minimum qiymet: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal min))
    {
        DisplayError("Qiymet reqem olmalidir!");
        return;
    }

    Console.Write("Maksimum qiymet: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal max))
    {
        DisplayError("Qiymet reqem olmalidir!");
        return;
    }

    var items = await service.GetByPriceIntervalAsync(min, max);
    DisplayMenuItems(items);
}

async Task SearchMenuItemsAsync(IMenuItemService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Menu İtemi Axtar                 │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Axtaris metni: ");
    string searchText = Console.ReadLine() ?? "";

    var items = await service.SearchAsync(searchText);
    DisplayMenuItems(items);
}

async Task EditMenuItemAsync(IMenuItemService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Menu İtemi Redakte Et            │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Menu İtem ID: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        DisplayError("ID reqem olmalidir!");
        return;
    }

    Console.Write("Yeni ad: ");
    string newName = Console.ReadLine() ?? "";

    Console.Write("Yeni qiymət: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
    {
        DisplayError("Qiymet reqem olmalidir!");
        return;
    }

    var dto = new MenuItemUpdateDto { Id = id, Name = newName, Price = newPrice };
    await service.EditMenuItemAsync(id, dto);

    DisplaySuccess("Menu İtem uğurla redakte edildi!");
}

async Task RemoveMenuItemAsync(IMenuItemService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Menu İtemi Sil                   │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Menu İtem ID: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        DisplayError("ID reqem olmalidir!");
        return;
    }

    await service.RemoveMenuItemAsync(id);
    DisplaySuccess("Menu İtem ugurla silindi!");
}

// ╔════════════════════════════════════════════════════════════╗
// ║               ORDER OPERATIONS MENU                        ║
// ╚════════════════════════════════════════════════════════════╝

async Task OrderMenuAsync(IOrderService service)
{
    bool backToMain = false;

    while (!backToMain)
    {
        Console.Clear();
        DisplaySectionHeader("SİFARİŞ ÖPERASİYONLARI");

        Console.WriteLine("1. Yeni Sifaris Əlavə Et");
        Console.WriteLine("2. Bütün Sifarişləri Göstər");
        Console.WriteLine("3. Tarixə Görə Axtar");
        Console.WriteLine("4. Tarix Aralığına Görə Axtar");
        Console.WriteLine("5. Qiymət Aralığına Görə Axtar");
        Console.WriteLine("6. Sifariş Nömrəsinə Görə Axtar");
        Console.WriteLine("7. Sifariş Sil");
        Console.WriteLine("8. Geri Qay\n");

        string choice = Console.ReadLine()?.Trim() ?? "";

        try
        {
            switch (choice)
            {
                case "1":
                    await AddOrderAsync(service);
                    break;
                case "2":
                    await GetAllOrdersAsync(service);
                    break;
                case "3":
                    await GetOrderByDateAsync(service);
                    break;
                case "4":
                    await GetOrdersByDateIntervalAsync(service);
                    break;
                case "5":
                    await GetOrdersByPriceAsync(service);
                    break;
                case "6":
                    await GetOrderByNumberAsync(service);
                    break;
                case "7":
                    await RemoveOrderAsync(service);
                    break;
                case "8":
                    backToMain = true;
                    break;
                default:
                    DisplayError("Səhv seçim!");
                    break;
            }
        }
        catch (Exception ex)
        {
            DisplayError($"Xəta: {ex.Message}");
        }

        if (!backToMain && choice != "8")
        {
            Console.WriteLine("\nDevam etmək üçün isə basın...");
            Console.ReadKey();
        }
    }
}

async Task AddOrderAsync(IOrderService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Yeni Sifariş Əlavə Et            │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    var orderItems = new List<OrderItemCreateDto>();
    bool addingItems = true;

    while (addingItems)
    {
        Console.Write("Menu İtem ID: ");
        if (!int.TryParse(Console.ReadLine(), out int menuItemId))
        {
            DisplayError("ID rəqəm olmalıdır!");
            continue;
        }

        Console.Write("Miqdar: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            DisplayError("Miqdar rəqəm olmalıdır!");
            continue;
        }

        orderItems.Add(new OrderItemCreateDto { MenuItemId = menuItemId, Quantity = quantity });

        Console.Write("\nDahil bir məhsul əlavə etmək istəyirsiniz? (y/n): ");
        addingItems = Console.ReadLine()?.ToLower() == "y";
        Console.WriteLine();
    }

    var dto = new OrderCreateDto { OrderItems = orderItems };
    await service.AddOrderAsync(dto);

    DisplaySuccess("Sifariş uğurla əlavə edildi!");
}

async Task GetAllOrdersAsync(IOrderService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Bütün Sifarişlər                 │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    var orders = await service.GetAllOrdersAsync();
    DisplayOrders(orders);
}

async Task GetOrderByDateAsync(IOrderService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Tarixə Görə Axtar                │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Tarix (yyyy-MM-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
    {
        DisplayError("Tarix formatı yanlışdır!");
        return;
    }

    var orders = await service.GetOrderByDateAsync(date);
    DisplayOrders(orders);
}

async Task GetOrdersByDateIntervalAsync(IOrderService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Tarix Aralığına Görə Axtar       │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Başlanğıc tarix (yyyy-MM-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime start))
    {
        DisplayError("Tarix formatı yanlışdır!");
        return;
    }

    Console.Write("Bitmə tarix (yyyy-MM-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime end))
    {
        DisplayError("Tarix formatı yanlışdır!");
        return;
    }

    var orders = await service.GetOrdersByDatesIntervalAsync(start, end);
    DisplayOrders(orders);
}

async Task GetOrdersByPriceAsync(IOrderService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Qiymət Aralığında Görə Axtar      │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Minimum qiymət: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal min))
    {
        DisplayError("Qiymət rəqəm olmalıdır!");
        return;
    }

    Console.Write("Maksimum qiymət: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal max))
    {
        DisplayError("Qiymət rəqəm olmalıdır!");
        return;
    }

    var orders = await service.GetOrdersByPriceIntervalAsync(min, max);
    DisplayOrders(orders);
}

async Task GetOrderByNumberAsync(IOrderService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Sifariş Nömrəsinə Görə Axtar     │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Sifariş Nömrəsi: ");
    if (!int.TryParse(Console.ReadLine(), out int orderNo))
    {
        DisplayError("Nömrə rəqəm olmalıdır!");
        return;
    }

    var order = await service.GetOrderByNoAsync(orderNo);
    DisplayOrder(order);
}

async Task RemoveOrderAsync(IOrderService service)
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│   Sifariş Sil                      │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.Write("Sifariş Nömrəsi: ");
    if (!int.TryParse(Console.ReadLine(), out int orderNo))
    {
        DisplayError("Nömrə rəqəm olmalıdır!");
        return;
    }

    await service.RemoveOrderAsync(orderNo);
    DisplaySuccess("Sifariş uğurla silindi!");
}

// ╔════════════════════════════════════════════════════════════╗
// ║                    DISPLAY UTILITIES                       ║
// ╚════════════════════════════════════════════════════════════╝

void DisplayApplicationHeader()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                                                            ║");
    Console.WriteLine("║          RESTORAN YÖNƏTİM SİSTEMİ (RMS)                   ║");
    Console.WriteLine("║                                                            ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");
    Console.ResetColor();
}

void DisplayMainMenu()
{
    Console.WriteLine("\n┌─────────────────────────────────────┐");
    Console.WriteLine("│         ANA MENYU                   │");
    Console.WriteLine("└─────────────────────────────────────┘\n");

    Console.WriteLine("1. Menu İtem Əməliyyatları");
    Console.WriteLine("2. Sifariş Əməliyyatları");
    Console.WriteLine("3. Çıxış\n");

    Console.Write("Seçim: ");
}

void DisplaySectionHeader(string title)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
    Console.WriteLine($"║  {title.PadRight(57)}  ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");
    Console.ResetColor();
}

void DisplayMenuItems(List<MenuItemReturnDto> items)
{
    if (items.Count == 0)
    {
        DisplayWarning("Nəticə tapılmadı.");
        return;
    }

    Console.WriteLine("\n┌──────┬─────────────────────┬──────────┬──────────────────┐");
    Console.WriteLine("│  ID  │ Ad                  │ Qiymət   │ Kateqoriya       │");
    Console.WriteLine("├──────┼─────────────────────┼──────────┼──────────────────┤");

    foreach (var item in items)
    {
        Console.WriteLine($"│ {item.Id,4} │ {item.Name,-19} │ {item.Price,8:F2} │ {item.Category,-16} │");
    }

    Console.WriteLine("└──────┴─────────────────────┴──────────┴──────────────────┘");
}

void DisplayOrders(List<OrderReturnDto> orders)
{
    if (orders.Count == 0)
    {
        DisplayWarning("Nəticə tapılmadı.");
        return;
    }

    Console.WriteLine("\n┌──────┬────────────────────────────┬──────────────────┐");
    Console.WriteLine("│  ID  │ Tarix                      │ Cəmi Məbləğ (₼)  │");
    Console.WriteLine("├──────┼────────────────────────────┼──────────────────┤");

    foreach (var order in orders)
    {
        Console.WriteLine($"│ {order.Id,4} │ {order.Date:yyyy-MM-dd HH:mm:ss} │ {order.TotalAmount,16:F2} │");
    }

    Console.WriteLine("└──────┴────────────────────────────────┴──────────────────┘");
}

void DisplayOrder(OrderReturnDto order)
{
    Console.WriteLine($"\n┌──────────────────────────────────────┐");
    Console.WriteLine($"│ Sifariş №{order.Id,-3}{"",-19}         │");
    Console.WriteLine($"├──────────────────────────────────────┤");
    Console.WriteLine($"│ Tarix: {order.Date:yyyy-MM-dd HH:mm:ss,-27} │");
    Console.WriteLine($"│ Cəmi Məbləğ: ₼{order.TotalAmount,23:F2} │");
    Console.WriteLine("├──────────────────────────────────────┤");
    Console.WriteLine("│ SİFARİŞ ÖĞƏLƏRİ:                       │");
    Console.WriteLine("└──────────────────────────────────────┘");

    Console.WriteLine("\n┌──────┬──────────────────┬──────────┬──────────────────┐");
    Console.WriteLine("│  ID  │ Məhsul           │ Miqdar   │ Cəmi (₼)         │");
    Console.WriteLine("├──────┼──────────────────┼──────────┼──────────────────┤");

    foreach (var item in order.OrderItems)
    {
        Console.WriteLine($"│ {item.MenuItemId,4} │ {item.MenuItemName,-16} │ {item.Quantity,8} │ {item.Subtotal,16:F2} │");
    }

    Console.WriteLine("└──────┴──────────────────┴──────────┴──────────────────┘");
}

void DisplaySuccess(string message)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n✓ {message}");
    Console.ResetColor();
}

void DisplayError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n✗ {message}");
    Console.ResetColor();
}

void DisplayWarning(string message)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n⚠ {message}");
    Console.ResetColor();
}

void DisplayExitMessage()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n✓ Proqramdan çıxış edildi. Sağolun!");
    Console.ResetColor();
}
