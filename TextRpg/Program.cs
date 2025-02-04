
using System.Xml.Linq;

public class Menu
{
    public List<string> menu;

    public Status status;
    public Inventory inventory;
    public Shop shop;

    public int i;

    public Menu()
    {
        menu = new List<string>() { "상태 보기", "인벤토리", "상점"};
        status = new Status();
        inventory = new Inventory();
        shop = new Shop();
    }

    public void ShowMenu()
    {
        Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

        i = 1;
        foreach (string m in menu)
        {
            Console.WriteLine($"{i}. {m}");
            i++;
        }

        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                 Console.Clear();
                 ShowStatus();
                 break;

            case "2":
                Console.Clear();
                ShowInventoryList();
                break;

            case "3":
                Console.Clear();
                ShowShopList();
                break;

            default:
                Console.Clear();
                ShowMenu();
                break;
        }   
    }

    public void ShowStatus()
    {
        Console.WriteLine("\n상태 보기\n캐릭터의 정보가 표시됩니다.\n");
        Console.WriteLine($"Lv. {status.lev}\n{status.name} {status.classes}\n공격력 : {status.power}\n방어력 : {status.defense}\n체 력 : {status.health}\nGold : {status.gold} G");
        Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "0" :
            Console.Clear();
            ShowMenu();
                break;

            default:
                Console.Clear();
                ShowStatus();
                break;
        }
    }

    public void ShowInventoryList()
    {
        Console.WriteLine("\n인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine($"[아이템 목록]");
        inventory.ItemList();
        Console.Write("\n1. 장착 관리\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.Clear();
                ShowEquipList();
                break;

            case "0":
                Console.Clear();
                ShowMenu();
                break;

            default:
                Console.Clear();
                ShowInventoryList();
                break;
        }
    }

    public void ShowEquipList()
    {
        Console.WriteLine("\n인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine($"[아이템 목록]");
        inventory.ItemEquip();
        Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":

            case "2":

            case "3":

            case "4":

            case "5":

            case "6":
                Equip(input);
                break;

            case "0":
                Console.Clear();
                ShowInventoryList();
                break;

            default:
                Console.Clear();
                ShowEquipList();
                break;
        }
    }

    public void Equip(string input)
    {
        if (inventory.inventory[Int32.Parse(input) - 1].Equip == false)
        {
            inventory.inventory[Int32.Parse(input) - 1].Equip = true;
        }
        else
        {
            inventory.inventory[Int32.Parse(input) - 1].Equip = false;
        }
        inventory.inventory[Int32.Parse(input) - 1].EquipOption(status);
        Console.Clear();
        ShowEquipList();
    }

    public void ShowShopList()
    {
        Console.WriteLine("\n상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine($"[보유골드]\n{status.gold} G");
        Console.WriteLine($"\n[아이템 목록]");
        shop.ItemList();
        Console.Write("\n1. 아이템 구매\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.Clear();
                ShowBuyItem();
                break;

            case "0":
                Console.Clear();
                ShowMenu();
                break;

            default:
                Console.Clear();
                ShowShopList();
                break;
        }
    }

    public void ShowBuyItem()
    {
        Console.WriteLine("\n상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine($"[보유골드]\n{status.gold} G");
        Console.WriteLine($"\n[아이템 목록]");
        shop.BuyItem();
        Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "0":
                Console.Clear();
                ShowMenu();
                break;

            case "1":

            case "2":

            case "3":

            case "4":

            case "5":

            case "6":
                CanBuy(input);
                break;

            default:
                Console.Clear();
                ShowBuyItem();
                break;
        }
    }

    public void CanBuy(string input)
    {
        if (shop.shop[Int32.Parse(input) - 1].CanBuy == false)
        {
            Console.Clear();
            Console.WriteLine("\n이미 구매한 아이템입니다.");
            ShowBuyItem();
        }
        else if (status.gold >= shop.shop[Int32.Parse(input) - 1].Price)
        {
            status.gold -= shop.shop[Int32.Parse(input) - 1].Price;
            inventory.inventory.Add(shop.shop[Int32.Parse(input) - 1]);
            shop.shop[Int32.Parse(input) - 1].CanBuy = false;
            Console.Clear();
            ShowBuyItem();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n보유골드가 부족합니다.");
            ShowBuyItem();
        }
    }
}

public class Status
{
    public int level;
    public int power;
    public int defense;
    public int health;
    public int gold;
    public string name;
    public string classes;

    public string lev;

    public Status()
    {
        level = 01; power = 10; defense = 5; health = 100; gold = 3500;
        name = "Chad"; classes = "( 전사 )";
        lev = level.ToString().Length == 1 ? $"0{level.ToString()}" : level.ToString() ;
    }
}

public class Item
{
    public string Name { get; set; }
    public string Option { get; }
    public string Explanation { get; }

    public bool CanBuy { get; set; } = true;
    public bool Equip { get; set; } = false;

    public int Price { get; }

    public Item(string name, string option, string explanation, int price)
    {
        Name = name;
        Option = option;
        Explanation = explanation;
        Price = price;
    }

    public virtual void EquipOption(Status status) { }
}

public class Item1 : Item
{
    public Item1() : base("수련자 갑옷      ", "  방어력 +5  ", "  수련에 도움을 주는 갑옷입니다.                    ",1000) { }

    public override void EquipOption(Status status)
    {
        int i = Equip == true ? 1 : -1;
        status.defense += 5 * i;
    }
}

public class Item2 : Item
{
    public Item2() : base("무쇠갑옷         ", "  방어력 +9  ", "  무쇠로 만들어져 튼튼한 갑옷입니다.                ",2000) { }

    public override void EquipOption(Status status)
    {
        int i = Equip == true ? 1 : -1;
        status.defense += 9 * i;
    }
}

public class Item3 : Item
{
    public Item3() : base("스파르타의 갑옷  ", "  방어력 +15 ", "  스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ",3500) { }

    public override void EquipOption(Status status)
    {
        int i = Equip == true ? 1 : -1;
        status.defense += 15 * i;
    }
}

public class Item4 : Item
{
    public Item4() : base("낡은 검          ", "  공격력 +2  ", "  쉽게 볼 수 있는 낡은 검 입니다.                   ", 600) { }

    public override void EquipOption(Status status)
    {
        int i = Equip == true ? 1 : -1;
        status.power += 2 * i;
    }
}

public class Item5 : Item
{
    public Item5() : base("청동 도끼        ", "  공격력 +5  ", "  어디선가 사용됐던 거 같은 도끼입니다.             ", 1500) { }

    public override void EquipOption(Status status)
    {
        int i = Equip == true ? 1 : -1;
        status.power += 5 * i;
    }
}

public class Item6 : Item
{
    public Item6() : base("스파르타의 창    ", "  공격력 +7  ", "  스파르타의 전사들이 사용했다는 전설의 창입니다.   ", 2500) { }

    public override void EquipOption(Status status)
    {
        int i = Equip == true ? 1 : -1;
        status.power += 7 * i;
    }
}


public class Inventory
{
    public List<Item> inventory;

    public Inventory()
    {
        inventory = new List<Item>();
    }

    public void ItemList()
    {
        if (inventory.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            foreach (Item _item in inventory)
            {
                if (_item.Equip == true) Console.WriteLine($"-  [E] {_item.Name}" + "|" + $"{_item.Option}" + "|" + $"{_item.Explanation}");
                else Console.WriteLine($"-  {_item.Name}" + "|" + $"{_item.Option}" + "|" + $"{_item.Explanation}");
                i++;
            }
        }
    }

    public void ItemEquip()
    {
        if (inventory.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            foreach (Item _item in inventory)
            {
                if(_item.Equip == true) Console.WriteLine($"- {i} [E] {_item.Name}" + "|" + $"{_item.Option}" + "|" + $"{_item.Explanation}");
                else Console.WriteLine($"- {i} {_item.Name}" + "|" + $"{_item.Option}" + "|" + $"{_item.Explanation}");
                i++;
            }
        }
    }
}

public class Shop
{
    public List<Item> shop;

    public Shop()
    {
        shop = new List<Item>();
    }

    public void ItemList()
    {
        if (shop.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            
            foreach (Item _item in shop)
            {
                string price = _item.CanBuy == true ? _item.Price.ToString() : "구매 완료";
                Console.WriteLine($"-  {_item.Name}" + "|" + $"{_item.Option}" + "|" + $"{_item.Explanation}"+ "|"+ $"  {price}G".PadLeft(7));
                i++;
            }
        }
    }

    public void BuyItem()
    {
        if (shop.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            foreach (Item _item in shop)
            {
                string price = _item.CanBuy == true ? _item.Price.ToString() + "G" : "구매 완료";
                Console.WriteLine($"- {i} {_item.Name}" + "|" + $"{_item.Option}" + "|" + $"{_item.Explanation}" + "|" + $"  {price}".PadLeft(7));
                i++;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();

        Item1 item1 = new Item1();
        Item2 item2 = new Item2();
        Item3 item3 = new Item3();
        Item4 item4 = new Item4();
        Item5 item5 = new Item5();
        Item6 item6 = new Item6();

        menu.shop.shop.Add(item1);
        menu.shop.shop.Add(item2);
        menu.shop.shop.Add(item3);
        menu.shop.shop.Add(item4);
        menu.shop.shop.Add(item5);
        menu.shop.shop.Add(item6);

        menu.ShowMenu();
    }
}