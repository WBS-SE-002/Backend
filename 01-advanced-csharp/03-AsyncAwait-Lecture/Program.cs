using System.Diagnostics;

static async Task<object> FetchDataAsync(string url)
{
  using var httpClient = new HttpClient();

  var data = await httpClient.GetStringAsync(url);
  return data;
}

// var ducks = await FetchDataAsync("https://duckpond-89zn.onrender.com/wild-ducks");
// Console.WriteLine(ducks);

static async Task DelayedLogAsync(string message)
{
  await Task.Delay(500);
  // throw new NotImplementedException("You didn't write this yet?");
  Console.WriteLine(message);
}

// await DelayedLogAsync("I'm slow...");

// try
// {
//   await DelayedLogAsync("Delayed hello");
// }
// catch (Exception ex)
// {
//   Console.WriteLine($"Error: {ex.Message}");
// }

var stopWatch = Stopwatch.StartNew();
Console.WriteLine("Hello");
Console.WriteLine($"Sync elapsed: {stopWatch.ElapsedMilliseconds} ms\n");


// var firstTask = DelayedLogAsync("Delayed hello");
// await DelayedLogAsync("Another delayed hello");
// await firstTask;

var tasks = new[] { DelayedLogAsync("Delayed hello"), DelayedLogAsync("Another delayed hello") };

await Task.WhenAll(tasks);

Console.WriteLine($"Async elapsed: {stopWatch.ElapsedMilliseconds} ms\n");