using Utils;

// or without using directive
// 	Utils.Greeter.Welcome();

Greeter.Welcome();                    // uses both defaults
Greeter.Welcome(course: "LINQ");      // named argument
Greeter.Welcome(name: "Amir");       // override one
Greeter.Welcome("Priya", "ASP.NET"); // positional
