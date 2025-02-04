
using System.Xml.Linq;

public class Menu
{
    public List<string> menu;

    Status status;
    Item item;

    public int i;

    public Menu()
    {
        menu = new List<string>() { "상태 보기", "인벤토리", "상점"};
        status = new Status();
        item = new Item();
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
                ShowItemList();
                break;
        }   
    }

    public void ShowStatus()
    {
        Console.WriteLine("\n상태 보기\n캐릭터의 정보가 표시됩니다.\n");
        Console.WriteLine($"Lv. {status.lev}\n{status.name} {status.classes}\n공격력 : {status.power}\n방어력 : {status.defense}\n체 력 : {status.health}\nGold : {status.gold} G");
        Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        if (input == 0.ToString())
        {
            Console.Clear();
            ShowMenu();
        }
    }

    public void ShowItemList()
    {
        Console.WriteLine("\n인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine($"[아이템 목록]");
        item.ItemList();
        Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.Clear();
                ShowStatus();
                break;

            case "0":
                Console.Clear();
                ShowMenu();
                break;
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
        level = 01; power = 10; defense = 5; health = 100; gold = 1500;
        name = "Chad"; classes = "( 전사 )";
        lev = level.ToString().Length == 1 ? $"0{level.ToString()}" : level.ToString() ;
    }
}

public class Item
{
    List<string> item;

    public Item()
    {
        item = new List<string>();
    }

    public void ItemList()
    {
        if (item == null)
        {
            Console.WriteLine("");
        }
        else
        {
            int i = 1;
            foreach (string _item in item)
            {
                Console.WriteLine($"- {i} {_item}");
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
        Status status = new Status();

        menu.ShowMenu();
    }
}