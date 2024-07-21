/// <summary>
/// A utility class for managing dialogues and user interactions.
/// </summary>
class DialogueManager
{
    /// <summary>
    /// Gets a menu choice from the user.
    /// </summary>
    /// <param name="prompt">The prompt message to display.</param>
    /// <param name="options">The list of menu options to choose from.</param>
    /// <returns>The user's selected choice.</returns>
    public static string GetMenuChoice(string prompt, List<string> options)
    {
        if (prompt == null)
        {
            DisplayErrorMessage("Неверный ввод. Приглашение (prompt) не может быть null.");
            return "";
        }
        
        if (options == null || options.Count == 0)
        {
            DisplayErrorMessage("Неверные варианты меню. Список вариантов null или пуст.");
            return ""; 
        }
        
        string choice = "";
        
        Console.WriteLine(prompt);

        // Display the menu options with indices.
        for (int i = 0; i < options.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.ResetColor(); 

        Console.Write("\nВаш выбор: ");
        choice = Console.ReadLine();
        Console.WriteLine();
        
        // Validate the user's choice.
        while (!IsOptionValid(choice, options.Count))
        {
            DisplayErrorMessage("Такого варианта не существует. Повторите попытку.");
            Console.Write("Ваш выбор: ");
            choice = Console.ReadLine();
            Console.WriteLine();
        }

        return choice;
    }

    /// <summary>
    /// Checks if the user's option is valid.
    /// </summary>
    /// <param name="choice">The user's input choice.</param>
    /// <param name="optionsCount">The total number of menu options.</param>
    /// <returns>True if the choice is valid, false otherwise.</returns>
    private static bool IsOptionValid(string choice, int optionsCount)
    {
        if (string.IsNullOrEmpty(choice) || !int.TryParse(choice, out int numericChoice))
        {
            return false;
        }

        return numericChoice >= 1 && numericChoice <= optionsCount;
    }
    
    /// <summary>
    /// Displays a success message to the console.
    /// </summary>
    /// <param name="successMessage">The success message to display.</param>
    public static void DisplaySuccessMessage(string successMessage)
    {
        if (string.IsNullOrWhiteSpace(successMessage))
        {
            throw new ArgumentException("Сообщение не должно быть пустым.", nameof(successMessage));
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(successMessage);
        Console.ResetColor();
    }

    /// <summary>
    /// Displays an error message to the console.
    /// </summary>
    /// <param name="errorMessage">The error message to display.</param>
    public static void DisplayErrorMessage(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new ArgumentException("Сообщение не должно быть пустым.", nameof(errorMessage));
        }
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.WriteLine();
        Console.ResetColor();
    }

    /// <summary>
    /// Gets user input from the console.
    /// </summary>
    /// <param name="prompt">The prompt message to display.</param>
    /// <returns>The user's input.</returns>
    public static string GetUserInput(string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            throw new ArgumentException("Строка не должна быть пустой.", nameof(prompt));
        }
        
        string userInput = "";

        do
        {
            Console.Write(prompt);
            userInput = Console.ReadLine().Trim();

        } while (string.IsNullOrEmpty(userInput));

        return userInput;
    }
}