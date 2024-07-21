using HW3._1;
using JsonPlayerDataProcessor;

class Program
{
    static void Main()
    {
        bool exitProgram = false;

        do
        {
            try
            {
                // Prompt the user to choose the method of reading data.
                string choice = DialogueManager.GetMenuChoice("Выберите способ считывания данных:",
                    new List<string> { "Ввести данные из консоли", "Загрузить данные из файла" });

                bool isFromFile = (choice == "2");
                string json = JsonParser.ReadJson(isFromFile);

                // Parse the JSON data into a list of Player objects.
                List<Player> players = JsonParser.ParsePlayers(json);

                // Prompt the user to choose the method of outputting data.
                string outputChoice = DialogueManager.GetMenuChoice("Выберите способ вывода данных:",
                    new List<string> { "Вывести данные в консоль", "Сохранить данные в файл" });

                bool writeToConsole = (outputChoice == "1");
                string filePath = null;

                // If saving to a file, prompt the user to enter the file path.
                if (!writeToConsole)
                {
                    filePath = DialogueManager.GetUserInput("Введите путь к файлу для сохранения данных: ");
                }

                while (true)
                {
                    // Display the main menu to the user.
                    string userChoice = DialogueManager.GetMenuChoice("\nМеню:",
                        new List<string> { "Отсортировать данные", "Отфильтровать данные", "Считать новые данные",
                            "Изменить способ считывания данных", "Изменить способ вывода данных", "Завершение программы" });

                    switch (userChoice)
                    {
                        case "1":
                            // Sort and output the data.
                            PlayerUtility.SortPlayers(players);
                            JsonParser.WriteJson(players, writeToConsole, filePath);
                            break;

                        case "2":
                            // Filter and output the data.
                            List<Player> filteredPlayers = PlayerUtility.FilterPlayers(players);
                            JsonParser.WriteJson(filteredPlayers, writeToConsole, filePath);
                            break;

                        case "3":
                            // Read new data.
                            json = isFromFile ? JsonParser.ReadJson(true) : JsonParser.ReadJson(false);
                            players = JsonParser.ParsePlayers(json);
                            DialogueManager.DisplaySuccessMessage("Новые данные успешно считаны.");
                            break;

                        case "4":
                            // Change data input method.
                            isFromFile = (DialogueManager.GetMenuChoice("Выберите способ считывания данных:",
                                new List<string> { "Ввести данные из консоли", "Загрузить данные из файла" }) == "2");
                            DialogueManager.DisplaySuccessMessage("Способ считывания данных успешно изменен.\n");
                            json = JsonParser.ReadJson(isFromFile);
                            players = JsonParser.ParsePlayers(json);
                            break;

                        case "5":
                            // Change data output method.
                            outputChoice = DialogueManager.GetMenuChoice("Выберите способ вывода данных:",
                                new List<string> { "Вывести данные в консоль", "Сохранить данные в файл" });
                            writeToConsole = (outputChoice == "1");
                            if (!writeToConsole)
                            {
                                filePath = DialogueManager.GetUserInput("Введите путь к файлу для сохранения данных: ");
                            }
                            DialogueManager.DisplaySuccessMessage("Способ вывода данных успешно изменен.");
                            break;

                        case "6":
                            // Exit the program.
                            exitProgram = true;
                            break;

                        default:
                            // Display an error for an invalid choice.
                            DialogueManager.DisplayErrorMessage("Неверный выбор. Повторите попытку.");
                            break;
                    }

                    if (exitProgram)
                    {
                        break;
                    }
                }
            }
            catch (FormatException ex)
            {
                DialogueManager.DisplayErrorMessage($"Ошибка формата JSON: {ex.Message}");
            }
            catch (IndexOutOfRangeException)
            {
                DialogueManager.DisplayErrorMessage("Неожиданный конец ввода");
            }
            catch (ArgumentNullException ex)
            {
                DialogueManager.DisplayErrorMessage($"Ошибка аргумента: {ex.ParamName} не должен быть null.");
            }
            catch (ArgumentException ex)
            {
                DialogueManager.DisplayErrorMessage($"Ошибка аргумента: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                DialogueManager.DisplayErrorMessage($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                DialogueManager.DisplayErrorMessage($"Ошибка: {ex.Message}");
            }
        } while (!exitProgram);
    }
}


