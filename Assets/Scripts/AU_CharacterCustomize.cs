using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AU_CharacterCustomize : MonoBehaviour
{
    [SerializeField] Color[] colors;



    public void SetColor(int colorIndex)
    {
        AU_PlayerController.localPlayer.SetColor(colors[colorIndex]);
    }

    public void NextScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
