
using System.Xml.Linq;

public class Menu
{
    public List<string> menu;

    public Status status;
    public Inventory inventory;
    public Shop shop;

    public Menu()
    {
        menu = new List<string>() { "상태 보기", "인벤토리", "상점"};
        status = new Status();
        inventory = new Inventory();
        shop = new Shop();
    }

    public void ShowMenu()  // 메인메뉴 출력
    {
        Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

        int i = 1;
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
                ShowInventory();
                break;

            case "3":
                Console.Clear();
                ShowShop();
                break;

            default:
                Console.Clear();
                Console.WriteLine("\n잘못된 입력입니다.");
                ShowMenu();
                break;
        }   
    }

    public void ShowStatus() // 상태창 출력
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

    public void ShowInventory() // 인벤토리 출력
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
                ShowEquipManagement();
                break;

            case "0":
                Console.Clear();
                ShowMenu();
                break;

            default:
                Console.Clear();
                Console.WriteLine("\n잘못된 입력입니다.");
                ShowInventory();
                break;
        }
    }

    public void ShowEquipManagement() // 장착 관리 출력
    {
        Console.WriteLine("\n인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine($"[아이템 목록]");
        inventory.EquipItemList();
        Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        if(input == "0")
        {
            Console.Clear();
            ShowInventory();
        }
        else if (Int32.TryParse(input, out int index) && index <= inventory.itemList.Count)
        {
            inventory.EquipItem(index - 1, status);
            Console.Clear();
            ShowEquipManagement();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n잘못된 입력입니다.");
            ShowEquipManagement();
        }
    }

    public void ShowShop() // 상점 아이템 출력
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
                ShowShop();
                break;
        }
    }

    public void ShowBuyItem() // 아이템 구매창 출력
    {
        Console.WriteLine("\n상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine($"[보유골드]\n{status.gold} G");
        Console.WriteLine($"\n[아이템 목록]");
        shop.BuyItemList();
        Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        if (input == "0")
        {
            Console.Clear();
            ShowMenu();
        }
        else if (Int32.TryParse(input, out int index) && index <= shop.itemList.Count)
        {
            Console.Clear();
            shop.BuyItem(index - 1, status, inventory);
            ShowBuyItem();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n잘못된 입력입니다.");
            ShowBuyItem();
        }
    }
}

public class Status // 스테이터스
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

public class Item // 아이템
{
    public string Name { get; set; }
    public string Option { get; }
    public string Explanation { get; }

    public bool CanBuy { get; set; } = true;
    public bool IsEquip { get; set; } = false;

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

public class Item1 : Item   // 수련자 갑옷
{
    public Item1() : base("수련자 갑옷      ", "  방어력 +5  ", "  수련에 도움을 주는 갑옷입니다.                    ",1000) { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.defense += 5 * i;
    }
}

public class Item2 : Item  // 무쇠갑옷
{
    public Item2() : base("무쇠갑옷         ", "  방어력 +9  ", "  무쇠로 만들어져 튼튼한 갑옷입니다.                ",2000) { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.defense += 9 * i;
    }
}

public class Item3 : Item   // 스파르타의 갑옷
{
    public Item3() : base("스파르타의 갑옷  ", "  방어력 +15 ", "  스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ",3500) { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.defense += 15 * i;
    }
}

public class Item4 : Item   // 낡은 검
{
    public Item4() : base("낡은 검          ", "  공격력 +2  ", "  쉽게 볼 수 있는 낡은 검 입니다.                   ", 600) { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.power += 2 * i;
    }
}

public class Item5 : Item   // 청동 도끼
{
    public Item5() : base("청동 도끼        ", "  공격력 +5  ", "  어디선가 사용됐던 거 같은 도끼입니다.             ", 1500) { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.power += 5 * i;
    }
}

public class Item6 : Item   // 스파르타의 창
{
    public Item6() : base("스파르타의 창    ", "  공격력 +7  ", "  스파르타의 전사들이 사용했다는 전설의 창입니다.   ", 2500) { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.power += 7 * i;
    }
}

public class Inventory // 인벤토리
{
    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void ItemList() // 인벤토리 아이템 리스트 출력
    {
        if (itemList.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            foreach (Item item in itemList)
            {
                if (item.IsEquip == true) Console.WriteLine($"-  [E] {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Explanation}");
                else Console.WriteLine($"-  {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Explanation}");
                i++;
            }
        }
    }

    public void EquipItemList() // 장착 관리 아이템 리스트 출력
    {
        if (itemList.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            foreach (Item item in itemList)
            {
                if(item.IsEquip == true) Console.WriteLine($"- {i} [E] {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Explanation}");
                else Console.WriteLine($"- {i} {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Explanation}");
                i++;
            }
        }
    }

    public void EquipItem(int index, Status status) // 아이템 장착
    {
        Item item = itemList[index];
        if (item.IsEquip == false)
        {
            item.IsEquip = true;
        }
        else
        {
            item.IsEquip = false;
        }
        item.EquipOption(status);
    }
}

public class Shop   // 상점
{
    public List<Item> itemList;

    public Shop()
    {
        itemList = new List<Item>()
        {
            new Item1(),
            new Item2(),
            new Item3(),
            new Item4(),
            new Item5(),
            new Item6()
        };
    }

    public void ItemList()  // 상점 아이템 리스트 출력
    {
        if (itemList.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            
            foreach (Item item in itemList)
            {
                string price = item.CanBuy == true ? item.Price.ToString() : "구매 완료";
                Console.WriteLine($"-  {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Explanation}"+ "|"+ $"  {price}G".PadLeft(7));
                i++;
            }
        }
    }

    public void BuyItemList()   // 아이템 구매 리스트 출력
    {
        if (itemList.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            foreach (Item item in itemList)
            {
                string price = item.CanBuy == true ? item.Price.ToString() + "G" : "구매 완료";
                Console.WriteLine($"- {i} {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Explanation}" + "|" + $"  {price}".PadLeft(7));
                i++;
            }
        }
    }

    public void BuyItem(int index, Status status,Inventory inventory) // 아이템 구매
    {
        Item item = itemList[index];
        if (itemList[index].CanBuy == false)
        {
            Console.WriteLine("\n이미 구매한 아이템입니다.");
        }
        else if (status.gold >= itemList[index].Price)
        {
            status.gold -= itemList[index].Price;
            inventory.itemList.Add(itemList[index]);
            itemList[index].CanBuy = false;
            Console.WriteLine("\n구매를 완료했습니다.");
        }
        else
        {
            Console.WriteLine("\nGold 가 부족합니다.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();

        menu.ShowMenu();
    }
}