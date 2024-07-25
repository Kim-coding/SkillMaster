using UnityEngine;

public class SetTriggerDeath : MonoBehaviour, IDestructible
{
    public DeathPopup deathPopup;
    public void OnDestruction(GameObject attacker)
    {
        var player = gameObject.GetComponent<IAnimation>();
        player.Animator.SetTrigger("Death");
        deathPopup.gameObject.SetActive(true);

    }
}
