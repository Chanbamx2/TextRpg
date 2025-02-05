
using System;
using System.Dynamic;
using System.Xml.Linq;

public class Menu
{
    public List<string> menu;

    public Status status;
    public Inventory inventory;
    public Shop shop;

    public DungeonList dungeonList;

    public Menu()
    {
        menu = new List<string>() { "상태 보기", "인벤토리", "상점", "던전입장"};
        status = new Status();
        inventory = new Inventory();
        shop = new Shop();
        dungeonList = new DungeonList();
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

            case "4":
                Console.Clear();
                dungeonList.ChoiceDungeon(status, this);
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
        Console.Write("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.Clear();
                ShowBuyItem();
                break;

            case "2":
                Console.Clear();
                ShowSellItem();
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
            ShowShop();
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

    public void ShowSellItem() // 아이템 판매창 출력
    {
        Console.WriteLine("\n상점 - 아이템 판매\n아이템을 판매 할 수 있는 상점입니다.\n");
        Console.WriteLine($"[보유골드]\n{status.gold} G");
        Console.WriteLine($"\n[아이템 목록]");
        shop.SellItemList(inventory);
        Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        if (input == "0")
        {
            Console.Clear();
            ShowShop();
        }
        else if (Int32.TryParse(input, out int index) && index <= inventory.itemList.Count)
        {
            Console.Clear();
            shop.SellItem(index - 1, status, inventory);
            ShowSellItem();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n잘못된 입력입니다.");
            ShowSellItem();
        }
    }

}

public class Status // 스테이터스
{
    public int level;
    public float power;
    public int defense;
    public int health;
    public int gold;
    public int exp;
    public int needExp;
    public string name;
    public string classes;

    public string lev;

    public Status()
    {
        level = 01; power = 10; defense = 5; health = 100; gold = 1500; exp = 0; needExp = 1;
        name = "Chad"; classes = "( 전사 )";
        lev = level.ToString().Length == 1 ? $"0{level.ToString()}" : level.ToString() ;
    }

    public void LevelUp()
    {
        if (exp == needExp)
        {
            level++;
            power += 0.5f;
            defense++;
            exp = 0;
            needExp++;

            Console.WriteLine($"\n레벨 업! 현재 레벨 : {level}");
        }
    }
}

public class Item // 아이템
{
    public string Name { get;}
    public string Option { get; }
    public string Explanation { get; }
    public string Type { get; }

    public bool CanBuy { get; set; } = true;
    public bool IsEquip { get; set; } = false;

    public int Price { get; }

    public Item(string name, string option, string explanation, int price, string type)
    {
        Name = name;
        Option = option;
        Explanation = explanation;
        Price = price;
        Type = type;
    }

    public virtual void EquipOption(Status status) { }
}

public class Item1 : Item   // 수련자 갑옷
{
    public Item1() : base("수련자 갑옷      ", "  방어력 + 5  ", "  수련에 도움을 주는 갑옷입니다.                          ", 1000,"armor") { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.defense += 5 * i;
    }
}

public class Item2 : Item  // 무쇠갑옷
{
    public Item2() : base("무쇠갑옷         ", "  방어력 + 9  ", "  무쇠로 만들어져 튼튼한 갑옷입니다.                      ", 2000,"armor") { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.defense += 9 * i;
    }
}

public class Item3 : Item   // 스파르타의 갑옷
{
    public Item3() : base("스파르타의 갑옷  ", "  방어력 +15  ", "  스파르타의 전사들이 사용했다는 전설의 갑옷입니다.       ", 3500, "armor") { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.defense += 15 * i;
    }
}

public class Item4 : Item   // 낡은 검
{
    public Item4() : base("낡은 검          ", "  공격력 + 2  ", "  쉽게 볼 수 있는 낡은 검 입니다.                         ", 600, "weapon") { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.power += 2 * i;
    }
}

public class Item5 : Item   // 청동 도끼
{
    public Item5() : base("청동 도끼        ", "  공격력 + 5  ", "  어디선가 사용됐던 거 같은 도끼입니다.                   ", 1500, "weapon") { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.power += 5 * i;
    }
}

public class Item6 : Item   // 스파르타의 창
{
    public Item6() : base("스파르타의 창    ", "  공격력 + 7  ", "  스파르타의 전사들이 사용했다는 전설의 창입니다.         ", 2500, "weapon") { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.power += 7 * i;
    }
}

public class Item7 : Item   // 쿠키 슬라임
{
    public Item7() : base("쿠키 슬라임      ", "  체  력 +10  ", "  코딩을 하다가 머리가 아플 때 도움이 될 수도 있습니다.   ", 3000, "accessory") { }

    public override void EquipOption(Status status)
    {
        int i = IsEquip == true ? 1 : -1;
        status.health += 10 * i;
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
            foreach (Item i in itemList)
            {
                if (i.Type == item.Type && i.IsEquip == true)
                {
                    i.IsEquip = false;
                    i.EquipOption(status);
                    break;
                }
            }
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
            new Item6(),
            new Item7()
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
                string price = item.CanBuy == true ? item.Price.ToString() + "G" : "구매 완료";
                Console.WriteLine($"-  {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Explanation}"+ "|"+ $"  {price}".PadLeft(7));
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

    public void SellItemList(Inventory inventory)   // 아이템 판매 리스트 출력
    {
        if (inventory.itemList.Count == 0)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            foreach (Item item in inventory.itemList)
            {
                string price = ((int)(item.Price * 0.85f)).ToString() + "G";
                Console.WriteLine($"- {i} {item.Name}" + "|" + $"{item.Option}" + "|" + $"{item.Explanation}" + "|" + $"  {price}".PadLeft(7));
                i++;
            }
        }
    }

    public void SellItem(int index, Status status, Inventory inventory) // 아이템 판매
    {
        Item item = inventory.itemList[index];
        inventory.EquipItem(index, status);
        inventory.itemList.RemoveAt(index);
        itemList[index].CanBuy = true;
        status.gold += (int)(item.Price * 0.85f);
        Console.WriteLine("판매를 완료했습니다.");
    }
}

public class DungeonList    // 던전 리스트 관리
{
    public List<Dungeon> List;

    public void ChoiceDungeon(Status status, Menu menu)     // 던전 난이도 선택창 출력
    {
        List = new List<Dungeon>() { new EasyDungeon(), new NormalDungeon(), new HardDungeon() };

        Console.WriteLine("\n던전입장\n이 곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
       
        int i = 1;

        foreach (Dungeon dungeon in List)
        {
            string blank = $"{dungeon.Difficulty}".Length == 5 ? " " : "";
            Console.WriteLine($"{i}. "+$"{dungeon.Difficulty}".PadRight(7)+ $"{blank}"+"| 방어력 "+$"{dungeon.RecommendedDefense}".PadLeft(2)+" 이상 권장");
            i++;
        }
        Console.WriteLine("0. 나가기\n");
        Console.Write("원하시는 행동을 입력해주세요.\n>>");

        string input = Console.ReadLine();

        DoDungeon(input, status, menu);
    }

    public void DoDungeon(string input, Status status, Menu menu)
    {
        if (input == "0")
        {
            menu.ShowMenu();
        }
        else if (Int32.TryParse(input, out int index) && index <= List.Count)
        {
            List[index - 1].EnterDungeon(status);
            List[index - 1].ExitDungeon(menu);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n잘못된 입력입니다.");
            ChoiceDungeon(status, menu);
        }
    }
}

public class Dungeon
{
    public int GetExp { get;}
    public string Difficulty {  get;}
    public int RecommendedDefense {  get;}
    public int Damage {  get;}
    public int Reward {  get;}
    public bool CanEnter { get; private set; } = true;

    Random random = new Random();

    public Dungeon(string dif, int RD, int reward)
    {
        GetExp = 1;
        Damage = random.Next(20, 36);
        Difficulty = dif;
        Reward = reward;
        RecommendedDefense = RD;
    }

    public void EnterDungeon(Status status)
    {
        Console.Clear();
        if (status.defense < RecommendedDefense)
        {
            Random random = new Random();
            int i = random.Next(0, 4);

            if (i < 4)
            {
                Fail(status);
            }
        }

        if (CanEnter)
        {
            ClearDungeon(status);
        }
    }

    public void ClearDungeon(Status status)
    {
        int AddReward = Reward * random.Next((int)status.power, (int)status.power * 2) / 100;
        status.exp += GetExp;
        int health = status.health;
        status.health -= Damage - (status.defense - RecommendedDefense);
        int gold = status.gold;
        status.gold += Reward + AddReward;

        Console.WriteLine($"\n던전 클리어\n축하합니다!!\n{Difficulty}을 클리어 하였습니다\n");
        Console.WriteLine($"[탐험 결과]\n체력 {health} -> {status.health}\nGold {gold} G -> {status.gold} G");
        status.LevelUp();
    }

    public void Fail(Status status)
    {
        Console.Clear();
        CanEnter = false;
        int health = status.health;
        status.health -= (Damage - (status.defense - RecommendedDefense)) / 2;
        Console.WriteLine($"\n던전 클리어 실패\n{Difficulty} 클리어에 실패하였습니다.\n");
        Console.WriteLine($"[탐험 결과]\n체력 {health} -> {status.health}");
    }

    public void ExitDungeon(Menu menu)
    {
        Console.WriteLine("\n0. 퇴장하기\n");
        Console.Write("원하시는 행동을 입력해주세요.\n>>");
        string input = Console.ReadLine();

        if (input == "0")
        {
            Console.Clear();
            menu.ShowMenu();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n잘못된 입력입니다.");
            ExitDungeon(menu);
        }
    }
}

public class EasyDungeon : Dungeon
{
    public EasyDungeon() : base("쉬운 던전", 5, 1000) { }
}

public class NormalDungeon : Dungeon
{
    public NormalDungeon() : base("일반 던전", 11, 1700) { }
}

public class HardDungeon : Dungeon
{
    public HardDungeon() : base("어려운 던전", 17, 2500) { }
}

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();

        menu.ShowMenu();
    }
}