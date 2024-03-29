﻿using UnityEngine;
using System.Collections;

public class CommonSys : MonoBehaviour {

    private static CommonSys commonSys;  //!< コモンシステム
    private static void SetSystem(CommonSys sys) { commonSys = sys; }
    public static CommonSys GetSystem() { return commonSys; }

    public CommonCache cache            = new CommonCache();        //!< コモンキャッシュ(ここではテクスチャにのみ使用)
    public CommonResource resource      = new CommonResource();     //!< コモンリソース(オブジェクト読み込み等はこれを使用)

    /*******************************************************/
    /* !@brief  : Awake
     *  @date   : 2014/03/18
     *  @author : コロソブス(korombus)
     *******************************************************/
    public virtual void Awake() {
        // システムを登録
        SetSystem(this);
        OPTION.LoadOption();
    }

    /*******************************************************/
    /* !@brief  : Start
     *  @date   : 2014/03/18
     *  @author : コロソブス(korombus)
     *******************************************************/
    public virtual void Start() {  }

//    public abstract void AfterStart();
//    public abstract IEnumerator BeforeStart();

    public virtual void AfterStart() {
        // フェード開始
        // 必要なデータ読み込み
        // データをセット
        // オプション
        // 画面生成
    }

    public virtual void BeforStart() {
        // システムを登録
        SetSystem(this); 
        // システムをセット
        // フェードを終了
    }

    /**********************************************************/
    /* !@brief      : テクスチャをオブジェクトにセット
     *  @param[in]  : tex   -> テクスチャ本体
     *  @param[in]  : obj   -> テクスチャをセットするオブジェクト
     *  @retval     : bool
     *  @date       : 2014/05/02
     *  @author     : コロソブス(korombus)
     **********************************************************/
    public bool SetTexture(Texture2D tex, GameObject obj) {
        // テクスチャがなければ返す
        if (tex == null || obj == null) { return false; }
        obj.renderer.material.mainTexture = tex;

        // テクスチャをキャッシュにセット
        cache.set(tex.name, tex);
        return true;
    }

    /**********************************************************/
    /* !@brief      : テクスチャ名からテクスチャを探してオブジェクトにセット
     *  @param[in]  : texName   -> テクスチャ名
     *  @param[in]  : obj       -> テクスチャをセットするオブジェクト
     *  @retval     : bool
     *  @date       : 2014/05/02
     *  @author     : コロソブス(korombus)
     **********************************************************/
    public bool SetTexture(string texName, GameObject obj, string texturePath) {
        if (texName == null || obj == null) { return false; }
        Texture2D tex = GetTexture(texName);
        if (tex == null) {
            CommonResource resource = new CommonResource();
            tex = resource.Load(this, texturePath, typeof(Texture2D)) as Texture2D;
        }
        SetTexture(tex, obj);
        return true;
    }

    /**********************************************************/
    /* !@brief      : テクスチャを取得
     *  @param[in]  : texName   -> テクスチャ名
     *  @retval     : Texture2D
     *  @date       : 2014/05/02
     *  @author     : コロソブス(korombus)
     **********************************************************/
    private Texture2D GetTexture(string texName) {
        Texture2D tex = cache.get(texName) as Texture2D;
        return tex;
    }

}

public class OPTION : DataSys
{
    // 音楽関連
    public enum Sound
    {
        BGM
       , SE
    }

    private static float _BGMVolume = 1.0f;   //!< BGM音量
    private static float _SEVolume  = 1.0f;    //!< SE音量
    private static float _TextSpeed = 0.07f;   //!< 文字表示速度

    public static float BGMVolume { get { return _BGMVolume; } }
    public static float SEVolume { get { return _SEVolume; } }
    public static float TextSpeed { get { return _TextSpeed; } }

    /*******************************************************/
    /* !@brief  : Volumeをセット
     *  @param[in]  : value ->  音量
     *  @param[in]  : sound ->  音量を変更する対象
     *  @retval : なし
     *  @date   : 2014/03/22
     *  @author : コロソブス(korombus)
     *******************************************************/
    public static void SetVolume(float value, Sound SB, CommonSound sound = null) {
        if (value <= 0 || value >= 1) { return; }

        switch (SB) {
            case Sound.BGM:
                _BGMVolume = value;
                break;
            case Sound.SE:
                _SEVolume = value;
                break;
            default:
                Debug.Log("NO SOUND");
                return;
        }

        if (sound != null) {
            sound.ChangeVolume(value);
        }
    }

    /*******************************************************/
    /* !@brief  : テキスト速度をセット
     *  @param[in]  : value ->  速度
     *  @retval : なし
     *  @date   : 2014/03/22
     *  @author : コロソブス(korombus)
     *******************************************************/
    public static void SetSpeed(float value) {
        if (value < 0 || value >= 1) { return; }
        _TextSpeed = value;
    }

    /*******************************************************/
    /* !@brief  : セーブ
     *  @param[in]  : saveId    -> セーブするID
     *  @retval : なし
     *  @date   : 2014/03/30
     *  @author : コロソブス(korombus)
     *******************************************************/
    public static bool DataSave(int saveId) {
        Save();
        return true;
    }

    /*******************************************************/
    /* !@brief  : ロード
     *  @param[in]  : loadId    -> ロードするID
     *  @retval : なし
     *  @date   : 2014/03/30
     *  @author : コロソブス(korombus)
     *******************************************************/
    public static bool DataLoad(int loadId) {
        Load();
        return true;
    }

    /*******************************************************/
    /* !@brief  : オプションをセーブ
     *  @param[in]  : なし
     *  @retval : なし
     *  @date   : 2014/03/22
     *  @author : コロソブス(korombus)
     *******************************************************/
    public static void SaveOption() {
        SaveOptionData();
    }

    /*******************************************************/
    /* !@brief  : オプションをロード
     *  @param[in]  : なし
     *  @retval : なし
     *  @date   : 2014/03/22
     *  @author : コロソブス(korombus)
     *******************************************************/
    public static void LoadOption() {
        LoadOptionData();
    }
}
