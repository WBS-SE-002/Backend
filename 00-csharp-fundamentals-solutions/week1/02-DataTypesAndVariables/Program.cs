// declarations and literals
int age = 23;
long worldPopulation = 8_000_000_000;
float temp = 36.6F;
double price = 13.99;

Console.WriteLine($"{age}, {worldPopulation}, {temp}, {price}");


// arithmetic and integer division
int a = 10;
int b = 3;

Console.WriteLine($"a + b = {a + b}, a - b = {a - b}, a * b  = {a * b}, a / b = {a / b}, a % b = {a % b}");
Console.WriteLine($"(double) a / b = {(double)a / b}");

// Comparison and logical operators
Console.WriteLine($" a > b: {a > b}, a < b: {a < b}, a == b: {a == b}, a != b: {a != b}");
// combine with &&, ||, !
bool cond = (a < 10) && (a > 10) || !(a == 7);
Console.WriteLine($"combined condition={cond}");

// scientific notation
double distanceToSun = 1.496e8;
double electronMass = 9.109e-31;

Console.WriteLine($"distanceToSun: {distanceToSun}, electronMass: {electronMass}");

// compound assignment and increment

int counter = 0;

counter += 5;

Console.WriteLine($"after adding 5: {counter}");

counter -= 3;

Console.WriteLine($"after subtracting 3: {counter}");

counter *= 4;
Console.WriteLine($"after multiplying by 4: {counter}");


counter /= 3;
Console.WriteLine($"after dividing by 3: {counter}");

counter++;
Console.WriteLine($"after incrementing: {counter}");


//conversion (casts) and rounding
double score = 7.85D;
int truncated = (int)score;
double rounded = Math.Round(score, 1);
float scoreF = (float)score;

Console.WriteLine($"score: {score}, truncated: {truncated}, rounded: {rounded}, scoreF: {scoreF}");

try
{
  checked
  {
    int max = int.MaxValue;
    Console.WriteLine($"Max int: {max}");
    // The next line will throw OverflowException if executed in checked context
    int boom = max + 1;
    Console.WriteLine($"This will not print (boom={boom})");
  }
}
catch (OverflowException)
{
  Console.WriteLine("Overflow caught in checked context");
}

unchecked
{
  int max = int.MaxValue;
  int wrapped = max + 1; // wraps to negative in unchecked context
  Console.WriteLine($"Unchecked wrap-around result: {wrapped}");
}

// float vs double point precision
float f = 0.1F + 0.2F;
double d = 0.1D + 0.2D;

Console.WriteLine($"f: {f}, d: {d}, f == d: {f == d}");

double weightKg = 94D;
double heightM = 1.86D;

double bmi = weightKg / (heightM * heightM);

string bmiThreshold = bmi > 30 || bmi < 18.5 ? "not normal" : "Normal";

Console.WriteLine($"Your bmi of {bmi} is in a {bmiThreshold} range.");