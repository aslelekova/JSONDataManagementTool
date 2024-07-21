using System.Text;

namespace JsonPlayerDataProcessor;

/// <summary>
/// A utility class for parsing and serializing JSON data for Player objects.
/// </summary>
public static class JsonParser
{
    /// <summary>
    /// Writes the list of players to either the console or a file, based on the specified parameters.
    /// </summary>
    /// <param name="players">The list of players to write.</param>
    /// <param name="writeToConsole">A flag indicating whether to write to the console.</param>
    /// <param name="filePath">The optional file path if writing to a file.</param>
    /// <exception cref="ArgumentException">Thrown when the provided arguments are not valid.</exception>
    public static void WriteJson(List<Player> players, bool writeToConsole, string filePath = null)
    {
        if (players == null)
        {
            throw new ArgumentNullException(nameof(players), "Список игроков не может быть null.");
        }

        // If the intention is to write to the console.
        if (writeToConsole)
        {
            WriteJsonToConsole(players);
        }
        // If the intention is to write to a file and a valid file path is provided.
        else if (!string.IsNullOrEmpty(filePath))
        {
            WriteJsonToFile(players, filePath);
        }
        // If the arguments are not valid (neither console nor file, or missing file path).
        else
        {
            // Throw an exception indicating invalid arguments for the WriteJson method.
            throw new ArgumentException("Недопустимые аргументы для метода WriteJson.");
        }
    }
        
    /// <summary>
    /// Writes JSON data to the console.
    /// </summary>
    /// <param name="players">The list of players to write.</param>
    /// <exception cref="InvalidOperationException">Thrown when Console.Out is null.</exception>
    private static void WriteJsonToConsole(List<Player> players)
    {
        if (Console.Out == null)
        {
            throw new InvalidOperationException("Console.Out равен null.");
        }
        
        WriteJsonToStream(players, Console.Out);
    }

    /// <summary>
    /// Writes JSON data to a file.
    /// </summary>
    /// <param name="players">The list of players to write.</param>
    /// <param name="filePath">The file path to write the JSON data to.</param>
    /// <exception cref="ArgumentException">Thrown when the file path is null or empty.</exception>
    private static void WriteJsonToFile(List<Player> players, string filePath)
    { 
        if (players == null)
        {
            throw new ArgumentException("Список игроков не должен быть null при записи в файл.");
        }
        
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("Путь к файлу не должен быть пустым при записи в файл.");
        }
        
        using (StreamWriter fileWriter = new StreamWriter(filePath))
        {
            WriteJsonToStream(players, fileWriter);
        }
    }

    /// <summary>
    /// Writes a list of players to the specified TextWriter as a JSON array.
    /// </summary>
    /// <param name="players">The list of players to write.</param>
    /// <param name="writer">The TextWriter to write JSON data to.</param>
    /// <exception cref="ArgumentException">Thrown when the list of players is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the TextWriter is null.</exception>
    private static void WriteJsonToStream(List<Player> players, TextWriter writer)
    {
        if (players == null)
        {
            throw new ArgumentException("Список игроков не должен быть null или пустым при записи в поток.");
        }
        
        if (writer == null)
        {
            throw new ArgumentNullException(nameof(writer), "TextWriter не должен быть null.");
        }
        
        // Writes the opening bracket for the JSON array.
        writer.WriteLine("[");
            
        // Iterates through the list of players to write each player to the stream.
        for (int i = 0; i < players.Count; i++)
        {
            WritePlayerToStream(players[i], writer);

            // Writes a comma if there are more players to follow, or a newline if it's the last player.
            if (i < players.Count - 1)
            {
                writer.WriteLine(",");
            }
            else
            {
                writer.WriteLine();
            }
        }
            
        // Writes the closing bracket for the JSON array.
        writer.WriteLine("]");
    }

    /// <summary>
    /// Writes a Player object to the specified TextWriter as a JSON object.
    /// </summary>
    /// <param name="player">The Player object to write.</param>
    /// <param name="writer">The TextWriter to write JSON data to.</param>
    /// <exception cref="ArgumentNullException">Thrown when the Player object is null.</exception>
    private static void WritePlayerToStream(Player player, TextWriter writer)
    {
        if (player == null)
        {
            throw new ArgumentNullException(nameof(player), "Объект Player не должен быть null при записи в поток.");
        }

        writer.WriteLine("  {");
        WriteJsonProperty("player_id", player.PlayerId, writer);
        WriteJsonProperty("username", player.Username, writer);
        WriteJsonProperty("level", player.Level, writer);
        WriteJsonProperty("game_score", player.GameScore, writer);
        WriteJsonProperty("achievements", player.Achievements, writer);
        WriteJsonProperty("inventory", player.Inventory, writer);
        WriteJsonProperty("guild", player.Guild, writer, isLastProperty: true); // Последний элемент, не добавляем запятую
        writer.Write("  }");
    }

    /// <summary>
    /// Writes a JSON property to the specified TextWriter.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="key">The property key.</param>
    /// <param name="value">The property value.</param>
    /// <param name="writer">The TextWriter to write JSON data to.</param>
    /// <param name="isLastProperty">If the property is last.</param>
    /// <exception cref="ArgumentNullException">Thrown when the key is null.</exception>
    private static void WriteJsonProperty<T>(string key, T value, TextWriter writer, bool isLastProperty = false)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key), "Ключ не должен быть null при записи в поток.");
        }
        
        // Writes the property key with double quotes.
        writer.Write($"    \"{key}\": ");
            
        if (value is List<string>)
        {
            // Casts the value to a List<string>.
            List<string> stringList = value as List<string>;
                
            if (stringList == null)
            {
                throw new ArgumentNullException(nameof(value), "Значение не должно быть null при записи в поток.");
            }
            
            writer.Write("[\n");
                
            // Iterates through the list of strings, writing each one with proper formatting.
            for (int i = 0; i < stringList.Count; i++)
            {
                writer.Write($"      \"{stringList[i]}\"");
                    
                // Writes a comma if there are more strings to follow, or a newline if it's the last string.
                if (i < stringList.Count - 1)
                {
                    writer.Write(",");
                }
                writer.Write("\n");
            }
                
            // Writes the closing bracket for the JSON array.
            writer.Write("    ],\n");
        }
        else
        {
            writer.WriteLine($"{SerializeValue(value)}{(isLastProperty ? string.Empty : ",")}");
        }
    }

    /// <summary>
    /// Serializes a value to its JSON representation.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to serialize.</param>
    /// <returns>The JSON representation of the value.</returns>
    private static string SerializeValue<T>(T value)
    {
        if (value == null)
        {
            // Throws a FormatException if the value is unexpectedly null inside a JSON object.
            throw new FormatException($"{nameof(ParsePlayer)}: Неожиданные данные объекта JSON");
        }
            
        if (value is string)
        { 
            // Returns the value enclosed in double quotes to represent a JSON string.
            return $"\"{value}\"";
        }
        else if (value is List<string>)
        {
            List<string> stringList = value as List<string>;
                
            // Returns the serialized representation of the list of strings, joining them with commas.
            return $"[ {string.Join(", ", stringList.Select(s => $"\"{s}\""))} ]";
        }
        else
        {
            return value.ToString();
        }
    }
        
    /// <summary>
    /// Reads JSON data from either the console or a file, based on the specified flag.
    /// </summary>
    /// <param name="isFromFile">A flag indicating whether to read from a file.</param>
    /// <returns>The JSON data as a string.</returns>
    public static string ReadJson(bool isFromFile)
    {
        if (isFromFile)
        {
            Console.Write("Введите путь к файлу JSON: ");
                
            string filePath = Console.ReadLine().Trim();
            Console.WriteLine();
                
            // Create a data reader for reading from the file.
            IDataReader dataReader = new FileReader(filePath);
            return dataReader.ReadData();
        }
        else
        {
            // Create a data reader for reading from the console.
            IDataReader dataReader = new ConsoleDataReader();
                
            return dataReader.ReadData();
        }
    }
        
    /// <summary>
    /// Parses a JSON string to a list of Player objects.
    /// </summary>
    /// <param name="json">The JSON data as a string.</param>
    /// <returns>A list of Player objects.</returns>
    /// <exception cref="ArgumentException">Thrown when the input JSON string is null, empty, or contains only whitespace characters.</exception>
    /// <exception cref="FormatException">Thrown when the input JSON string does not represent a valid JSON array.</exception>
    public static List<Player> ParsePlayers(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("Входная строка JSON не должна быть пустой или состоять только из пробелов.", nameof(json));
        }

        int position = 0;

        SkipWhitespace(json, ref position);

        // Check if the JSON string is empty or doesn't start with an opening square bracket '['.
        if (position >= json.Length || json[position] != '[')
            throw new FormatException("Ожидался непустой JSON-массив. Перезапуск программы.");

        // Move past the opening square bracket '['.
        position++;

        List<Player> players = new List<Player>();

        // Iterate through the JSON array until the closing square bracket ']' is encountered.
        while (position < json.Length && json[position] != ']')
        {
            // Skip any whitespaces between array elements.
            SkipWhitespace(json, ref position);
                
            players.Add(ParsePlayer(json, ref position));
                
            SkipWhitespace(json, ref position);

            // Check for a comma ',' indicating the presence of another array element.
            if (position < json.Length && json[position] == ',')
                position++;
        }

        // Check if the array is closed properly with a closing square bracket ']'.
        if (position >= json.Length || json[position] != ']')
            throw new FormatException("Некорректные данные объекта JSON. Перезапуск программы.");

        // Move past the closing square bracket ']'.
        position++; 

        // Return the list of parsed Player objects.
        return players;
    }
        
    /// <summary>
    /// Parses a JSON string to a Player object.
    /// </summary>
    /// <param name="json">The JSON data as a string.</param>
    /// <param name="position">The current position in the JSON string. Updated to the position after the parsed object.</param>
    /// <returns>A Player object with parsed values.</returns>
    /// <exception cref="ArgumentException">Thrown when the input JSON string is null, empty, or contains only whitespace characters.</exception>
    /// <exception cref="FormatException">Thrown when the input JSON string does not represent a valid JSON object or contains duplicate keys.</exception>
    private static Player ParsePlayer(string json, ref int position)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("Входная строка JSON не должна быть пустой или состоять только из пробелов.", nameof(json));
        }
        
        // Check if the JSON object starts with an opening curly brace '{'.
        if (position >= json.Length || json[position] != '{')
            throw new FormatException("Некорректные данные объекта JSON. Перезапуск программы.");

        // Move past the opening curly brace '{'.
        position++;

        int playerId = 0;
        string username = null;
        int level = 0;
        int gameScore = 0;
        List<string> achievements = null;
        List<string> inventory = null;
        string guild = null;

        // Sets to track used and duplicate keys.
        HashSet<string> usedKeys = new HashSet<string>();
        HashSet<string> duplicateKeys = new HashSet<string>();

        while (position < json.Length && json[position] != '}')
        {
            // Skip any whitespaces between key-value pairs.
            SkipWhitespace(json, ref position);

            // Check for unexpected end of input inside the JSON object.
            if (position >= json.Length || json[position] == '}')
                throw new FormatException("Некорректные данные объекта JSON. Перезапуск программы.");

            string key = ParseString(json, ref position);
            SkipWhitespace(json, ref position);

            // Check for duplicate keys and collect them.
            if (!usedKeys.Add(key) && !duplicateKeys.Contains(key))
            {
                duplicateKeys.Add(key);
            }

            // Check for the expected ':' after a key in a JSON object.
            if (position >= json.Length || json[position] != ':')
                throw new FormatException("Некорректые данные объекта JSON. Перезапуск программы.");

            // Move past the ':'.
            position++;

            SkipWhitespace(json, ref position);

            // Parse and assign values based on the key.
            switch (key)
            {
                case "player_id":
                    playerId = ParseNumber(json, ref position);
                    break;
                case "username":
                    username = ParseString(json, ref position);
                    break;
                case "level":
                    level = ParseNumber(json, ref position);
                    break;
                case "game_score":
                    gameScore = ParseNumber(json, ref position);
                    break;
                case "achievements":
                    achievements = ParseStringArray(json, ref position);
                    break;
                case "inventory":
                    inventory = ParseStringArray(json, ref position);
                    break;
                case "guild":
                    guild = ParseString(json, ref position);
                    break;
                default:
                    throw new ArgumentException($"Неизвестные данные: {key}", nameof(json));
            }

            SkipWhitespace(json, ref position);

            // Check for a comma ',' indicating the presence of another key-value pair.
            if (position < json.Length && json[position] == ',')
                position++;
        }

        // Check if the JSON object is closed properly with a closing curly brace '}'.
        if (position >= json.Length || json[position] != '}')
            throw new FormatException("Некорректные данные объекта JSON. Перезапуск программы.");

        // Move past the closing curly brace '}'.
        position++; 

        // Check for any duplicate keys collected during parsing.
        if (duplicateKeys.Count > 0)
        {
            throw new FormatException($"Повторяющиеся данные: {string.Join(", ", duplicateKeys)}");
        }

        // Return a new Player object with the parsed values.
        return new Player(playerId, username, level, gameScore, achievements, inventory, guild);
    }
        
    /// <summary>
    /// Parses a JSON string to a string array.
    /// </summary>
    /// <param name="json">The JSON data as a string.</param>
    /// <param name="position">The current position in the string.</param>
    /// <returns>The parsed string array.</returns>
    /// <exception cref="ArgumentException">Thrown if the input JSON string is null, empty, or contains only whitespaces.</exception>
    /// <exception cref="FormatException">Thrown if the JSON array is expected to start with an opening square bracket '[' but doesn't.</exception>
    /// <exception cref="FormatException">Thrown if unexpected null value is encountered in the JSON array.</exception>
    private static List<string> ParseStringArray(string json, ref int position)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("Входная строка JSON не должна быть пустой или состоять только из пробелов.", nameof(json));
        }

        // Check if the JSON array starts with an opening square bracket '['.
        if (position >= json.Length || json[position] != '[')
            throw new FormatException("Ожидается '[' в начале JSON массива. Перезапуск программы.");

        // Move past the opening square bracket '['.
        position++;

        List<string> result = new List<string>();

        // Check for an empty array.
        if (position < json.Length && json[position] == ']')
        {
            position++; // Move past the closing square bracket ']'.
            return result; // Return an empty list for an empty array.
        }

        while (position < json.Length && json[position] != ']')
        {
            // Skip any whitespaces between array elements.
            SkipWhitespace(json, ref position);

            result.Add(ParseString(json, ref position) ?? throw new FormatException("Неожиданное значение null в JSON-массиве"));

            SkipWhitespace(json, ref position);

            // Check for a comma ',' indicating the presence of another array element.
            if (position < json.Length && json[position] == ',')
                position++;
        }

        // Check if the JSON array is closed properly with a closing square bracket ']'.
        if (position >= json.Length || json[position] != ']')
            throw new FormatException("Некорректные данные объекта JSON. Перезапуск программы.");

        // Move past the closing square bracket ']'.
        position++;

        return result;
    }


    /// <summary>
    /// Parses a JSON string value enclosed in double quotes.
    /// </summary>
    /// <param name="json">The JSON data as a string.</param>
    /// <param name="position">The current position in the JSON string. Updated to the position after the parsed string.</param>
    /// <returns>The parsed string value.</returns>
    /// <exception cref="ArgumentException">Thrown when the input JSON string is null, empty, or contains only whitespace characters.</exception>
    /// <exception cref="FormatException">Thrown when the input JSON string does not represent a valid JSON string or is missing the closing double quote.</exception>
    private static string ParseString(string json, ref int position)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("Входная строка JSON не должна быть пустой или состоять только из пробелов.", nameof(json));
        }
        
        // Check if the JSON string starts with a double quote '"'.
        if (position >= json.Length || json[position] != '"')
            throw new FormatException("Некорректные данные объекта JSON. Перезапуск программы.");

        // Move past the opening double quote '"'.
        position++;

        StringBuilder builder = new StringBuilder();

        while (position < json.Length && json[position] != '"')
        {
            // Append the current character to the StringBuilder.
            builder.Append(json[position]);
            position++;

            // Check for unexpected end of input inside the JSON string.
            if (position >= json.Length)
                throw new FormatException("Некорректные данные объекта JSON. Перезапуск программы.");
        }

        // Check if the JSON string is closed properly with a closing double quote '"'.
        if (position >= json.Length || json[position] != '"')
            throw new FormatException("Некорректные данные объекта JSON. Перезапуск программы.");

        position++;

        return builder.ToString();
    }

    /// <summary>
    /// Parses a JSON numeric value from the current position in the JSON string.
    /// </summary>
    /// <param name="json">The JSON data as a string.</param>
    /// <param name="position">The current position in the JSON string. Updated to the position after the parsed number.</param>
    /// <returns>The parsed numeric value.</returns>
    /// <exception cref="ArgumentException">Thrown when the input JSON string is null, empty, or contains only whitespace characters.</exception>
    private static int ParseNumber(string json, ref int position)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("Входная строка JSON не должна быть пустой или состоять только из пробелов.", nameof(json));
        }
        
        StringBuilder builder = new StringBuilder();

        while (position < json.Length && char.IsDigit(json[position]))
        {
            // Append the current digit to the StringBuilder.
            builder.Append(json[position]);
            position++;

            // Break if the end of the JSON string is reached.
            if (position >= json.Length)
                break;
        }

        int.TryParse(builder.ToString(), out int result);
        return result;
    }

    /// <summary>
    /// Skips whitespace characters from the current position in the JSON string.
    /// </summary>
    /// <param name="json">The JSON data as a string.</param>
    /// <param name="position">The current position in the JSON string. Updated to the position after skipping whitespace.</param>
    /// <exception cref="ArgumentException">Thrown when the input JSON string is null, empty, or contains only whitespace characters.</exception>
    private static void SkipWhitespace(string json, ref int position)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("Входная строка JSON не должна быть пустой или состоять только из пробелов.", nameof(json));
        }
        
        while (position < json.Length && char.IsWhiteSpace(json[position]))
            position++;
    }
}