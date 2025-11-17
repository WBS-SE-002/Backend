var currTemp = new Thermometer(40);

Console.WriteLine($"initial temp: {currTemp.TemperatureCelsius} C");
// currTemp.TemperatureCelsius = 32; // compilation error

currTemp.SetTemperature(55.4);

Console.WriteLine($"after updating with setter: {currTemp.TemperatureCelsius} C");
Console.WriteLine($"in Fahrenheit: {currTemp.GetFahrenheit()} F");

var student = new Student();

Console.WriteLine($"student name and grade: {student.Name}, {student.Grade}");

// student.Grade = 77; //compilation error

student.UpdateGrade(95);

Console.WriteLine($"new grade: {student.Grade}");

student.Introduce();

var door = new Door();
door.Status();
door.Open();
door.Status();
door.Close();
door.Status();