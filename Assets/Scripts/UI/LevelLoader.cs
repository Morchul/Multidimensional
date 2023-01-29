using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private LevelContainer levelContainer;

    [SerializeField]
    private UIButton levelPrefab;

    private int levelsPerRow = 3;

    [SerializeField]
    private float deltaBetweenButtons = 15;

    [SerializeField]
    private Transform levelsUIParent;

    // Start is called before the first frame update
    void Start()
    {

        PlayerData playerData = GlobalGameController.Instance.PlayerData;
        Debug.Log("playerData.currentLevel: " + playerData.currentLevel);

        Rect rect = levelPrefab.GetComponent<RectTransform>().rect;
        float width = rect.width;
        float height = rect.height;

        int countOfRows = levelContainer.levels.Length / 3 + 1;

        Vector2 startPos = new Vector2(-(width + deltaBetweenButtons), (countOfRows - 1f) / 2f * (height + deltaBetweenButtons));

        int counter = 0;
        while(counter < levelContainer.levels.Length)
        {
            for(int i = 0; i < levelsPerRow; ++i)
            {
                if (counter == levelContainer.levels.Length) break;

                string sceneName = levelContainer.levels[counter];

                UIButton levelButton = Instantiate(levelPrefab, levelsUIParent);
                TextMeshProUGUI text = levelButton.GetComponentInChildren<TextMeshProUGUI>();
                text.text = (counter + 1).ToString();
                
                if (playerData.currentLevel > counter)
                {
                    levelButton.onClick.AddListener(delegate { SelectLevel(sceneName); });
                }
                else
                {
                    levelButton.Disable();
                }

                levelButton.GetComponent<RectTransform>().anchoredPosition = startPos + new Vector2(
                    i * (width + deltaBetweenButtons),
                    counter / levelsPerRow * -(height + deltaBetweenButtons));

                ++counter;
            }
        }
    }

    private void SelectLevel(string sceneName)
    {
        Debug.Log("Load level: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
