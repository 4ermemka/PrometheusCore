using Newtonsoft.Json;
using System;

[Serializable]
public class Player
{
    #region Events
    [JsonIgnore]
    public Action<Player> OnPlayerUpdated { get; set; }

    #region Effects
    [JsonIgnore]
    public Action<Buff> OnBuffAdded;
    [JsonIgnore]
    public Action<Buff> OnBuffUpdated;
    [JsonIgnore]
    public Action<Buff> OnBuffRemoved;

    [JsonIgnore]
    public Action<Buff> OnDebuffAdded;
    [JsonIgnore]
    public Action<Buff> OnDebuffUpdated;
    [JsonIgnore]
    public Action<Buff> OnDebuffRemoved;

    [JsonIgnore]
    public Action<Injury> OnInjuryAdded;
    [JsonIgnore]
    public Action<Injury> OnInjuryUpdated;
    [JsonIgnore]
    public Action<Injury> OnInjuryRemoved;
    #endregion

    #region HealthBar
    [JsonIgnore]
    public Action<int, int> OnHeal;
    [JsonIgnore]
    public Action<int, int> OnDamage;
    [JsonIgnore]
    public Action<int, int, int> OnDeath;

    [JsonIgnore]
    public Action<int, int> OnRadiationIncreased;
    [JsonIgnore]
    public Action<int, int> OnRadiationDecreased;
    #endregion

    #region Body

    [JsonIgnore]
    public Action<Part> OnBodyPartHealed;
    [JsonIgnore]
    public Action<Part> OnBodyPartDamaged;

    #endregion

    #endregion

    public string Name { get; set; }
    public int Caps { get; set; }

    public Body Body { get; set; }
    public Stats Stats { get; set; }
    public HealthBar HealthBar { get; set; }
    public Level Level { get; set; }

    public Player(string name, int caps, int maxHealth, Stats stats = null, Body body = null, Level level = null, HealthBar healthBar = null)
    {
        Name = name;
        Caps = caps;

        if (stats != null)
        {
            Stats = stats;
        }
        else
        {
            Stats = new Stats(0, 0, 0, 0, 0, 0, 0);
        }

        if (healthBar != null)
        {
            HealthBar = healthBar;
        }
        else
        {
            HealthBar = new HealthBar(maxHealth);
        }

        if (level != null)
        {
            Level = level;
        }
        else
        {
            Level = new Level();
        }

        if (body != null)
        {
            Body = body;
        }
        else
        {
            Body = new Body();
        }
        SubscribeToInner();
        SubscribeToOuter();
    }

    public Player(string jsonString)
    {
        Body = new Body();
        Stats = new Stats();
        HealthBar = new HealthBar();
        Level = new Level();

        Player updatedState = JsonConvert.DeserializeObject<Player>(jsonString);
        Update(updatedState);
    }

    public void Update(Player updatedState)
    {
        Stats.UpdateState(updatedState.Stats);
        HealthBar.UpdateState(updatedState.HealthBar);
        Level.UpdateState(updatedState.Level);
    }

    private void SubscribeToInner()
    {
        Stats.OnBuffAdded += OnBuffAdded;
        Stats.OnBuffUpdated += OnBuffUpdated;
        Stats.OnBuffRemoved += OnBuffRemoved;

        Stats.OnDebuffAdded += OnDebuffAdded;
        Stats.OnDebuffUpdated += OnDebuffUpdated;
        Stats.OnDebuffRemoved += OnDebuffRemoved;

        Stats.OnInjuryAdded += OnInjuryAdded;
        Stats.OnInjuryUpdated += OnInjuryUpdated;
        Stats.OnInjuryRemoved += OnInjuryRemoved;

        HealthBar.OnHeal += OnHeal;
        HealthBar.OnDamage += OnDamage;
        HealthBar.OnDeath += OnDeath;
        HealthBar.OnRadiationIncreased += OnRadiationIncreased;
        HealthBar.OnRadiationDecreased += OnRadiationDecreased;

        Body.OnBodyPartHealed += OnBodyPartHealed;
        Body.OnBodyPartDamaged += OnBodyPartDamaged;
    }

    private void SubscribeToOuter()
    {
        OnBuffAdded += new Action<Buff>((_) => OnPlayerUpdated?.Invoke(this));
        OnBuffUpdated += new Action<Buff>((_) => OnPlayerUpdated?.Invoke(this));
        OnBuffRemoved += new Action<Buff>((_) => OnPlayerUpdated?.Invoke(this));

        Stats.OnDebuffAdded += new Action<Buff>((_) => OnPlayerUpdated?.Invoke(this));
        Stats.OnDebuffUpdated += new Action<Buff>((_) => OnPlayerUpdated?.Invoke(this));
        Stats.OnDebuffRemoved += new Action<Buff>((_) => OnPlayerUpdated?.Invoke(this));

        Stats.OnInjuryAdded += new Action<Injury>((_) => OnPlayerUpdated?.Invoke(this));
        Stats.OnInjuryUpdated += new Action<Injury>((_) => OnPlayerUpdated?.Invoke(this));
        Stats.OnInjuryRemoved += new Action<Injury>((_) => OnPlayerUpdated?.Invoke(this));

        HealthBar.OnHeal += new Action<int, int>((_, __) => OnPlayerUpdated?.Invoke(this));
        HealthBar.OnDamage += new Action<int, int>((_, __) => OnPlayerUpdated?.Invoke(this));
        HealthBar.OnDeath += new Action<int, int, int>((_, __, ___) => OnPlayerUpdated?.Invoke(this));
        HealthBar.OnRadiationIncreased += new Action<int, int>((_, __) => OnPlayerUpdated?.Invoke(this));
        HealthBar.OnRadiationDecreased += new Action<int, int>((_, __) => OnPlayerUpdated?.Invoke(this));

        Body.OnBodyPartHealed += new Action<Part>((_) => OnPlayerUpdated?.Invoke(this));
        Body.OnBodyPartDamaged += new Action<Part>((_) => OnPlayerUpdated?.Invoke(this));
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}