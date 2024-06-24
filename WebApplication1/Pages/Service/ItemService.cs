public class ItemService
{
    private readonly string _itemsFilePath = "items.txt";

    public ItemService()
    {
        if (!File.Exists(_itemsFilePath))
        {
            File.Create(_itemsFilePath).Close();
        }
    }

    public List<Item> GetItems()
    {
        var items = new List<Item>();
        var lines = File.ReadAllLines(_itemsFilePath);
        foreach (var line in lines)
        {
            var parts = line.Split(';');
            items.Add(new Item
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Price = decimal.Parse(parts[2]),
                Description = parts.Length > 3 ? parts[3] : string.Empty,
                ImagePath = parts.Length > 4 ? parts[4] : string.Empty
            });
        }
        return items;
    }

    public Item GetItemById(int id)
    {
        var items = GetItems();
        return items.FirstOrDefault(i => i.Id == id);
    }

    public void AddItem(Item item)
    {
        var items = GetItems();
        item.Id = items.Count > 0 ? items.Max(i => i.Id) + 1 : 1;
        items.Add(item);
        SaveItems(items);
    }

    public void DeleteItem(int id)
    {
        var items = GetItems();
        var itemToRemove = items.FirstOrDefault(i => i.Id == id);
        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
            SaveItems(items);
        }
    }

    private void SaveItems(List<Item> items)
    {
        var lines = items.Select(item => $"{item.Id};{item.Name};{item.Price};{item.Description};{item.ImagePath}");
        File.WriteAllLines(_itemsFilePath, lines);
    }
}
