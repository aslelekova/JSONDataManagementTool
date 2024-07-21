using System;
using System.Text;

/// <summary>
/// Implementation of the IDataReader interface for reading JSON data from the console.
/// </summary>
public class ConsoleDataReader : IDataReader
{
    /// <summary>
    /// Reads JSON data from the console.
    /// </summary>
    /// <returns>A string containing the JSON data.</returns>
    public string ReadData()
    {
        Console.WriteLine("Введите данные JSON (введите 'finish' для завершения ввода):");

        StringBuilder jsonBuilder = new StringBuilder();
        string line;

        do
        {
            line = Console.ReadLine();

            // Append non-empty lines to the StringBuilder.
            if (!string.IsNullOrWhiteSpace(line))
            {
                jsonBuilder.AppendLine(line);
            }

        } while (!string.Equals(line, "finish", StringComparison.OrdinalIgnoreCase));

        return jsonBuilder.ToString().Trim();
    }
}