using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{
    // 4 босса побеждены - игра закончена 

    public class Game
    {
        private Player theplayer = new Player();
        private Monster themonster;
        private int numberBosses = 4;
        private int numberMonsters = 3; // специально поменяла!!! если меняем на 10, менять в 71 строке тоже
        RandomChoice randoming = new RandomChoice();

        public bool endGame = false;

        private List<Monster> bosses = new List<Monster>()
    {
        new Goblin("БОСС:"),
        new Skeleton("БОСС:"),
        new Magician("БОСС:"),
        new SkeletonPestovC()
    };

        private List<Armour> armours = new List<Armour>()
    {
        new Armour("Магические доспехи", 40),
        new Armour("Механические доспехи", 30),
        new Armour("Латные доспехи", 25),
        new Armour("Кольчужные доспехи", 20),
        new Armour("Тканевые доспехи", 15),
        new Armour("Кожаные доспехи", 10)
    };

        private List<Weapon> weapons = new List<Weapon>()
    {
        new Weapon("Меч", 20),
        new Weapon("Молот", 10),
        new Weapon("Арбалет", 40),
        new Weapon("Копьё", 30),
        new Weapon("Лук", 35),
        new Weapon("Ножи", 12)
    };



        public bool HodIgry()
        {
            bool gaming = true;

            while (gaming)
            {
                bool choosingHod = randoming.GetRandomBoolean();
                if (choosingHod)
                {
                    Fight();
                }
                else
                {
                    Chest();
                }
                gaming = !(numberBosses == 0 || theplayer.IsDead());
            }

            return gaming;
        }

        public void Fight()
        {
            bool theMonsterIsAlive = true;

            if (numberMonsters > 0)
            {
                int vvv = randoming.GetRandomInteger012();

                switch (vvv)
                {
                    case 0: themonster = new Goblin(); break;
                    case 1: themonster = new Magician(); break;
                    case 2: themonster = new Skeleton(); break;

                }
                numberMonsters--;
            }
            else if (numberBosses > 0)
            {
                int numberInList = randoming.GetRandomInt(bosses.Count);
                themonster = bosses[numberInList];
                bosses.RemoveAt(numberInList);
                numberBosses--;
                numberMonsters = 3;
            }

            while (theMonsterIsAlive)
            {
                theplayer.printInfo();
                themonster.printInfo();

                bool result = false;

                if (theplayer.isFreezed())
                {
                    result = true;
                    Console.WriteLine("ВЫ ЗАМОРОЖЕНЫ. ВЫ ПРОПУСКАЕТЕ ХОД!");
                    theplayer.healFromFreezing();
                }

                int choice = 3;

                while (!result)
                {
                    Console.WriteLine("ВЫБЕРИТЕ: 1 - защита, 2 - атака");
                    result = int.TryParse(Console.ReadLine(), out choice);
                    if (choice != 1 && choice != 2)
                    {
                        result = false;
                        Console.WriteLine("НЕКОРРЕКТНЫЙ ВВОД");
                    }
                }

                bool avoidance = false;
                bool attack = false;
                int damage = themonster.Attack();
                bool avoidingDefense = themonster.avoidingDefence();

                if (choice == 1) // ЗАЩИТА
                {
                    avoidance = randoming.ChanceFortyPercent();
                    if (!avoidance)
                    {
                        damage = damage - (int)(damage * randoming.GetRandomDoubleInRange70());
                        if (damage < 0) { damage = 0; }
                    }
                }
                else if (choice == 2) // АТАКА
                {
                    attack = true;
                }

                if (!avoidance && !themonster.IsDead() && !avoidingDefense)
                {
                    Console.WriteLine("Атакует!");
                    theplayer.GetDamage(damage);
                }
                else if (!avoidance && !themonster.IsDead() && avoidingDefense)
                {
                    Console.WriteLine("Атакует!");
                    theplayer.GetDamageAvoidingDefence(damage);
                }
                else
                {
                    Console.WriteLine("НЕ Атакует!");
                }

                if (themonster.freezingSpell())
                {
                    theplayer.getFreezed();
                }

                if (attack)
                {
                    themonster.GetDamage(theplayer.Attack());
                }


                // ЗАВЕРШЕНИЯ ЦИКЛА
                if (themonster.IsDead())
                {
                    theMonsterIsAlive = false;
                    Console.WriteLine("МОНСТР ПОБЕЖДЕН!");
                }

                if (theplayer.IsDead())
                {
                    Console.WriteLine("ВЫ УБИТЫ!");
                    break;
                }

            }


        }

        public void Chest()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine("Вам выпал сундук!");
            int secret = randoming.GetRandomInteger012();

            switch (secret)
            {
                case 0:
                    Console.WriteLine("ЛЕЧЕБНОЕ ЗЕЛЬЕ!");
                    theplayer.GetHealing();
                    break;
                case 1:
                    Console.WriteLine("НОВЫЕ ДОСПЕХИ!");
                    int numberInArmourList = randoming.GetRandomInt(armours.Count);
                    Armour newArmour = armours[numberInArmourList];
                    newArmour.printInfo();

                    bool result = false;
                    int choice = 3;

                    while (!result)
                    {
                        Console.WriteLine("БЕРЕТЕ НОВЫЕ ДОСПЕХИ?");
                        Console.WriteLine("1 - ДА!!! ЧТО ЗА ВОПРОС ВООБЩЕ??, 2 - НЕТ, осталяю старые, УЖ СЛИШКОМ РОДНЫЕ");
                        result = int.TryParse(Console.ReadLine(), out choice);
                        if (choice != 1 && choice != 2)
                        {
                            result = false;
                            Console.WriteLine("НЕКОРРЕКТНЫЙ ВВОД");
                        }
                    }

                    if (choice == 1)
                    {
                        theplayer.GetNewArmour(newArmour);
                    }

                    break;
                case 2:
                    Console.WriteLine("НОВОЕ ОРУЖИЕ!");
                    int numberInWeaponList = randoming.GetRandomInt(weapons.Count);
                    Weapon newWeapon = weapons[numberInWeaponList];
                    newWeapon.printInfo();

                    bool result2 = false;
                    int choice2 = 3;

                    while (!result2)
                    {
                        Console.WriteLine("БЕРЕТЕ НОВОЕ ОРУЖИЕ?");
                        Console.WriteLine("1 - ДА!!! ЧТО ЗА ВОПРОС ВООБЩЕ??, 2 - НЕТ, осталяю старое, УЖ СЛИШКОМ РОДНОЕ");
                        result2 = int.TryParse(Console.ReadLine(), out choice2);
                        if (choice2 != 1 && choice2 != 2)
                        {
                            result2 = false;
                            Console.WriteLine("НЕКОРРЕКТНЫЙ ВВОД");
                        }
                    }

                    if (choice2 == 1)
                    {
                        theplayer.GetNewWeapon(newWeapon);
                    }

                    break;
            }
            Console.ResetColor();
        }
    }
}
