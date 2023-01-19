namespace DefaultNamespace
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	public class UnitManager : MonoBehaviour
	{
		[SerializeField] private Unit _unitPrefab;

		[SerializeField] private int _teamSize = 500;

		private Dictionary<int, List<Unit>> _activeUnits = new Dictionary<int, List<Unit>>();

		
		private void Start()
		{
			int[] _teams = new[] { 0, (int)(Screen.width / Settings.zoom) };
			
			float spacing = (Screen.height / Settings.zoom) / _teamSize;

			int team = 0;
			foreach (var startX in _teams)
			{
				_activeUnits.Add(team, new List<Unit>());
				float y = 0;
				for (int i = 0; i < _teamSize; i++)
				{
					var unit = Instantiate(_unitPrefab, transform);
					unit.Setup(this, team);
					_activeUnits[team].Add(unit);
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