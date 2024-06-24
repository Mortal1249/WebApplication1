public class CartService
{
    private readonly string _cartFilePath = "cart.txt";

    public CartService()
    {
        if (!File.Exists(_cartFilePath))
        {
            File.Create(_cartFilePath).Close();
        }
    }

    public List<CartItem> GetCartItems()
    {
        var items = new List<CartItem>();
        var lines = File.ReadAllLines(_cartFilePath);

        foreach (var line in lines)
        {
            var parts = line.Split('|'); // Используем символ | в качестве разделителя

            if (parts.Length >= 6)
            {
                int id;
                if (!int.TryParse(parts[0], out id))
                {
                    Console.WriteLine($"Ошибка парсинга Id: {parts[0]}");
                    continue;
                }

                decimal price;
                if (!decimal.TryParse(parts[3], out price))
                {
                    Console.WriteLine($"Ошибка парсинга Price: {parts[3]}");
                    continue;
                }

                int quantity;
                if (!int.TryParse(parts[5], out quantity))
                {
                    Console.WriteLine($"Ошибка парсинга Quantity: {parts[5]}");
                    continue;
                }

                items.Add(new CartItem
                {
                    Id = id,
                    Name = parts[1],
                    Description = parts[2],
                    Price = price,
                    ImagePath = parts.Length > 4 ? parts[4] : null,
                    Quanity = quantity
                });
            }
            else
            {
                Console.WriteLine($"Некорректная строка в файле cart.txt: {line}");
            }
        }

        return items;
    }

    public void AddItemToCart(Item item)
    {
        var items = GetCartItems();
        var existingItem = items.FirstOrDefault(ci => ci.Id == item.Id);

        if (existingItem != null)
        {
            existingItem.Quanity++;
        }
        else
        {
            items.Add(new CartItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImagePath = item.ImagePath,
                Quanity = 1
            });
        }

        SaveCartItems(items);
    }

    public void RemoveFromCart(int id)
    {
        var items = GetCartItems();
        var itemToRemove = items.FirstOrDefault(i => i.Id == id);

        if (itemToRemove != null)
        {
            if (itemToRemove.Quanity > 1)
            {
                itemToRemove.Quanity--;
            }
            else
            {
                items.Remove(itemToRemove);
            }

            SaveCartItems(items);
        }
    }

    public void ClearCart()
    {
        File.WriteAllLines(_cartFilePath, new string[] { });
    }

    private void SaveCartItems(List<CartItem> items)
    {
        var lines = items.Select(item => $"{item.Id}|{item.Name}|{item.Description}|{item.Price}|{item.ImagePath ?? ""}|{item.Quanity}");
        File.WriteAllLines(_cartFilePath, lines);
    }
}