using UnityEngine;

public class ThisIsIsaac : MonoBehaviour
{
    private void Awake()
    {
        BecomeIsaac();
    }

    public void BecomeIsaac()
    {
        GameManager.GetInstance().isIsaac = true;
    }
}
