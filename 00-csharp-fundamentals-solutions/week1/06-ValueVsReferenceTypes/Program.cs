int valueX = 5;
ValueDemo.Bump(valueX);
Console.WriteLine($"valueX remains unaltered: {valueX}");

int swap1 = 5;
int swap2 = 6;

Console.WriteLine($"swap1 before: {swap1}");
Console.WriteLine($"swap2 before: {swap2}");

RefOps.Swap(ref swap1, ref swap2);

Console.WriteLine($"swap1 after: {swap1}");
Console.WriteLine($"swap2 after: {swap2}");

string goodPrice1 = "12.50";
string goodPrice2 = "£12.50";
string goodPrice3 = "12,50";

string badPrice = "asfdasf";

decimal out1;
decimal out2;
decimal out3;
decimal outBad;

bool couldParse1 = Parser.TryParsePrice(goodPrice1, out out1);
bool couldParse2 = Parser.TryParsePrice(goodPrice2, out out2);
bool couldParse3 = Parser.TryParsePrice(goodPrice3, out out3);
bool badParse = Parser.TryParsePrice(badPrice, out outBad);

Console.WriteLine($"{goodPrice1} couldParse: {couldParse1}, result: {out1}");
Console.WriteLine($"{goodPrice2} couldParse: {couldParse2}, result: {out2}");
Console.WriteLine($"{goodPrice3} couldParse: {couldParse3}, result: {out3}");
Console.WriteLine($"{badPrice} badParse: {badParse}, result: {outBad}");