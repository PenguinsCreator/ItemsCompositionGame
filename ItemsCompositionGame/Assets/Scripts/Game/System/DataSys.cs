using UnityEngine;
using System.Collections;

public class DataSys {

    private static int _userCompositionCount;   //!< 合成回数
    private static int _userItemCompleteNum;    //!< これまで揃えたアイテムの数

    private static float _bgmVolume;    //!< BGM音量
    private static float _seVolume;     //!< SE音量

    public static int UserCompositionCount {
        get { return _userCompositionCount; }
        private set { _userCompositionCount = value; }
    }

    public static int UserItemCompleteNum {
        get { return _userItemCompleteNum; }
        private set { _userItemCompleteNum = value; }
    }

    /// <summary>
    /// ロード
    /// </summary>
    protected static void Load() {
        UserCompositionCount = PlayerPrefs.GetInt(GameDefine.SAVE_KEY_USER_COMPOSITION_COUNT);
        UserItemCompleteNum = PlayerPrefs.GetInt(GameDefine.SAVE_KEY_COMPLETE_ITEM_NUM);
    }

    /// <summary>
    /// セーブ
    /// </summary>
    protected static void Save() {
        PlayerPrefs.SetInt(GameDefine.SAVE_KEY_USER_COMPOSITION_COUNT, UserCompositionCount);
        PlayerPrefs.SetInt(GameDefine.SAVE_KEY_COMPLETE_ITEM_NUM, UserItemCompleteNum);
    }

    /// <summary>
    /// オプション情報をロード
    /// </summary>
    protected static void LoadOptionData() {
        OPTION.SetVolume(PlayerPrefs.GetFloat(GameDefine.SAVE_KEY_OPTION_BGM_VOLUME), OPTION.Sound.BGM);
        OPTION.SetVolume(PlayerPrefs.GetFloat(GameDefine.SAVE_KEY_OPTION_SE_VOLUME), OPTION.Sound.SE);
    }

    /// <summary>
    /// オプション情報をセーブ
    /// </summary>
    protected static void SaveOptionData() {
        PlayerPrefs.SetFloat(GameDefine.SAVE_KEY_OPTION_BGM_VOLUME, OPTION.BGMVolume);
        PlayerPrefs.SetFloat(GameDefine.SAVE_KEY_OPTION_SE_VOLUME, OPTION.SEVolume);
    }
}
