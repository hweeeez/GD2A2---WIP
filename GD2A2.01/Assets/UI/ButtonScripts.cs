using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ButtonScripts : MonoBehaviour
{
    string buttonSelect;
    public Camera maincamera;
    Vector3 homePos;
    Vector3 menuPos;
    // Start is called before the first frame update
    void Start()
    {
        homePos = new Vector3(-0.23f, 1f, -10f);
        menuPos = new Vector3(20.6f, 1f, -10f);
        maincamera.transform.position = homePos;
    }

    // Update is called once per frame
    void Update()
    {
        buttonSelect = EventSystem.current.currentSelectedGameObject.name;
        print(buttonSelect);
    }
    IEnumerator MoveTo(Camera maincam, Vector3 destination, float speed)
    {
        // This looks unsafe, but Unity uses
        // en epsilon when comparing vectors.
        while (maincam.transform.position != destination)
        {
            maincam.transform.position = Vector3.MoveTowards(
                maincam.transform.position,
                destination,
                speed * Time.deltaTime);
            // Wait a frame and move again.
            yield return null;
        }
    }
    public void menuClicked()
    {
        // maincamera.transform.position = Vector3.MoveTowards(homePos, menuPos, 1 * Time.deltaTime);
        StartCoroutine(MoveTo(maincamera, menuPos, 30));
    }
    public void homeClicked()
    {
        StartCoroutine(MoveTo(maincamera, homePos, 30));
    }
    public void loadLevel()
    {
        SceneManager.LoadScene(buttonSelect);
        int buttonIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + buttonSelect + ".unity");

        PlayerPrefs.SetInt("lastscene", buttonIndex);
    }
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void loadLastcene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("lastscene"));
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastscene"));
    }
    public void loadHome()
    {
        SceneManager.LoadScene("Home");
    }
}
