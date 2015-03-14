using UnityEngine;
using System.Collections;

public class GameSettings {
	public HeroItemConfig[] heroConfigs;
	public WeaponItemConfig[] weaponConfigs;
	public BoosterItemConfig[] boosterConfigs;

	public void initGameSettings() {
		heroConfigs = Resources.LoadAll<HeroItemConfig> ("Prefabs/HeroCfg");
		weaponConfigs = Resources.LoadAll<WeaponItemConfig> ("Prefabs/WeaponCfg");
		boosterConfigs = Resources.LoadAll<BoosterItemConfig> ("Prefabs/BoosterCfg");
	}

}
