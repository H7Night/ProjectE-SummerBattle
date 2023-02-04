using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword;

    private void Start()
    {
        if(PlayerController.Instance.scenePassword == entrancePassword)
        {
            PlayerController.Instance.transform.position = transform.position;
        }
        else
        {
            Debug.LogError("Wrong PW. Please Check your Scene name and Entrance password");
        }
    }
}
