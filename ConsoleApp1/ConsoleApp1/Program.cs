using System.Collections;
using System.Text;

void readFileMenu()
{
    string filePath = "D:\\LoremIpsum.txt";

    Console.WriteLine("Write the number of words you want to see: ");

    int n = Convert.ToInt32(Console.ReadLine());

    if (File.Exists(filePath))
    {
        string text = File.ReadAllText(filePath);
        string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (n > words.Length) {
            n = words.Length;
        }

        Console.WriteLine("Your text:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"{words[i]} ");
        }
        Console.WriteLine();
    }
    else {
        Console.WriteLine("File doesn't exist!");
    }
}

int mathMenu()
{
    while (true)
    {

        Console.WriteLine("Write first number");
        string num1Str = Console.ReadLine();
        int num1;
        if (!int.TryParse(num1Str, out num1))
        {
            if (num1Str == "exit") {
                Console.WriteLine("Exiting..");
                return 0;
            }
            Console.WriteLine("Invalid input! The first number should be an integer.");
            continue;
        }

        Console.WriteLine("Write second number");
        string num2Str = Console.ReadLine();
        int num2;
        if (!int.TryParse(num2Str, out num2))
        {
            if (num1Str == "exit")
            {
                Console.WriteLine("Exiting..");
                return 0;
            }
            Console.WriteLine("Invalid input! The second number should be an integer.");
            continue;
        }

        Console.WriteLine("Write operand from list (+, -, /, *) or exit");
        string operand = Console.ReadLine();

        float result = 0;
        switch (operand)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "/":
                result = num1 / num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "exit":
                return 0;
            default:
                Console.WriteLine("Unknown operand! Try again!");
                continue;
        }
        Console.WriteLine($"{num1} {operand} {num2} = {result}");
    }
    return 0;
}

void main() {
    while (true)
    {
        Console.WriteLine("Menu:");
        Console.WriteLine("1 - Write n words from file");
        Console.WriteLine("2 - Do some meth");
        Console.WriteLine("3 - Exit");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("Reading text...");
                readFileMenu();
                break;
            case "2":
                Console.WriteLine("Doint math:");
                mathMenu();
                break;
            case "3":
                Console.WriteLine("sya!");
                return;
            default:
                Console.WriteLine("Unknown choice! Choose again!");
                break;
        }
    }
}


main();