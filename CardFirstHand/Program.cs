// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;

string firstHandNumInput = "";
string trialNumInput = "";

Console.Write("手札の枚数 : ");
firstHandNumInput = Console.ReadLine();

int firstHandNum = 0;
while (!int.TryParse(firstHandNumInput, out firstHandNum)) 
{
    Console.WriteLine("整数値を入力してください");
    Console.WriteLine("");
    Console.Write("手札の枚数 : ");
    firstHandNumInput = Console.ReadLine();
}

Console.Write("試行回数 : ");
trialNumInput = Console.ReadLine();

int trialNum = 0;
while (!int.TryParse(trialNumInput, out trialNum))
{
    Console.WriteLine("整数値を入力してください");
    Console.WriteLine("");
    Console.Write("試行回数 : ");
    trialNumInput = Console.ReadLine();
}


string[] deck;
string[] handListInput;

using (var srDeck = new StreamReader(@"input\Deck.txt"))
using (var srHand = new StreamReader(@"input\Hand.txt"))
{
    deck = srDeck.ReadToEnd().Split("\r\n");
    handListInput = srHand.ReadToEnd().Split("\r\n");
}

var handList = new List<List<string>>();

foreach (var hand in handListInput)
{
    handList.Add(hand.Split(",").ToList());
}

var randomHand = new List<string>();
var random = new Random();
var matched = new List<int>();

using (var sw = new StreamWriter(@$"log.txt", false, Encoding.UTF8))
{
    for (int i = 0; i < trialNum; i++)
    {
        var exist = false;
        randomHand = deck.OrderBy(x => random.NextDouble()).Take(5).ToList();
        sw.WriteLine($"{i + 1}回目の手札");
        sw.WriteLine($"{string.Join(",", randomHand)}");
        sw.WriteLine("");

        foreach (var hand in handList)
        {
            if (randomHand.Intersect(hand).Count() == hand.Count())
            {
                exist = true;
                break;
            }
        }

        if (exist == true)
        {
            matched.Add(1);
        }
        else
        {
            matched.Add(0);
        }
    }
}


Console.WriteLine("");
Console.WriteLine("―――――結果―――――");
Console.WriteLine("");
Console.WriteLine($"{matched.Sum()}回 / {trialNum}回中");
Console.WriteLine("");
Console.WriteLine("―――――――――――――");

Console.WriteLine("");
Console.WriteLine("ENTERで終了");
while (Console.ReadKey().Key != ConsoleKey.Enter) { }
