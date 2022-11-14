using UnityEngine;
using UnityEngine.UI;

public class ThisIsIsaac : MonoBehaviour
{
    [SerializeField] Image avatar;
    [SerializeField] Sprite fullSpoonsAvatar;
    [SerializeField] Sprite highSpoonsAvatar;
    [SerializeField] Sprite halfSpoonsAvatar;
    [SerializeField] Sprite lowSpoonsAvatar;
    [SerializeField] CharacterInteractor characterInteractor;

    private void Awake()
    {
        BecomeIsaac();
    }

    public void BecomeIsaac()
    {
        GameManager.GetInstance().isIsaac = true;
        characterInteractor.fullSpoonsAvatar = fullSpoonsAvatar;
        characterInteractor.highSpoonsAvatar = highSpoonsAvatar;
        characterInteractor.halfSpoonsAvatar = halfSpoonsAvatar;
        characterInteractor.lowSpoonsAvatar = lowSpoonsAvatar;
    }
}
