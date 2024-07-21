/// <summary>
/// Represents a data reader that reads data from a file.
/// </summary>
public class FileReader : IDataReader
{
    private readonly string filePath;

    /// <summary>
    /// Initializes a new instance of the FileReader class with the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the file to be read.</param>
    public FileReader(string filePath)
    {
        // Validate that the provided file path is not null or empty.
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("Файл не должен быть пустым.", nameof(filePath));
        }

        // Use a loop to repeatedly prompt for a correct file path.
        while (!File.Exists(filePath))
        {
            Console.WriteLine($"Файл не найден: {filePath}");
            Console.Write("Введите корректный путь к файлу: ");
            filePath = Console.ReadLine().Trim();
            Console.WriteLine();
        }

        this.filePath = filePath;
    }

    /// <summary>
    /// Reads data from the specified file and returns it as a string.
    /// </summary>
    /// <returns>The read data as a string.</returns>
    public string ReadData()
    {
        try
        {
            // Save the current standard input stream.
            TextReader originalInput = Console.In;

            // Redirect the standard input stream to the file.
            using (StreamReader fileReader = new StreamReader(filePath))
            {
                Console.SetIn(fileReader);

                // Read data from the file; now it will be read from Console.In.
                return Console.In.ReadToEnd();
            }
        }
        catch (IOException ex)
        {
            throw new IOException($"Ошибка при чтении файла: {ex.Message}");
        }
        finally
        {
            // Restore the original standard input stream.
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
    }
}