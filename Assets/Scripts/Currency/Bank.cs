using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBalance = 250;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI balanceText;
    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startBalance;
        UpdateBalanceText();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateBalanceText();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateBalanceText();
    }

    void UpdateBalanceText()
    {
        balanceText.text = $"Gold: {currentBalance.ToString()}";
    }
}
