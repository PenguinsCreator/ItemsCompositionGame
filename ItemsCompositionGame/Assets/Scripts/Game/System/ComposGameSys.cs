using UnityEngine;
using System.Collections;

public class ComposGameSys : CommonSys {

    // 継承変数
    protected static CommonSound BGM;   //!< BGM再生用オブジェクト
    protected static CommonSound SE;    //!< SE再生用オブジェクト

    // 内部変数
    private static bool alreadyReadMasterData = false;  //!< マスタデータ読み込みフラグ

    /// <summary>
    /// Awake
    /// </summary>
    public virtual void Awake() {
        base.Awake();
        // マスタデータ読み込みがまだならば読み込む
        if (!alreadyReadMasterData) {
            CommonMasterLoader master = new CommonMasterLoader();
            master.InitMasterData();
            alreadyReadMasterData = true;
        }
        // BGMがなければ作成
        if (BGM == null) {
            BGM = CommonUtil.PrefabInstance("BGM", GameDefine.COMMON_PREFABS_PATH + "BGM", this.gameObject.transform).GetComponent<CommonSound>();
            SE = CommonUtil.PrefabInstance("SE", GameDefine.COMMON_PREFABS_PATH + "SE", BGM.gameObject.transform).GetComponent<CommonSound>();
        }
        // 音量調節
        BGM.ChangeVolume(OPTION.BGMVolume);
        SE.ChangeVolume(OPTION.SEVolume);
    }

    /// <summary>
    /// Start
    /// </summary>
    public virtual void Start() {
        base.Start();
    }

    /// <summary>
    /// アイテムカテゴリに準じたアトラスを取得する
    /// </summary>
    /// <param name="i_categoryId">アイテムカテゴリＩＤ</param>
    /// <returns>UIAtlas</returns>
    public static UIAtlas GetUIAtlus(int i_categoryId) {
        string atlasName = GameDefine.CATEGORAIZE_ATLAS_NAME[i_categoryId];
        GameObject obj = Resources.Load(GameDefine.COMMON_ATLASES_ROOT + atlasName) as GameObject;
        return obj.GetComponent<UIAtlas>();
    }
}
