namespace DefaultNamespace
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Weapons;

	public class UnitManager : MonoBehaviour
	{
		[Serializable]
		private class WeaponPrice
		{
			public int price;
			public WeaponBase weapon;
		}
		[Serializable]
		private class ArmorPrice
		{
			public int price;
			//public ArmorBase armor;
		}
		[Serializable]
		private class UnitPrice
		{
			public int price;
			public Unit unit;
		}
		
		[SerializeField] private int _teamValue = 500;
		
		[SerializeField] private List<UnitPrice> _units;
		[SerializeField] private List<ArmorPrice> _armors;
		[SerializeField] private List<WeaponPrice> _weapons;

		private Dictionary<int, List<Unit>> _activeUnits = new Dictionary<int, List<Unit>>();

		
		private void Start()
		{
			int[] _teams = new[] { 0, (int)(Screen.width / Settings.zoom) };

			int team = 0;
			foreach (var startX in _teams)
			{
				_activeUnits.Add(team, new List<Unit>());
				var value = _teamValue;
				while (value > 0)
				{
					var posUnits = _units.Where(u => u.price <= value).ToList();
					if (posUnits.Count > 0)
					{
						var selectedUnit = posUnits.GetRandomElement();
						
						value -= selectedUnit.price;
						var unitInstance = Instantiate(selectedUnit.unit, transform);
						_activeUnits[team].Add(unitInstance);

						WeaponBase weapon = null;
						
						var posWeapons = _weapons.Where(u => u.price <= value).ToList();
						if (posWeapons.Count > 0)
						{
							var selectedWeapon = posWeapons.GetRandomElement();
							value -= selectedWeapon.price;
							weapon = Instantiate(selectedWeapon.weapon, unitInstance.transform);
						}
						
						var posArmors = _weapons.Where(u => u.price <= value).ToList();
						if (posArmors.Count > 0)
						{
							var selectedArmor = posArmors.GetRandomElement();
							value -= selectedArmor.price;
							//armor = Instantiate(selectedArmor.armor, unitInstance.transform);
						}

						unitInstance.Setup(this, team, weapon);
					}
					else
					{
						value = 0;
					}
				}
			
				float spacing = (Screen.height / Settings.zoom) / _activeUnits[team].Count;			
				float y = 0;
				foreach (var unit in _activeUnits[team])
				{
					unit.transform.position = new Vector3(startX, y);
					y += spacing;
				}

				team++;
			}
		}

		public void UnitDead(Unit unit)
		{
			_activeUnits[unit.team].Remove(unit);
		}

		public Unit GetEnemy(int teamOpposing)
		{
			var otherTeams = new List<int>(_activeUnits.Keys);
			otherTeams.Remove(teamOpposing);

			var index = otherTeams.GetRandomElement();
			return _activeUnits[index].GetRandomElement();
		}
	}
}