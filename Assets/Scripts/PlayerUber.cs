﻿using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlayerUber {

    #region stats

    //maximum health points
    public int maxhp;
    //current health points
    public int hp;
    //magic points
    public int mp;
    //attack points
    public int ap;
    //defense points
    public int dp;
    //speed points
    public int sp;
    //level 
    public int lv;
    //current experience points  
    public int exp;
    //level maximum exp
    public int maxExp;
    //money
    public int money = 0;
    // Turns that the player is to skip
    public int turnsToSkip = 0;
    // Is this player currently on a fight?
    public bool onDuty = false;
    //fans
    public int fans = 0;
    //Gender
    public int gender;
    //Items, HashTable de la manera:  (LLave, Valor) = (itemNumber, Instancias de ese item)
    public PlayerItems items;
    //Equipment: Para referencia vease la clase PlayerEquipment
    public PlayerEquipment playerEquipment = new PlayerEquipment();
    //Magia con la que empieza el jugador
    public Magica magica;
    //An array that has the perks to be used on the fight(HP,MP,AP,DP,SP)
    public int[] perkArray = { 0, 0, 0, 0, 0 };
    //LEVEL es el objeto que se encarga de calcular los subidones en el subido de nivel. 
    public LV level;

    #endregion

    #region level

    public void LevelUp()
    {
        ApplyLevelUp(this.level.LevelUP(this.lv));
        this.exp = 0;
        this.maxExp = level.MaxExp(this.maxExp);
        this.lv++;
    }

    public int NumLVUP(int expplus)
    {
        if (expplus==-1)
        {
            return 1;
        }
        int localexp = this.exp+expplus;
        int levelcounter = 0;
        int tempmax = this.maxExp;
        while (localexp>=tempmax)
        {
            localexp = localexp - tempmax;
            levelcounter++;
            tempmax = level.MaxExp(tempmax);
        }
        return levelcounter;
    }

    public void ApplyLevelUp(int[] arr)
    {
        this.hp += arr[0] * 5;
        this.mp += arr[1];
        this.ap += arr[2];
        this.dp += arr[3];
        this.sp += arr[4];
    }

    public float ExpPercentage()
    {
        float lol = (float)this.exp / this.maxExp;
        //Debug.Log(lol);
        return lol;
    }

    #endregion

    #region Health

    //moreLess; 0 less, an attack. 1 more, an Item or heal
    public void RecalculateHealth(int moreLess, int hpChange)
    {
        if (moreLess == 0)
        {
            this.hp -= hpChange;
        }
        else
        {
            this.hp += hpChange;
        }

    }

    public float HPPercentage()
    {
        float lol = (float)this.hp / this.maxhp;
        //Debug.Log(lol);
        return lol;
    }

    #endregion

    #region Magic
    //To change magic
    //0:"Basic Magic",1:"Fire",2:"Blizzard",3:"Shock",4:"Natura"
    //0:"",1:"",2:"Big ",3:"Death ",4:"God "
	//new magic
    public void ChangeMagic(String magic)
    {
        int mlevel, mtype;
        if (magic.Contains("Fire"))
        {
            mtype = 1;
        }
        else if (magic.Contains("Blizzard"))
        {
            mtype = 2;
        }
        else if (magic.Contains("Shock"))
        {
            mtype = 3;
        }
        else
        {
            mtype = 4;
        }

        if (magic.Contains("Big"))
        {
            mlevel = 2;
        }
        else if (magic.Contains("Death"))
        {
            mlevel = 3;
        }
        else if (magic.Contains("God"))
        {
            mlevel = 4;
        }
        else
        {
            mlevel = 1;
        }
        magica.UpdateMagic(mlevel, mtype);
    }

    #endregion

    #region Items

    //Method to use item A.K.A. remove item from bag
    // ONLY FOR HP, for other perks please use perks.
    public int UseItem(int itemNo)
    {
        items.UseItem(itemNo);
        return Math.Abs(items.GetItem(itemNo).perks[0]);
    }

    //Method to add items to the bag.
    public void NewItem(int itemNo, int nummer)
    {
        items.NewItem(itemNo, nummer);
    }

    //To list my Items on the Inventory
    public ItemRPG[] GetItems() {
        return items.GetMyItems();
    }

    public string GetItemName(int itmo)
    {
        return items.GetItem(itmo).name;
    }

	#endregion

	#region Equipment

	//Method to equip equipment 
	//part: (1-Head | 2-Hands | 3-Torso | 4-Feet);
	///new equ
	public void Equip(int part, int equip)
    {
        playerEquipment.EquipEquipment(part, equip);
        ApplyEquipment(part, equip, 0);
    }

    //Method to unequip equipment 
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    public void UnEquip(int part, int equip)
    {
        playerEquipment.UnEquipEquipment(part);
        ApplyEquipment(part, equip, 1);
    }

    //Unequip 1 true, 0 false
    public void ApplyEquipment(int part, int equip, int unequip)
    {
        //Overall order is (HP,MP,AP,DP,SP,NUMBER)
        int[] tempPerks = playerEquipment.GetEquipmentPerks(part, equip);
        if (unequip == 0)
        {
            this.hp += tempPerks[0];
            this.mp += tempPerks[1];
            this.ap += tempPerks[2];
            this.dp += tempPerks[3];
            this.sp += tempPerks[4];
        }
        else
        {
            this.hp -= tempPerks[0];
            this.mp -= tempPerks[1];
            this.ap -= tempPerks[2];
            this.dp -= tempPerks[3];
            this.sp -= tempPerks[4];
        }

    }

    #endregion

    #region Perk

    //0- Begin, 1- End
    public void ApplyPerk(int beginEnd)
    {
        //(HP,MP,AP,DP,SP,number)
        //(HP,MP,AP,DP,SP)
        if (beginEnd == 0)
        {
            this.hp += perkArray[0];
            this.mp += perkArray[1];
            this.ap += perkArray[2];
            this.dp += perkArray[3];
            this.sp += perkArray[4];
        }
        else
        {
            this.hp -= perkArray[0];
            this.mp -= perkArray[1];
            this.ap -= perkArray[2];
            this.dp -= perkArray[3];
            this.sp -= perkArray[4];
        }
    }

    public void UpdatePerk(int[] newPerkArr)
    {
        int[] tempPerk = new int[perkArray.Length];
        for (int i = 0; i < perkArray.Length; i++)
        {
            tempPerk[i] = perkArray[i];
        }
        for (int i = 0; i < perkArray.Length; i++)
        {
            perkArray[i] = newPerkArr[i];
        }
        ApplyPerk(0);
        for (int i = 0; i < perkArray.Length; i++)
        {
            perkArray[i] = newPerkArr[i] + tempPerk[i];
        }
    }

    public void ResetPerks()
    {
        ApplyPerk(1);
        for (int i = 0; i < this.perkArray.Length; i++)
        {
            this.perkArray[i] = 0;
        }
    }

    #endregion

    #region fans

    //Method to recalculate fans 
    public void RecalculateFans(int fan)
    {
        int tempMoney = this.fans + fan;
        this.fans = tempMoney > 0 ? tempMoney : 0;
    }

    #endregion

    #region Wealth

    //Method to recalculate wealth based on a new wealth
    public void RecalculateWealth(int newWealth)
    {
        this.money = newWealth;
    }

    //Method to recalculate wealth 
    public void RecalculateFightWealth(int wealth)
    {
        int tempMoney = this.money + wealth;
        this.money = tempMoney > 0 ? tempMoney : 0;
    }


    #endregion

    #region Fight

    public int Defend()
    {
        return this.dp;
    }

    //Method to Attack physicially
    //(Attack Power, Type)
    //0 attack means missed, 1 normal, 2 critical
    public int[] Attack()
    {
        int[] mu = {0,0};
        if (!AttackMissed())
        {
            if (AttackCritical())
            {
                return new int[] { (int)(ap * 1.8), 2 };
            }
            else
            {
                return new int[] { ap, 1};
            }
        }
        return mu;
    }


    //Method to Attack with magica
    //(Attack Power, Type)
    //0 attack means missed, 1 normal, 2 critical
    public int[] AttackMagi()
    {
        int[] mu = { 0, 0 };
        if (!AttackMissed())
        {
            if (AttackCritical())
            {
                return new int[] { (int)(mp * 1.8), 2 };
            }
            else
            {
                return new int[] { mp, 1 };
            }
        }
        return mu;
    }

    //Method to get powerfullness of an attack
    // So return types will be: (0: normalAttack,1:critical attack)
    public bool AttackCritical()
    {
        System.Random randomizerMax = new System.Random();
        int rr = randomizerMax.Next(0, 100);
        rr += this.sp/2;
        if (rr < 80)
        {
            return false;
        }
        return true;
    }

    //Method to get missness of an attack
    // So return types will be: (0: done,1:missed)
    public bool AttackMissed()
    {
        System.Random randomizerMax = new System.Random();
        int rr = randomizerMax.Next(0, 100);
        rr -= this.sp;
        if (rr < 80)
        {
            return false;
        }
        return true;
    }

    #endregion

    public PlayerUber(int quejugadoresm8)
    {
        //(1:Undead,2:Witch,3:Samurai,4:RiceMan)
        switch (quejugadoresm8)
        {
            case 1:
                Undead(1);
                break;
            case 2:
                Magician(1);
                break;
            case 3:
                Samurai(1);
                break;
            case 4:
                RiceMonk(1);
                break;
            default:
                break;
        }
    }

    public void Undead(int gen)
    {
        maxhp = 60;
        hp = 60;
        mp = 6;
        ap = 20;
        dp = 20;
        sp = 6;
        lv = 1;
        gender = gen;
        money = 200;
        fans = 50;
        exp = 0;
        maxExp = 5;
        items = new PlayerItems();
        level = new LV(3);
        magica = new Magica(0, 0);
    }

    public void Samurai(int gen)
    {
        maxhp = 100;
        hp = 100;
        mp = 10;
        ap = 10;
        dp = 8;
        sp = 10;
        lv = 1;
        gender = gen;
        money = 200;
        fans = 50;
        exp = 0;
        maxExp = 5;
        items = new PlayerItems();
        level = new LV(0);
        magica = new Magica(0, 0);
    }

    public void RiceMonk(int gen)
    {
        maxhp = 60;
        hp = 60;
        mp = 6;
        ap = 16;
        dp = 4;
        sp = 16;
        lv = 1;
        gender = gen;
        money = 200;
        fans = 50;
        exp = 0;
        maxExp = 5;
        items = new PlayerItems();
        level = new LV(2);
        magica = new Magica(0, 0);
    }

    public void Magician(int gen)
    {
        maxhp = 80;
        hp = 80;
        mp = 18;
        ap = 6;
        dp = 6;
        sp = 8;
        lv = 1;
        gender = gen;
        money = 200;
        fans = 50;
        exp = 0;
        maxExp = 5;
        items = new PlayerItems();
        level = new LV(1);
        magica = new Magica(0, 0);
    }

    public static int normalizeCurrentPlayer(int currentPlayer){
        switch (currentPlayer){
			case 1:
				currentPlayer = 1;
				break;
			case 2:
				currentPlayer = 4;
				break;
			case 3:
				currentPlayer = 2;
				break;
			case 4:
				currentPlayer = 3;
				break;
			default:
				currentPlayer = 1;
				break;
		}
        return currentPlayer;   
    }
}
