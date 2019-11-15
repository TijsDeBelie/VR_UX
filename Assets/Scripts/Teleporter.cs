using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour
{
    /// <summary>
    ///  From video : https://www.youtube.com/watch?v=-T09oRMDuG8
    /// </summary>
    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleporterAction;

    public SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);

        if (m_TeleporterAction.GetStateUp(m_Pose.inputSource))
            TryTeleport();
    }

    private void TryTeleport()
    {
        // Check valid position and if already teleporting
        print("TryTeleport()");
        if (!m_HasPosition || m_IsTeleporting)
            return;
        // Check camera rig
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // Figure out translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translationVector = m_Pointer.transform.position - groundPosition;

        //Move
        StartCoroutine(MoveRig(cameraRig, translationVector));
    }

    private IEnumerator MoveRig(Transform cameraRig,Vector3 translation)
    {
        print("MoveRig()");
        // Flag
        m_IsTeleporting = true;
        // Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);
        // Apply translation
        yield return new WaitForSeconds(m_FadeTime);
        cameraRig.position += translation;

        // Fade back to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

        // De-flag
        m_IsTeleporting = false;
    }

    private bool UpdatePointer()
    {
        // Ray from controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        // If it is a hit
        if(Physics.Raycast(ray, out hit))
        {
            m_Pointer.transform.position = hit.point;
            return true;
        }
        // if not a hit
        return false;
    }
}
