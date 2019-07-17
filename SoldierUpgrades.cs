using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum UpgradeType
{
    Soldier,
    Archer,
    Mage,
    Ninja
}

public class SoldierUpgrades : MonoBehaviour
{
    public UpgradeType UType;
    public BaseKnightMovement SoldierRef;
    private PlayerStatManager PSM;
    public Text UpgradeHealthText;
    public Text UpgradeDamageText;
    public Text UpgradeSpeedText;
    public Text UpgradeSpawnRateText;
    public Text UpgradeMageSpawnRateText;
    public Text UpgradeNinjaSpawnRateText;
    public Text UpgradeMageAoeRangeText;

    public int UpgradeHealthCost;
    public int UpgradeDamageCost;
    public int UpgradeSpeedCost;
    public int UpgradeSpawnRateCost;
    public int UpgradeMageSpawnRateCost;
    public int UpgradeMageAoeRangeCost;
    public int UpgradeNinjaSpawnRateCost;

	void Start ()
    {
        PSM = PlayerStatManager.Instance;
        GetSaves.Instance.SaveAll += Save;
        if (PlayerPrefs.HasKey("SoldierUpgradeHealthCost") || PlayerPrefs.HasKey("SoldierUpgradeDamageCost"))
            Load();    
        else
        {
            UpgradeHealthText.text = "Gems: " + UpgradeHealthCost;
            UpgradeDamageText.text = "Gems: " + UpgradeDamageCost;

            if (UType == UpgradeType.Mage)
            {
                UpgradeMageSpawnRateText.text = "Gems: " + UpgradeMageSpawnRateCost;
                UpgradeMageAoeRangeText.text = "Gems: " + UpgradeMageAoeRangeCost;
                UpgradeSpeedText.text = "Gems: " + UpgradeSpeedCost;
            }
            else if (UType == UpgradeType.Soldier)
            {
                UpgradeSpawnRateText.text = "Gems: " + UpgradeSpawnRateCost;
                UpgradeSpeedText.text = "Gems: " + UpgradeSpeedCost;
            }
            else if (UType == UpgradeType.Ninja)
            {
                UpgradeNinjaSpawnRateText.text = "Gems: " + UpgradeNinjaSpawnRateCost;
            }
        }
    }

    public void IncreaseSoldierCost()
    {
        if (UType == UpgradeType.Soldier)
        {
            PSM.SoldierCost += 20;
            UIManager.Instance.UpdateUI();
        }
        else if (UType == UpgradeType.Mage)
        {
            PSM.MageCost += 20;
            UIManager.Instance.UpdateUI();
        }
        else if (UType == UpgradeType.Ninja)
        {
            PSM.NinjaCost += 20;
            UIManager.Instance.UpdateUI();
        }
        GetSaves.Instance.SaveGame();
    }

    public void UpgradeHealth()
    {
        if(PSM.Gems >= UpgradeHealthCost)
        {
            if (UType == UpgradeType.Soldier)
                PSM.SMaxHealth++;
            else if (UType == UpgradeType.Archer)
                PSM.AMaxHealth++;
            else if (UType == UpgradeType.Mage)
                PSM.MMaxHealth++;
            else if (UType == UpgradeType.Ninja)
                PSM.NMaxHealth++;

            PSM.Gems -= UpgradeHealthCost;
            UpgradeHealthCost = UpgradeHealthCost += 5;
            UpgradeHealthText.text = "Gems: " + UpgradeHealthCost;

            IncreaseSoldierCost();
        }
    }

    public void UpgradeDamage()
    {
        if(PSM.Gems >= UpgradeDamageCost)
        {
            if (UType == UpgradeType.Soldier)
                PSM.SMaxDamage++;
            else if (UType == UpgradeType.Archer)
                PSM.AMaxDamage++;
            else if (UType == UpgradeType.Mage)
                PSM.MMaxDamage++;
            else if (UType == UpgradeType.Ninja)
                PSM.NMaxDamage++;

            PSM.Gems -= UpgradeDamageCost;
            UpgradeDamageCost = UpgradeDamageCost += 5;
            UpgradeDamageText.text = "Gems: " + UpgradeDamageCost;

            IncreaseSoldierCost();
        }
    }

    public void UpgradeSpeed()
    {
        if(PSM.Gems >= UpgradeSpeedCost)
        {
            PSM.Speed += 30;
            PSM.Gems -= UpgradeSpeedCost;
            UpgradeSpeedCost += 1;
            UpgradeSpeedText.text = "Gems: " + UpgradeSpeedCost;

            IncreaseSoldierCost();
        }
    }

    public void UpgradeSpawnRate()
    {
        if(PSM.Gems >= UpgradeSpawnRateCost)
        {
            BattleManager.Instance.FSpawnRate -= 0.1f;
            PSM.Gems -= UpgradeSpawnRateCost;
            UpgradeSpawnRateCost += 1;
            UpgradeSpawnRateText.text = "Gems: " + UpgradeSpawnRateCost;

            IncreaseSoldierCost();
        }
    }

    public void UpgradeMageSpawnRate()
    {
        if (PSM.Gems >= UpgradeMageSpawnRateCost)
        {
            BattleManager.Instance.FMageSpawnRate -= 0.1f;
            PSM.Gems -= UpgradeMageSpawnRateCost;
            UpgradeMageSpawnRateCost += 1;
            UpgradeMageSpawnRateText.text = "Gems: " + UpgradeMageSpawnRateCost;

            IncreaseSoldierCost();
        }
    }

    public void UpgradeMageAoeRange()
    {
        if (PSM.Gems >= UpgradeMageAoeRangeCost)
        {
            PSM.AOERange += 5;
            PSM.Gems -= UpgradeMageAoeRangeCost;
            UpgradeMageAoeRangeCost = UpgradeMageAoeRangeCost += 5;
            UpgradeMageAoeRangeText.text = "Gems: " + UpgradeMageAoeRangeCost;

            IncreaseSoldierCost();
        }
    }
    public void UpgradeNinjaSpawnRate()
    {
        if (PSM.Gems >= UpgradeNinjaSpawnRateCost)
        {
            BattleManager.Instance.FNinjaSpawnRate -= 0.1f;
            PSM.Gems -= UpgradeNinjaSpawnRateCost;
            UpgradeNinjaSpawnRateCost += 1;
            UpgradeNinjaSpawnRateText.text = "Gems: " + UpgradeNinjaSpawnRateCost;

            IncreaseSoldierCost();
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void Save()
    {
        if (UType == UpgradeType.Soldier)
        {
            PlayerPrefs.SetInt("SoldierUpgradeHealthCost", UpgradeHealthCost);
            PlayerPrefs.SetInt("SoldierUpgradeDamageCost", UpgradeDamageCost);
            PlayerPrefs.SetInt("SoldierUpgradeSpeedCost", UpgradeSpeedCost);
            PlayerPrefs.SetInt("SoldierUpgradeSpawnRateCost", UpgradeSpawnRateCost);
        }
        else if (UType == UpgradeType.Archer)
        {
            PlayerPrefs.SetInt("ArcherUpgradeHealthCost", UpgradeHealthCost);
            PlayerPrefs.SetInt("ArcherUpgradeDamageCost", UpgradeDamageCost);
        }
        else if (UType == UpgradeType.Mage)
        {
            PlayerPrefs.SetInt("MageUpgradeHealthCost", UpgradeHealthCost);
            PlayerPrefs.SetInt("MageUpgradeDamageCost", UpgradeDamageCost);
            PlayerPrefs.SetInt("MageUpgradeSpeedCost", UpgradeSpeedCost);
            PlayerPrefs.SetInt("MageUpgradeSpawnRateCost", UpgradeMageSpawnRateCost);
            PlayerPrefs.SetInt("MageUpgradeMageAoeRangeCost", UpgradeMageAoeRangeCost);
        }
        else if (UType == UpgradeType.Ninja)
        {
            PlayerPrefs.SetInt("NinjaUpgradeHealthCost", UpgradeHealthCost);
            PlayerPrefs.SetInt("NinjaUpgradeDamageCost", UpgradeDamageCost);
            PlayerPrefs.SetInt("UpgradeNinjaSpawnRateCost", UpgradeNinjaSpawnRateCost);
        }

    }
    public void Load()
    {
        if (UType == UpgradeType.Soldier)
        {
            UpgradeHealthCost = PlayerPrefs.GetInt("SoldierUpgradeHealthCost");
            UpgradeDamageCost = PlayerPrefs.GetInt("SoldierUpgradeDamageCost");
            UpgradeSpeedCost = PlayerPrefs.GetInt("SoldierUpgradeSpeedCost");
            UpgradeSpawnRateCost = PlayerPrefs.GetInt("SoldierUpgradeSpawnRateCost");
        }
        else if (UType == UpgradeType.Archer)
        {
            UpgradeHealthCost = PlayerPrefs.GetInt("ArcherUpgradeHealthCost");
            UpgradeDamageCost = PlayerPrefs.GetInt("ArcherUpgradeDamageCost");
        }
        else if (UType == UpgradeType.Mage)
        {
            UpgradeHealthCost = PlayerPrefs.GetInt("MageUpgradeHealthCost");
            UpgradeDamageCost = PlayerPrefs.GetInt("MageUpgradeDamageCost");
            UpgradeSpeedCost = PlayerPrefs.GetInt("MageUpgradeSpeedCost");
            UpgradeMageSpawnRateCost = PlayerPrefs.GetInt("MageUpgradeSpawnRateCost");
            UpgradeMageAoeRangeCost = PlayerPrefs.GetInt("MageUpgradeMageAoeRangeCost");
        }
        else if (UType == UpgradeType.Ninja)
        {
            UpgradeHealthCost = PlayerPrefs.GetInt("NinjaUpgradeHealthCost");
            UpgradeDamageCost = PlayerPrefs.GetInt("NinjaUpgradeDamageCost");
            UpgradeNinjaSpawnRateCost = PlayerPrefs.GetInt("UpgradeNinjaSpawnRateCost");
        }


        UpgradeHealthText.text = "Gems: " + UpgradeHealthCost;
        UpgradeDamageText.text = "Gems: " + UpgradeDamageCost;

        if (UType == UpgradeType.Mage)
        {
            UpgradeMageSpawnRateText.text = "Gems: " + UpgradeMageSpawnRateCost;
            UpgradeMageAoeRangeText.text = "Gems: " + UpgradeMageAoeRangeCost;
            UpgradeSpeedText.text = "Gems: " + UpgradeSpeedCost;
        }
        else if (UType == UpgradeType.Soldier)
        {
            UpgradeSpawnRateText.text = "Gems: " + UpgradeSpawnRateCost;
            UpgradeSpeedText.text = "Gems: " + UpgradeSpeedCost;
        }
        else if (UType == UpgradeType.Ninja)
        {
            UpgradeNinjaSpawnRateText.text = "Gems: " + UpgradeNinjaSpawnRateCost;
        }
    }
}
