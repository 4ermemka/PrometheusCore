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

    public void Update(Player updatedState)
    { 
        Stats = updatedState.Stats;
        HealthBar = updatedState.HealthBar;
        Level = updatedState.Level;
    }

    public override string ToString()
    {
        string stats = Stats.ToString();
        string health = HealthBar.ToString();
        string level = Level.ToString();

        string result = $"{Name}:{Caps}:{stats}:{health}:{level}";
        return result;
    }
}