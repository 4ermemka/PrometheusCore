public class Player
{
    public string Name;
    public int Caps;
    
    public Stats Stats;
    public HealthBar HealthBar;
    public Level Level;

    public Player(string name, int caps, Stats stats, int maxHealth)
    {
        Name = name;
        Caps = caps;
        Stats = stats;
        HealthBar = new HealthBar(maxHealth);
        Level = new Level();
    }

    public Player(string name, int caps, Stats stats, HealthBar healthBar, Level level)
    {
        Name = name;
        Caps = caps;
        Stats = stats;
        HealthBar = healthBar;
        Level = level;
    }
}