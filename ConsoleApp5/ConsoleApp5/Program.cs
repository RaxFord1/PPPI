using ConsoleApp5;

ClientWeather client = new ClientWeather("b4af138646d645c952b8e9b795cbabe4");

Console.WriteLine("Kyiv:\n");
WeatherModel result = await client.Get();

result.PrintResult();

Console.WriteLine("\n\nLVIV:\n");

WeatherModel result2 = await client.Post("Lviv");

result2.PrintResult();
