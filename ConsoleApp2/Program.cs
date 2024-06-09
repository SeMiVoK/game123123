using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать, воин!");
        Console.Write("Назови себя: ");
        string playerName = Console.ReadLine();

        Weapon playerWeapon = new Weapon("Полутораручный меч", 20, 100);
        Aid playerAid = new Aid("Зелье здоровья", 10);

        Player player = new Player(playerName, 100, playerWeapon, playerAid);

        Console.WriteLine($"Ваше имя {playerName}!");
        Console.WriteLine($"Вам был ниспослан {playerWeapon.Name} ({playerWeapon.Damage}), а также {playerAid.Name} ({playerAid.HealingPower}hp).");
        Console.WriteLine($"У вас {player.CurrentHealth}hp.\n");

        while (player.CurrentHealth > 0)
        {
            Weapon enemyWeapon = new Weapon("Экскалибур", new Random().Next(5, 16), new Random().Next(20, 101));
            Enemy enemy = new Enemy("Варвар", new Random().Next(30, 61), enemyWeapon);

            Console.WriteLine($"**{player.Name}** встречает врага **{enemy.Name}** (hp: {enemy.CurrentHealth}), у врага на поясе сияет оружие **{enemyWeapon.Name} ({enemyWeapon.Damage})**");

            while (enemy.CurrentHealth > 0 && player.CurrentHealth > 0)
            {
                Console.WriteLine("Что вы будете делать?");
                Console.WriteLine("1. Ударить");
                Console.WriteLine("2. Пропустить ход");
                Console.WriteLine("3. Использовать аптечку");
                Console.Write("> ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        player.Attack(enemy);
                        if (enemy.CurrentHealth > 0)
                        {
                            enemy.Attack(player);
                        }
                        break;
                    case "2":
                        Console.WriteLine("Вы пропустили ход.");
                        break;
                    case "3":
                        player.Heal();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                        continue;
                }

                Console.WriteLine($"У противника {enemy.CurrentHealth}hp, у вас {player.CurrentHealth}hp\n");
            }

            if (player.CurrentHealth > 0)
            {
                player.Points++;
                Console.WriteLine($"Противник **{enemy.Name}** побежден!\n");
            }
        }

        Console.WriteLine($"Game over! You scored {player.Points} points.");
    }
}

class Player
{
    public string Name { get; private set; }
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public Aid Aid { get; private set; }
    public Weapon Weapon { get; private set; }
    public int Points { get; set; }

    public Player(string name, int maxHealth, Weapon weapon, Aid aid)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Weapon = weapon;
        Aid = aid;
        Points = 0;
    }

    public void Heal()
    {
        CurrentHealth = Math.Min(MaxHealth, CurrentHealth + Aid.HealingPower);
        Console.WriteLine($"**{Name}** использовал {Aid.Name}.");
        Console.WriteLine($"У вас {CurrentHealth}hp\n");
    }

    public void Attack(Enemy enemy)
    {
        int damage = Weapon.Damage + new Random().Next(-5, 6);
        enemy.TakeDamage(damage);
        Console.WriteLine($"**{Name}** ударил противника **{enemy.Name}**.");
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Console.WriteLine("Game over!");
        }
    }
}

class Enemy
{
    public string Name { get; private set; }
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public Weapon Weapon { get; private set; }

    public Enemy(string name, int maxHealth, Weapon weapon)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Weapon = weapon;
    }

    public void Attack(Player player)
    {
        int damage = Weapon.Damage + new Random().Next(-5, 6);
        player.TakeDamage(damage);
        Console.WriteLine($"Противник **{Name}** ударил вас!");
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Console.WriteLine($"Противник **{Name}** побежден!");
        }
    }
}

class Aid
{
    public string Name { get; private set; }
    public int HealingPower { get; private set; }

    public Aid(string name, int healingPower)
    {
        Name = name;
        HealingPower = healingPower;
    }
}

class Weapon
{
    public string Name { get; private set; }
    public int Damage { get; private set; }
    public int Durability { get; private set; }

    public Weapon(string name, int damage, int durability)
    {
        Name = name;
        Damage = damage;
        Durability = durability;
    }
}
