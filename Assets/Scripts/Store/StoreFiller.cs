using UnityEngine;
using UnityEditor;

public class StoreFiller
{
    StoreMagia sM = new StoreMagia();
    StoreItem  sI = new StoreItem();
    StoreEquip sE = new StoreEquip();
    public LittleMagia lM;
    public LittleItem lI;
    public LittleEquip lE;

    public StoreFiller(int area)
    {
        lM = sM.Selector(area);
        lI = sI.Selector(area);
        lE = sE.Selector(area);
    }
}

public class StoreMagia
{
    string[][] magicNames = new string[][] {new string[] { "Fire", "Blizzard", "Shock", "Natura" }, new string[] { "Big Fire", "Big Blizzard", "Big Shock", "Big Natura" }, new string[] { "Death Fire", "Death Blizzard", "Death Shock", "Death Natura" }, new string[] { "God Fire", "God Blizzard", "God Shock", "God Natura" } };
    string[][] magicDescr = new string[][] { new string[] { "It burns a little.","It cools a little.","It electrocutes a little.","It calms a little."}, new string[] { "It burns a lot.","It cools a lot.","It electrocutes a lot.","It calms a lot."}, new string[] { "It burns so much.","It cools so much.","It electrocutes so much.","It calms so much."}, new string[] { "It burns like a god.","It cools like a god.","It electrocutes like a god.","It calms like a god."}};
    int[][] magicPower = new int[][] { new int[] { 20,20,20,20}, new int[] { 50,50,50,50}, new int[] { 70,70,70,70}, new int[] { 100,100,100,100}};
    int[][] magicCosts = new int[][]  { new int[] { 100,100,100,100}, new int[] { 500,500,500,500}, new int[] { 1250,1250,1250,1250}, new int[] { 3125,3125,3125,3125}};

    public LittleMagia Selector(int area)
    {
        return new LittleMagia(area-1,magicNames,magicDescr,magicPower,magicCosts);
    }
}

public class LittleMagia
{
    public string[] magicNames;
    public string[] magicDescr;
    public int[] magicPower;
    public int[] magicCosts;

    public LittleMagia(){;}

    public LittleMagia(int area, string[][] mN, string[][] mD, int[][] mP, int[][] mC)
    {
        magicNames = mN[area];
        magicDescr = mD[area];
        magicPower = mP[area];
        magicCosts = mC[area];
    }
}

public class StoreItem
{
    string[][] itemNames = new string[][] { new string[] {"Food","Great Food","Almost-Empty Machine Core","Half Full Machine Core","A Rock","Two Rocks","","A bit Of Defence Stew","Some Defence Stew","OK Shoelaces","Nice Shoelaces","Astral Food"}, new string[] {"Great Food","Delicious Food","Half Full Machine Core","Almost-Full Machine Core","Two Rocks","The Same Rock. But With A Hat","Some Defence Stew","A Bowl Defence Stew","Nice Shoelaces","Great Shoelaces","Abstract Food"}, new string[] { "Delicious Food", "A Delicacy", "Almost-Full Machine Core", "Full Machine Core", "The Same Rock. But With A Hat", "Two Rocks With Hats", "A Bowl Defence Stew", "A Pot Of Defence Stew", "Great Shoelaces", "Shoelaces That Look Like Snakes", "Cubist Food" }, new string[] { "A Delicacy", "Pizza", "Full Machine Core", "Big Machine Core", "Two Rocks With Hats", "Two Really Elegant Rocks", "A Pot Of Defence Stew", "Defence Stew Supreme", "Shoelaces That Look Like Snakes", "Shoelaces With Flames Painted On", "Astral Food", "Abstract Food", "Cubist Food", "The Ear Of An Artist" } };
    string[][] itemDescription = new string[][] { new string[] {"Some food. Looks stale.","Some good looking food.","Was filled with magic... Not so much now.","This one is half full. Or half empty...","It's a rock.","What's better than one rock?","It's like a spoonful...","Meh. At least it's not a spoonful.","OK.","Nice.","Some out of this world food."}, new string[] {"Some good looking food.","Finally something delicious.","This one is half full. Or half empty...","Nearly filled... You can smell the magic.","What's better than one rock?","So cute.","Meh. At least it's not a spoonful.","Some decent sized stew.","Nice.","Great.","This looks like a melted clock..."}, new string[] { "Finally something delicious.", "Looks expensive and really tasty.", "Nearly filled... You can smell the magic.", "Fully loaded and ready to be consumed.", "So cute.", "OMG look at their little hats!", "Some decent sized stew.", "A bit too much but ok. It looks yummy.", "Great.", "They should be called pythons.", "So blue. Why is it so blue?" }, new string[] { "Looks expensive and really tasty.", "Greatest food ever!", "Fully loaded and ready to be consumed.", "So big you can already feel the magic.", "OMG look at their little hats!", "They look so handsome... And cute!", "A bit too much but ok. It looks yummy.", "You are not gonna be able to eat all that.", "They should be called pythons.", "Because anything with flames on is faster.", "Some out of this world food.", "This looks like a melted clock...", "So blue. Why is it so blue? ", "I wouldnt eat this if I were you." } };
    int[][] itemNumbers =  new int[][] { new int[] {1,2,6,7,11,12,16,17,21,22,26}, new int[] { 2, 3, 7, 8, 12, 13, 17, 18, 22, 23, 27 }, new int[] { 3, 4, 8, 9, 13, 14, 18, 19, 23, 24, 28 }, new int[] { 4, 5, 9, 10, 14, 15, 19, 20, 24, 25, 26, 27, 28, 29 } };
    int[][] itemPower1 = new int[][] { new int[] { 10,0,0,0,0,1}, new int[] {30,0,0,0,0,2}, new int[] {0,5,0,0,0,6}, new int[] {0,10,0,0,0,7}, new int[] {0,0,5,0,0,11}, new int[] {0,0,10,0,0,12}, new int[] {2,0,0,5,0,16}, new int[] {4,0,0,10,0,17}, new int[] {0,0,0,0,5,21}, new int[] {0,0,0,0,10,22}, new int[] {0,0,15,20,20,26}};
    int[][] itemPower2 = new int[][] { new int[] {30,0,0,0,0,2}, new int[] {70,0,0,0,0,3}, new int[] {0,10,0,0,0,7}, new int[] {0,15,0,0,0,8}, new int[] {0,0,10,0,0,12}, new int[] {0,0,15,0,0,13}, new int[] {4,0,0,10,0,17}, new int[] {10,0,0,15,0,18}, new int[] {0,0,0,0,10,22}, new int[] {0,0,0,0,15,23}, new int[] {0,0,0,0,0,27}};
    int[][] itemPower3 = new int[][] {  new int[] { 70, 0, 0, 0, 0, 3 },  new int[] { 100, 0, 0, 0, 0, 4 },  new int[] { 0, 15, 0, 0, 0, 8 },  new int[] { 0, 25, 0, 0, 0, 9 },  new int[] { 0, 0, 15, 0, 0, 13 },  new int[] { 0, 0, 25, 0, 0, 14 },  new int[] { 10, 0, 0, 15, 0, 18 },  new int[] { 20, 0, 0, 25, 0, 19 },  new int[] { 0, 0, 0, 0, 15, 23 },  new int[] { 0, 0, 0, 0, 25, 24 },  new int[] { 20, 15, 30, 0, 0, 28 } };
    int[][] itemPower4 = new int[][] { new int[] {100,0,0,0,0,4}, new int[] {150,0,0,0,0,5}, new int[] {0,25,0,0,0,9}, new int[] {0,30,0,0,0,10}, new int[] {0,0,25,0,0,14}, new int[] {0,0,30,0,0,15}, new int[] {20,0,0,25,0,19}, new int[] {30,0,0,30,0,20}, new int[] {0,0,0,0,25,24}, new int[] {0,0,0,0,30,25}, new int[] {0,0,15,20,20,26}, new int[] {0,0,0,0,0,27}, new int[] {20,15,30,0,0,28}, new int[] {-100,50,50,30,0,29}};
    int[][] itemCosts =  new int[][] { new int[] {70,120,70,120,70,120,70,120,70,120,1500}, new int[] { 120, 170, 120, 170, 120, 170, 120, 170, 120, 170, 1500 }, new int[] { 170, 220, 170, 220, 170, 220, 170, 220, 170, 220, 1500 }, new int[] { 220, 270, 220, 270, 220, 270, 220, 270, 220, 270, 1500, 1500, 1500, 1500 } };

    public LittleItem Selector(int area)
    {
        int[][] tempPower = itemPower1;
        switch (area)
        {
            case 1:
                tempPower = itemPower1;
                break;
            case 2:
                tempPower = itemPower2;
                break;
            case 3:
                tempPower = itemPower3;
                break;
            case 4:
                tempPower = itemPower4;
                break;
            default:
                break;
        }
        return new LittleItem(area-1, itemNames, itemDescription, itemNumbers, tempPower, itemCosts);
    }
}

public class LittleItem
{
    public string[] itemNames;
    public string[] itemDescription;
    public int[] itemNumbers;
    public int[][] itemPower;
    public int[] itemCosts;

    public LittleItem(int area, string[][] iNa, string[][] iD, int[][] iNo, int[][] iP, int[][] iC)
    {
        itemNames       = iNa[area];
        itemDescription = iD[area];
        itemNumbers     = iNo[area];
        itemPower       = iP;
        itemCosts       = iC[area];
    }
}

public class StoreEquip
{
    public string[][] equipmentNames = new string[][] { new string[] {"Leather Helmet","Wood Helmet","Metal Helmet","Toy Knife","Wood Sword","Old Hammer","Not-So-Cool Hammer","Tree Branch","A Stick","A Rice Grain","Some Rice","Leather Armor","Wood Armor","Metal Armor","Leather Shoes","Wood Shoes","Metal Shoes" }, new string[] { "Metal Helmet", "Ruby Helmet", "Sappire Helmet", "Wood Sword", "Great Sword", "Not-So-Cool Hammer", "Great Hammer", "A Stick", "Magic Wand", "Some Rice", "Rice Bowl", "Metal Armor", "Ruby Armor", "Sappire Armor", "Metal Shoes", "Ruby Shoes", "Sappire Shoes" }, new string[] {"Sappire Helmet","Pearl Helmet","Saviour Helmet","Great Sword","Hero Sword","Great Hammer","Destruction Hammer","Magic Wand","Magic Staff","Rice Bowl","Rice Bag","Sappire Armor","Pearl Armor","Saviour Armor","Sappire Shoes","Pearl Shoes","Saviour Shoes"}, new string[] { "Supernatural Helmet", "Abstract Helmet", "Future Helmet", "Hero Sword", "Astral Sword", "Destruction Hammer", "Thaors Hammer", "Magic Staff", "Deity's Staff", "Rice Bag", "Ancient God Rice Supreme", "Supernatural Armor", "Abstract Armor", "Future Armor", "Supernatural Shoes", "Abstract Shoes", "Future Shoes" } };
    public int[][] equipmentArea   = new int[][] { new int[] { 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4 }, new int[] {1,1,1,2,2,2,2,2,2,2,2,3,3,3,4,4,4}, new int[] {1,1,1,2,2,2,2,2,2,2,2,3,3,3,4,4,4}, new int[] {1,1,1,2,2,2,2,2,2,2,2,3,3,3,4,4,4} };
    public int[][] equipmentLV     = new int[][] { new int[] {1,2,3,1,2,6,7,11,12,16,17,1,2,3,1,2,3}, new int[] {3,4,5,2,3,7,8,12,13,17,18,3,4,5,3,4,5}, new int[] {5,6,7,3,4,8,9,13,14,18,19,5,6,7,5,6,7}, new int[] {8,9,10,4,5,9,10,14,15,19,20,8,9,10,8,9,10} };
    public int[][] equipmentPower1 = new int[][] {new int[] {0,0,0,2,0,1},new int[] {0,3,0,4,0,2},new int[] {0,0,0,6,0,3},new int[] {0,0,4,0,0,1},new int[] {0,0,10,0,0,2},new int[] {0,0,7,0,0,6},new int[] {0,0,15,0,0,7},new int[] {0,5,2,0,0,11},new int[] {0,8,3,0,0,12},new int[] {0,1,1,0,0,16},new int[] {0,2,2,0,0,17},new int[] {3,0,0,0,0,1},new int[] {4,0,0,4,0,2},new int[] {15,0,0,10,0,3},new int[] {0,0,0,0,2,1},new int[] {0,0,0,0,5,2},new int[] {0,0,0,4,2,3}};
    public int[][] equipmentPower2 = new int[][] {new int[] {0,0,0,6,0,3},new int[] {0,15,0,15,0,4},new int[] {0,10,0,20,0,5},new int[] {0,0,10,0,0,2},new int[] {0,5,20,0,0,3},new int[] {0,0,15,0,0,7},new int[] {0,0,40,0,0,8},new int[] {0,8,3,0,0,12},new int[] {0,15,5,0,0,13},new int[] {0,2,2,0,0,17},new int[] {0,10,10,0,0,18},new int[] {15,0,0,10,0,3},new int[] {30,5,0,10,0,4},new int[] {30,5,0,10,0,5},new int[] {0,0,0,4,2,3},new int[] {0,10,0,0,10,4},new int[] {0,5,0,0,10,5}};
    public int[][] equipmentPower3 = new int[][] {new int[] {0,10,0,20,0,5},new int[] {0,20,0,10,0,6},new int[] {0,20,0,20,0,7},new int[] {0,5,20,0,0,3},new int[] {0,10,50,0,0,4},new int[] {0,0,40,0,0,8},new int[] {0,0,70,0,0,9},new int[] {0,15,5,0,0,13},new int[] {0,40,15,0,0,14},new int[] {0,10,10,0,0,18},new int[] {0,30,30,0,0,19},new int[] {30,5,0,10,0,5},new int[] {30,5,0,10,0,6},new int[] {60,0,0,20,0,7},new int[] {0,5,0,0,10,5},new int[] {0,10,0,0,10,6},new int[] {0,0,0,0,20,7}};
    public int[][] equipmentPower4 = new int[][] {new int[] {0,35,0,35,0,8},new int[] {0,60,0,0,0,9},new int[] {0,30,0,50,0,10},new int[] {0,10,50,0,0,4},new int[] {0,20,70,0,0,5},new int[] {0,0,70,0,0,9},new int[] {0,0,100,0,0,10},new int[] {0,40,15,0,0,14},new int[] {0,60,20,0,0,15},new int[] {0,30,30,0,0,19},new int[] {0,50,50,0,0,20},new int[] {80,0,0,20,0,8},new int[] {100,20,0,30,0,9},new int[] {100,0,0,50,0,10},new int[] {0,0,0,0,30,8},new int[] {0,30,0,0,40,9},new int[] {0,0,0,0,50,10}};
    public int[][] equipmentCosts  = new int[][] { new int[] { 500, 900, 1300, 600, 1100, 600, 1100, 600, 1100, 600, 1100, 500, 900, 1300, 500, 900, 1300 }, new int[] {1300,1700,2100,1100,1600,1100,1600,1100,1600,1100,1600,1300,1700,2100,1300,1700,2100}, new int[] { 2100, 2500, 2900, 1600, 2100, 1600, 2100, 1600, 2100, 1600, 2100, 2100, 2500, 2900, 2100, 2500, 2900 }, new int[] {3300,3700,4100,2100,2600,2100,2600,2100,2600,2100,2600,3300,3700,4100,3300,3700,4100} };

    public LittleEquip Selector(int area)
    {
        int[][] tempPower = equipmentPower1;
        switch (area)
        {
            case 1:
                tempPower = equipmentPower1;
                break;
            case 2:
                tempPower = equipmentPower2;
                break;
            case 3:
                tempPower = equipmentPower3;
                break;
            case 4:
                tempPower = equipmentPower4;
                break;
            default:
                break;
        }
        return new LittleEquip(area-1, equipmentNames, equipmentArea, equipmentLV, tempPower, equipmentCosts);
    }
}

public class LittleEquip
{
    public string[] equipmentNames;
    public int[] equipmentArea;
    public int[] equipmentLV;
    public int[][] equipmentPower;
    public int[] equipmentCosts;

    public LittleEquip(int area, string[][] eNa, int[][] eA, int[][] eLv, int[][] eP, int[][] eC)
    {
        equipmentNames = eNa[area];
        equipmentArea  = eA[area];
        equipmentLV    = eLv[area];
        equipmentPower = eP;
        equipmentCosts = eC[area];
    }
}

