using UnityEngine;
using TMPro;

namespace Catan.Unity.Panels
{
    public class InformationUI : MonoBehaviour
    {
        public TextMeshProUGUI TurnCounterText;
        public TextMeshProUGUI RolledNumberText;
        public TextMeshProUGUI InformationTableText;

        public void UpdateTurnNumber(int turnNumber)
        {
            TurnCounterText.text = $"Turn number: {turnNumber}";
        }

        public void UpdateRolledNumber(int rolledNumber)
        {
            RolledNumberText.text = $"Rolled number: {rolledNumber}";
        }

        public void UpdateInformationTable(string informationTable)
        {
            InformationTableText.text = informationTable;
        }
    }
}