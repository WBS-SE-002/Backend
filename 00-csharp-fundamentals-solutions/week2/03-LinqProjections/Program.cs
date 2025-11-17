var day1 = new DaySchedule
{
  Date = new DateOnly(2025, 9, 15),
  Sessions = new List<Session>
    {
        new Session { Title = "Modern C# Patterns", Track = "Backend", DurationMinutes = 50, Tags = new() { "C#", "Design" } },
        new Session { Title = "LINQ Deep Dive", Track = "Backend", DurationMinutes = 60, Tags = new() { "C#", "LINQ", "Performance" } },
        new Session { Title = "WebAssembly with Blazor", Track = "Frontend", DurationMinutes = 45, Tags = new() { "Blazor", "WASM" } },
    }
};

var day2 = new DaySchedule
{
  Date = new DateOnly(2025, 9, 16),
  Sessions = new List<Session>
    {
        new Session { Title = "APIs at Scale", Track = "Backend", DurationMinutes = 40, Tags = new() { "API", "Scaling" } },
        new Session { Title = "UX Micro‑interactions", Track = "Frontend", DurationMinutes = 35, Tags = new() { "UX", "Animation" } },
        new Session { Title = "Streaming Data 101", Track = "Data", DurationMinutes = 55, Tags = new() { "Data", "Streams" } },
    }
};

var conference = new List<DaySchedule> { day1, day2 };


// Helper sequences for Zip tasks
var rooms = new[] { "Room A", "Room B", "Room C" };
var timeSlots = new[] { "09:00", "10:30", "12:00" };

// Select (projection)
var day1SessionTitles = day1.Sessions.Select(session => session.Title);
Console.WriteLine("Day 1 Session titles:");
foreach (var title in day1SessionTitles) Console.WriteLine(title);
Console.WriteLine();


var day1SessionDurations = day1.Sessions.Select(session => session.DurationMinutes);
Console.WriteLine("Day 1 Session durations:");
foreach (var duration in day1SessionDurations) Console.WriteLine(duration);
Console.WriteLine();

// to get all sessions
var allSessions = conference.SelectMany(day => day.Sessions).ToList();

var allTitleAndTrack = allSessions.Select(session => new { session.Title, session.Track });
Console.WriteLine("All tracks and titles:");
foreach (var item in allTitleAndTrack) Console.WriteLine($"{item.Title}: {item.Track}");
Console.WriteLine();


var sessionCards = allSessions.Select(session => new SessionCard(session.Title, session.DurationMinutes));
Console.WriteLine("Session Cards:");
foreach (var item in sessionCards) Console.WriteLine($"{item.Title}: {item.DurationMinutes} minutes");
Console.WriteLine();

var minutesPerTag = allSessions.Select(session => new { session.Title, MinutesPerTag = session.DurationMinutes / Math.Max(1, session.Tags.Count) });
Console.WriteLine("Session Cards:");
foreach (var item in minutesPerTag) Console.WriteLine($"{item.Title}: {item.MinutesPerTag} minutes per tag");
Console.WriteLine();

// SelectMany (flattening)
// done already
// var allTitleAndTrack = allSessions.Select(session => new { session.Title, session.Track });
// Console.WriteLine("All tracks and titles:");
// foreach (var item in allTitleAndTrack) Console.WriteLine($"{item.Title}: {item.Track}");
// Console.WriteLine();

var allTags = conference.SelectMany(day => day.Sessions).SelectMany(session => session.Tags);
Console.WriteLine("All tags:");
foreach (var item in allTags) Console.WriteLine(item);
Console.WriteLine();

var datesAndTitles = conference.SelectMany(day => day.Sessions.Select(session => new { day.Date, session.Title })).ToList();
Console.WriteLine("Dates and Titles:");
foreach (var item in datesAndTitles) Console.WriteLine($"{item.Date:yyyy-MM-dd}: {item.Title}");
Console.WriteLine();


// Zip (combining sequences)
var first3Titles = datesAndTitles.Select(info => info.Title).Take(3);

var roomAssignments = first3Titles.Zip(rooms, (title, room) => $"{title} @ {room}");
Console.WriteLine("Room assignments:");
foreach (var item in roomAssignments) Console.WriteLine(item);
Console.WriteLine();

var eventSchedule = first3Titles.Zip(rooms, (title, room) => (title, room)).Zip(timeSlots, (info, time) => $"{time} -- {info.title} -- {info.room}");
Console.WriteLine("Schedule:");
foreach (var item in eventSchedule) Console.WriteLine(item);
Console.WriteLine();


public record SessionCard(string Title, int DurationMinutes);