// See https://aka.ms/new-console-template for more information

Item blacktea = new Item(1, "Black Tea Earl Grey", 100, 10, true, ItemCategories.Tea);
Item greentea = new Item(2, "Green Tea Jasmine", 120, 10, true, ItemCategories.Tea);
Item mooncake = new Item(3, "Moon Cake", 230, 30, true, ItemCategories.Sweets);
Item teapot = new Item(4, "Teapot Chinese Dragon", 890, 5, true, ItemCategories.Souvenirs);
Item cup = new Item(5, "Cup Mountains", 350, 20, true, ItemCategories.Souvenirs);


List<Item> items = [blacktea, greentea, mooncake, teapot, cup];

bool condition = true;

while (condition)
{
    int var = 0;
    printMenu();

    bool result = false;
    while (!result)
    {
        Console.WriteLine("Type the number, please.");
        result = int.TryParse(Console.ReadLine(), out var);
    }

    switch (var)
    {
        case 0: condition = false; break;
        case 1: printAllItems(); break;
        case 2: addANewItem(); break;
        case 3: deleteTheItem(); break;
        case 4: orderAnItem(); break;
        case 5: sellTheItem(); break;
        case 6: findTheItemMenu(); break;
        default: Console.WriteLine("Something went wrong. Try again."); break;

    }


}

void printAllItems()
{
    for (int i = 0; i < items.Count; i++) items[i].printInfo();
}

void addANewItem()
{

    int iID;
    string iName;
    double iPrice;
    int iQuantity;
    bool iPresenceInStock;
    ItemCategories iCategory;

    bool checking = true;

    while (checking)
    {
        Console.WriteLine("Add a new item:");
        Console.WriteLine("Type the name: ");
        iName = Console.ReadLine();

        if (string.IsNullOrEmpty(iName))
        {
            Console.WriteLine("The name is empty. Try again.");
            break;
        }

        Console.WriteLine("Type the price (with come (,)): ");
        bool result = double.TryParse(Console.ReadLine(), out iPrice);

        if (iPrice <= 0 || !result)
        {
            Console.WriteLine("The price is incorrect. Try again.");
            break;
        }

        Console.WriteLine("Type the quantity: ");
        bool result2 = int.TryParse(Console.ReadLine(), out iQuantity);

        if (iQuantity < 0 || !result2)
        {
            Console.WriteLine("The quantity is incorrect. Try again.");
            break;
        }

        if (iQuantity > 0)
        {
            iPresenceInStock = true;
        }
        else
        {
            iPresenceInStock = false;
        }

        bool checkCategory = true;

        do
        {

            Console.WriteLine("Type the category");
            Console.WriteLine("1. Tea");
            Console.WriteLine("2. Sweets");
            Console.WriteLine("3. Souvenirs");

            int chooseCategory = 0;
            iCategory = ItemCategories.Tea; // po umolchaniu

            bool result3 = int.TryParse(Console.ReadLine(), out chooseCategory);

            if (result3 == false)
            {
                Console.WriteLine("Something went wrong. Try again");
                checkCategory = false;
                break;
            }

            switch (chooseCategory)
            {
                case 1: iCategory = ItemCategories.Tea; break;
                case 2: iCategory = ItemCategories.Sweets; break;
                case 3: iCategory = ItemCategories.Souvenirs; break;
                default: Console.WriteLine("Something went wrong. Try again"); checkCategory = false; break;
            }

            checking = false;

        } while (!checkCategory);


        iID = items[items.Count - 1].ID + 1;

        Item addNewOneItem = new Item(iID, iName, iPrice, iQuantity, iPresenceInStock, iCategory);

        addNewOneItem.printInfo();
        items.Add(addNewOneItem);

        Console.WriteLine("Item is added");

    }


}

void deleteTheItem()
{
    printAllItems();

    Console.WriteLine("\nWhat would you like to delete? Write down the ID of the item");
    int IDwantedDelete = 0;
    bool delete = false;
    bool result = int.TryParse(Console.ReadLine(), out IDwantedDelete);
    if (result)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == IDwantedDelete)
            {
                items.RemoveAt(i);
                Console.WriteLine("The item is deleted");
                delete = true;
            }
        }

        if (!delete)
        {
            Console.WriteLine("There isn't such ID. Try again.");
        }
        ;


    }
    else
    {
        Console.WriteLine("ID is incorrect. Try again.");
    }

}

void orderAnItem()
{

    printAllItems();

    Console.WriteLine("\nWhat would you like to order in stock? Write down the ID of the item");
    int IDwantedOrder = 0;
    //bool ordered = false;
    bool result = int.TryParse(Console.ReadLine(), out IDwantedOrder);
    if (result)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == IDwantedOrder)
            {

                Console.WriteLine("\nHow many items would you like to add?");
                int amount = 0;

                bool resultNum = int.TryParse(Console.ReadLine(), out amount);

                if (resultNum && amount > 0)
                {
                    items[i].Quantity += amount;
                    Console.WriteLine("The item is ordered");
                    //ordered = true;

                }
                else
                {
                    Console.WriteLine("The amount of items is incorrect. Try again");
                    break;
                }
            }
        }

    }
    else
    {
        Console.WriteLine("ID is incorrect. Try again.");
    }


}

void sellTheItem()
{
    printAllItems();

    Console.WriteLine("\nWhat would you like to sell? Write down the ID of the item");
    int IDwantedSell = 0;
    //bool sold = false;
    bool result = int.TryParse(Console.ReadLine(), out IDwantedSell);
    if (result)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == IDwantedSell)
            {

                Console.WriteLine("\nHow many items would you like to sell?");
                int amount = 0;

                bool resultNum = int.TryParse(Console.ReadLine(), out amount);

                if (resultNum && amount > 0 && items[i].Quantity >= amount)
                {
                    items[i].Quantity -= amount;
                    Console.WriteLine("The item is sold");
                    if (items[i].Quantity == 0)
                    {
                        items[i].PresenceInStock = false;
                    }
                    //sold = true;

                }
                else if (!resultNum || amount <= 0)
                {
                    Console.WriteLine("The amount of items is incorrect. Try again");
                    break;
                }
                else if (items[i].Quantity < amount)
                {
                    Console.WriteLine("The amount of items in stock is less than we can sell. Try again");
                }
            }
        }

    }
    else
    {
        Console.WriteLine("ID is incorrect. Try again.");
    }


}

void findTheItemMenu()
{
    Console.WriteLine("\nHow would you like to find the item? Choose 1, 2, 3");
    Console.WriteLine("1. ID");
    Console.WriteLine("2. Name");
    Console.WriteLine("3. Category");

    int v = 0;
    bool result = int.TryParse(Console.ReadLine(), out v);

    if (result && (v == 1 || v == 2 || v == 3))
    {

        switch (v)
        {
            case 1: findID(); break;
            case 2: findName(); break;
            case 3: findCategory(); break;
            default: Console.WriteLine("Something went wrong"); break;
        }

    }
    else
    {
        Console.WriteLine("Something went wrong. Try again");
    }

}

void findID()
{
    Console.WriteLine("Write down the ID of the item");
    int IDwantedFind = 0;
    bool found = false;
    bool result = int.TryParse(Console.ReadLine(), out IDwantedFind);
    if (result)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == IDwantedFind)
            {
                Console.WriteLine("The item is found");
                items[i].printInfo();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("There isn't such ID. Try again.");
        }
        ;


    }
    else
    {
        Console.WriteLine("ID is incorrect. Try again.");
    }
}

void findName()
{
    Console.WriteLine("Write down the name of the item");
    bool found = false;
    string nameFind = Console.ReadLine();

    if (nameFind != null)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Name == nameFind)
            {
                Console.WriteLine("The item is found");
                items[i].printInfo();
                found = true;

            }
        }

    }
    else
    {
        Console.WriteLine("The name shouldn't be empty. Try again");
    }



    if (!found)
    {
        Console.WriteLine("There isn't such name if item. Try again");
    }


}

void findCategory()
{

    Console.WriteLine("Type the category");
    Console.WriteLine("1. Tea");
    Console.WriteLine("2. Sweets");
    Console.WriteLine("3. Souvenirs");

    int chooseCategory = 0;
    bool result = int.TryParse(Console.ReadLine(), out chooseCategory);
    ItemCategories iCategory = 0;


    if (result)
    {

        switch (chooseCategory)
        {
            case 1: iCategory = ItemCategories.Tea; break;
            case 2: iCategory = ItemCategories.Sweets; break;
            case 3: iCategory = ItemCategories.Souvenirs; break;
            default: Console.WriteLine("Something went wrong. Try again"); break;
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Category == iCategory)
            {
                items[i].printInfo();
            }
        }

    }
    else { Console.WriteLine("Something went wrong. Try again"); }

}


void printMenu()
{
    Console.WriteLine("\nWhat would you like to do?");
    Console.WriteLine("1. Print all items.");
    Console.WriteLine("2. Add a new item.");
    Console.WriteLine("3. Delete the item.");
    Console.WriteLine("4. Order an item.");
    Console.WriteLine("5. Sell the item."); // what's that?
    Console.WriteLine("6. Find the item (ID, Name, Category).");
    Console.WriteLine("0. Exit.\n");
}

enum ItemCategories
{
    Tea,
    Sweets,
    Souvenirs

}


class Item
{
    public int ID;
    public string Name;
    public double Price;
    public int Quantity;
    public bool PresenceInStock;
    public ItemCategories Category;


    public void printInfo()
    {
        Console.WriteLine("ID: {0, 3} Name: {1, 25} Price: {2, 6} Quantity: {3, 5} InStock: {4, 5} Category: {5, 9}", ID.ToString(), Name, Price.ToString(), Quantity.ToString(), PresenceInStock.ToString(), Category.ToString());

    }

    public Item(int ID, string name, double price, int quantity, bool presenceInStock, ItemCategories category)
    {
        this.ID = ID;
        this.Name = name;
        this.Price = price;
        this.Quantity = quantity;
        this.PresenceInStock = presenceInStock;
        this.Category = category;
    }
}
