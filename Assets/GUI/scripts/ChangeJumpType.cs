using UnityEngine;
using UnityEngine.UI;

public class ChangeJumpType : MonoBehaviour
{
    public PlayerStats stats;
    public Dropdown dropdown;

    private void Awake()
    {
        dropdown.value = 2;
        stats.jumpType = JumpType.boost;
    }

    public void ChangeType()
    {
        if (dropdown.value == 0)
            stats.jumpType = JumpType.hover;
        else if (dropdown.value == 1)
            stats.jumpType = JumpType.rocket;
        else if (dropdown.value == 2)
            stats.jumpType = JumpType.boost;
    }

}
