using JsonPlayerDataProcessor;
namespace HW3._1;

/// <summary>
/// Utility class for sorting and filtering Player objects.
/// </summary>
public class PlayerUtility
{
    /// <summary>
    /// Sorts the list of players based on the chosen field and order.
    /// </summary>
    /// <param name="players">The list of players to be sorted.</param>
    public static void SortPlayers(List<Player> players)
    {
        if (players == null || players.Count == 0)
        {
            Console.WriteLine("Список игроков пуст или равен null. Сортировка отменена.");
            return;
        }
        
        string fieldChoice = DialogueManager.GetMenuChoice("Выберите поле для сортировки:",
            new List<string> { "Player ID", "Username", "Level", "Game Score", "Guild" });
        
        string orderChoice = DialogueManager.GetMenuChoice("Выберите порядок сортировки:",
            new List<string> { "По возрастанию", "По убыванию" });

        // Determine if the order is descending.
        bool descendingOrder = (orderChoice == "2");

        // Perform sorting based on the chosen field.
        switch (fieldChoice)
        {
            case "1":
                players = SortByField(players, p => p.PlayerId, descendingOrder);
                break;
            case "2":
                players = SortByField(players, p => p.Username, descendingOrder);
                break;
            case "3":
                players = SortByField(players, p => p.Level, descendingOrder);
                break;
            case "4":
                players = SortByField(players, p => p.GameScore, descendingOrder);
                break;
            case "5":
                players = SortByField(players, p => p.Guild, descendingOrder);
                break;
            default:
                Console.WriteLine("Неверный выбор поля. Сортировка отменена.");
                return;
        }
    }

    /// <summary>
    /// Sorts the list of players based on the specified field and order.
    /// </summary>
    /// <typeparam name="T">The type of the field for sorting.</typeparam>
    /// <param name="players">The list of players to be sorted.</param>
    /// <param name="selector">A function to extract the sorting field from a Player object.</param>
    /// <param name="descendingOrder">A flag indicating the sorting order.</param>
    /// <returns>The sorted list of players.</returns>
    static List<Player> SortByField<T>(List<Player> players, Func<Player, T> selector, bool descendingOrder)
    {
        // Check if the players list is null.
        if (players == null)
        {
            Console.WriteLine("Список игроков равен null. Сортировка отменена.");
            return new List<Player>(); 
        }

        // Check if the selector function is provided.
        if (selector == null)
        {
            Console.WriteLine("Функция селектора для сортировки не предоставлена. Сортировка отменена.");
            return new List<Player>(); 
        }
        
        Comparison<Player> comparison;

        if (descendingOrder)
        {
            comparison = (x, y) => Comparer<T>.Default.Compare(selector(y), selector(x));
        }
        else
        {
            comparison = (x, y) => Comparer<T>.Default.Compare(selector(x), selector(y));
        }

        players.Sort(comparison);

        return players;
    }

    /// <summary>
    /// Filters the list of players based on the chosen field and user-provided filter value.
    /// </summary>
    /// <param name="players">The list of players to be filtered.</param>
    /// <returns>The filtered list of players.</returns>
    public static List<Player> FilterPlayers(List<Player> players)
    {
        if (players == null)
        {
            Console.WriteLine("Список игроков равен null. Фильтрация отменена.");
            return new List<Player>(); 
        }
        
        string fieldChoice = DialogueManager.GetMenuChoice("\nВыберите поле для фильтрации:",
            new List<string> { "Player ID", "Username", "Level", "Game Score", "Guild" });
        
        Console.Write("Введите значение для фильтрации: ");
        string filterValue = Console.ReadLine().Trim();
        
        // Check if the input is empty or consists only of whitespace characters.
        if (string.IsNullOrWhiteSpace(filterValue))
        {
            DialogueManager.DisplayErrorMessage("\nВведено пустое значение или пробел. Фильтрация отменена, возвращен пустой список.");
            return new List<Player>();
        }
        
        // Perform filtering based on the chosen field.
        switch (fieldChoice)
        {
            case "1":
                return FilterByField(players, p => p.PlayerId.ToString().Contains(filterValue));

            case "2":
                return FilterByField(players, p => p.Username.Contains(filterValue));

            case "3":
                return FilterByField(players, p => p.Level.ToString().Contains(filterValue));

            case "4":
                return FilterByField(players, p => p.GameScore.ToString().Contains(filterValue));

            case "5":
                return FilterByField(players, p => p.Guild.Contains(filterValue));

            default:
                Console.WriteLine("Неверный выбор. Возвращены неотфильтрованные данные.");
                return players;
        }
    }

    /// <summary>
    /// Filters the list of players based on the specified predicate.
    /// </summary>
    /// <param name="players">The list of players to be filtered.</param>
    /// <param name="predicate">The predicate for filtering.</param>
    /// <returns>The filtered list of players.</returns>
    private static List<Player> FilterByField(List<Player> players, Func<Player, bool> predicate)
    {
        if (players == null)
        {
            Console.WriteLine("Список игроков равен null. Фильтрация отменена.");
            return new List<Player>(); 
        }

        // Check if the predicate function is provided.
        if (predicate == null)
        {
            Console.WriteLine("Функция предиката для фильтрации не предоставлена. Фильтрация отменена.");
            return new List<Player>(); 
        }

        List<Player> filteredPlayers = players.FindAll(new Predicate<Player>(predicate));

        if (filteredPlayers.Count == 0)
        {
            DialogueManager.DisplayErrorMessage("\nДанные не найдены для указанных параметров фильтрации. Возвращен пустой список.");
            return new List<Player>();
        }
        return filteredPlayers;
    }
}