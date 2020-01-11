using UnityEngine;
using System.IO;

public class InputReader : MonoBehaviour
{
    private int mh, mv, lh, lv, imh, imv, ich, icv;
    private ChangeInputAxes whichAxisToRead;
    private void Awake()
    {
        // GameObject gui = GameObject.FindGameObjectWithTag("GUI");
        // whichAxisToRead = gui.GetComponent<ChangeInputAxes>();
    }
    private PlayerStats _stats;
    private void OnEnable()
    {
        _stats = GetComponent<StatHolder>().held;
    }
    private void Update()
    {
        // mh = whichAxisToRead.movhor;
        // mv = whichAxisToRead.movver;
        // lh = whichAxisToRead.lookhor;
        // lv = whichAxisToRead.lookver;
        // imh = whichAxisToRead._ilkbh == true ? -1 : 1;
        // imv = whichAxisToRead._ilkbv == true ? -1 : 1;
        // ich = whichAxisToRead._ilch == true ?  -1 : 1;
        // icv = whichAxisToRead._ilcv == true ?  -1 : 1;
    }

    public float moveHorizontal
    {get{
        float kb = Input.GetAxisRaw("Horizontal");
        float ls = Input.GetAxis("LeftStickHorizontal");
        float rs = Input.GetAxis("RightStickHorizontal");
        if (kb != 0)
            return kb;
        else if (mh == 0 && ls != 0)
            return ls;
        else if (mh == 1 && ls != 0)
            return rs;
        else return 0;        
    }}

    public float moveVertical
    {get{
        float kb = Input.GetAxisRaw("Vertical");
        if (kb != 0)
            return kb;
        else if (mh == 0)
            return Input.GetAxis("LeftStickVertical");
        else if (mh == 1)
            return Input.GetAxis("RightStickVertical");
        else return 0;        
    }}

    public float lookHorizontal
    {get{
        float kb = Input.GetAxisRaw("LookHorizontal") * _stats.horizSensitivity;
        if (kb != 0)
            return kb;
        else if (mh == 0)
            return Input.GetAxisRaw("RightStickHorizontal") * _stats.horizSensitivity;
        else if (mh == 1)
            return Input.GetAxisRaw("LeftStickHorizontal") * _stats.horizSensitivity;
        else return 0;        
    }}

    public float lookVertical
    {get{
        float kb = Input.GetAxisRaw("LookVertical") * _stats.vertSensitivity;
        if (kb != 0)
            return kb;
        else if (mh == 0)
            return Input.GetAxisRaw("RightStickVertical") * _stats.vertSensitivity;
        else if (mh == 1)
            return Input.GetAxisRaw("LeftStickVertical") * _stats.vertSensitivity;
        else return 0;
    }}

    public bool jump
    {get{return Input.GetButtonDown("Jump");}}

    public bool sprint
    {get{return Input.GetButtonDown("Sprint");}}

    public bool crouch
    {get{return Input.GetButtonDown("Crouch");}}

    public bool hasMoveInput
    {get
        {
            if (moveHorizontal > 0 || moveHorizontal < 0)
                return true;
            else if (moveVertical > 0 || moveVertical < 0)
                return true;
            else
                return false;
        }
    }

    [HideInInspector] public bool wasSprinting;
}
