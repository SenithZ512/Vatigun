using TMPro;
using UnityEngine;

public class UpdateMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;

    private void Start()
    {
        Moneyupdate();
    }
    private void OnEnable()
    {
        GameEvent.OnUpgrade += Moneyupdate;
    }
    private void OnDisable()
    {
        GameEvent.OnUpgrade -= Moneyupdate;
    }
    private void Moneyupdate()
    {
        Text.text = "Curency: "+Currency.currentcurrency.ToString();
    }
         
    

}
