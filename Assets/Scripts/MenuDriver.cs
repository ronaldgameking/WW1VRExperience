using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityUtils;
using System.Linq;
using System.Text;

using Text = TMPro.TextMeshProUGUI;

public class MenuDriver : MonoBehaviour
{
    //public SceneAsset SceneAss;
    public string SceneAssStr;
    public string FlagsPath => Path.Combine(Application.persistentDataPath, "flags");

    public bool EnableResolutionWarning = false;
    [DrawIf(nameof(EnableResolutionWarning), DisablingType.DontDraw)]
    public Vector2Int IntendedResolution = new Vector2Int(1920, 1080);
    [DrawIf(nameof(EnableResolutionWarning), DisablingType.DontDraw)]
    public Text WarningText;
    
    public Dictionary<string, int> SceneLocations = new Dictionary<string, int>();
    [SerializeField]
    private int menuScenes;

    [SerializeField] private float zR;
    private GameObject aaaaa;

    private void Awake()
    {
        //Scene location definitions
        SceneLocations.Add("main", 0);
        SceneLocations.Add("gameover", 1);

        //Resolution warn UI
        if (EnableResolutionWarning)
        {
            Resolution res = Screen.currentResolution;
            //Show a warning that UI elements may look off at the current resolution if it doesn't match the intended resolution
            if (res.width != IntendedResolution.x || res.height != IntendedResolution.y)
                WarningText.gameObject.SetActive(true);
            else
                WarningText.gameObject.SetActive(false);

        }
    }

    public void PlayButton()
    {
        PlayerPrefs.SetInt("hasPlayed", 1);
        //Global.playAmount += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + menuScenes);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(SceneLocations["settings"]);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(SceneLocations["credits"]);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Uses a string
    /// </summary>
    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GoTo(string destination)
    {
        if (SceneLocations.TryGetValue(destination, out int targetIndex))
        {
            SceneManager.LoadScene(targetIndex);
        }
        else
        {
            Debug.LogWarning(string.Format("Scene with name {0} not found", destination));
        }
    }
    public void SetFlag(string crossSceneFlag)
    {
        FileStream fs = new FileStream(FlagsPath, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs, Encoding.UTF8, false, 1024, true);

        string flagsStr = sr.ReadToEnd();
        flagsStr = flagsStr.TrimEnd(',');
        string[] flags = flagsStr.Split(',');

        HashSet<string> uniqFlags = new HashSet<string>();
        foreach (var flag in flags)
        {
            if (flag == string.Empty) continue;
            uniqFlags.Add(flag);
        }
        uniqFlags.Add(crossSceneFlag);

        string[] exportFlags = new string[uniqFlags.Count];
        uniqFlags.CopyTo(exportFlags);

        sr.Close();
        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8, 1024, true);
        sw.Write(string.Join(",", exportFlags));

        sw.Close();
        fs.Close();
    }

    public bool GetFlag(string flag)
    {
        try
        {
            FileStream fs = new FileStream(FlagsPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8, false, 1024, true);

            string flagsStr = sr.ReadToEnd();
            flagsStr = flagsStr.TrimEnd(',');
            string[] flags = flagsStr.Split(',');

            HashSet<string> uniqFlags = new HashSet<string>();
            foreach (var flagChj in flags)
            {
                if (flagChj == string.Empty) continue;
                if (flagChj == flag) continue;
                uniqFlags.Add(flag);
            }

            string[] exportFlags = new string[uniqFlags.Count];
            uniqFlags.CopyTo(exportFlags);

            sr.Close();
            fs.Close();
            fs = new FileStream(FlagsPath, FileMode.Truncate);

            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8, 1024, true);
            sw.Write(string.Join(",", exportFlags));

            sw.Close();
            fs.Close();

            return flags.Contains(flag);

        }
        catch(FileNotFoundException)
        {
            return false;
        }
    }

    // public void PlayFileSelect()
    // {
    //     AudioManager.Instance.FadeOut("title_bgm");
    //     AudioManager.Instance.Play("file_select");
    // }
    public void PlayTitle()
    {
        // if (AudioManager.Instance.GetSource("file_select").isPlaying)
        //     AudioManager.Instance.FadeOut("file_select");

        if (AudioManager.Instance.GetSource("settings_bgm").isPlaying)
            AudioManager.Instance.FadeOut("settings_bgm");
        
        AudioManager.Instance.Play("title_bgm");
    }

    public void PlayGameSound()
    {
        // AudioManager.Instance.FadeOut("file_select");
        AudioManager.Instance.Play("game_bgm");
    }

    public void FadeSound(string s)
    {
        AudioManager.Instance.FadeOut(s);
    }

    public void LogVoid(string a)
    {
        
    }
}
