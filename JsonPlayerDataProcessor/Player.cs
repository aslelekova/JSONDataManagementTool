namespace JsonPlayerDataProcessor;

/// <summary>
/// Represents a player with various attributes and data.
/// </summary>
public class Player
{
    private int _playerId;
    private string _username;
    private int _level;
    private int _gameScore;
    private List<string> _achievements;
    private List<string> _inventory;
    private string _guild;

    /// <summary>
    /// Gets the unique identifier of the player.
    /// </summary>
    public int PlayerId => _playerId;

    /// <summary>
    /// Gets the username of the player.
    /// </summary>
    public string Username => _username;

    /// <summary>
    /// Gets the level of the player.
    /// </summary>
    public int Level => _level;

    /// <summary>
    /// Gets the game score of the player.
    /// </summary>
    public int GameScore => _gameScore;

    /// <summary>
    /// Gets the list of achievements of the player.
    /// </summary>
    public List<string> Achievements => _achievements;

    /// <summary>
    /// Gets the inventory of the player.
    /// </summary>
    public List<string> Inventory => _inventory;

    /// <summary>
    /// Gets the guild to which the player belongs.
    /// </summary>
    public string Guild => _guild;

    /// <summary>
    /// Default constructor for the Player class.
    /// </summary>
    public Player()
    {
    }

    /// <summary>
    /// Parameterized constructor for the Player class.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <param name="username">The username of the player.</param>
    /// <param name="level">The level of the player.</param>
    /// <param name="gameScore">The game score of the player.</param>
    /// <param name="achievements">The list of achievements of the player.</param>
    /// <param name="inventory">The inventory of the player.</param>
    /// <param name="guild">The guild to which the player belongs.</param>
    public Player(int playerId, string username, int level, int gameScore, List<string> achievements, List<string> inventory, string guild)
    {
        _playerId = playerId;
        _username = username;
        _level = level;
        _gameScore = gameScore;
        _achievements = achievements;
        _inventory = inventory;
        _guild = guild;
    }
}
