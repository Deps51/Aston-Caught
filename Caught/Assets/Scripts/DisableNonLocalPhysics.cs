using Mirror;
using UnityEngine; 

public class DisableNonLocalPhysics : NetworkBehaviour
{
    private void OnStartClient()
    {
        if(!isLocalPlayer)
        {
            Destroy(GetComponent<SphereCollider>());
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<PlayerMovement>());
        }
    }
}