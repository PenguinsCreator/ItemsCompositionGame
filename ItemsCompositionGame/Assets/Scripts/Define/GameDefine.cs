using UnityEngine;
using System.Collections;

public class GameDefine {

    // CommonRoot
    public const string COMMON_ANIMATORES_ROOT      = "Animatores/";    //!< アニメーションコントローラー
    public const string COMMON_ATLUSES_ROOT         = "Atluses/";       //!< アトラス
    public const string COMMON_MASTER_DATAS_ROOT    = "MasterDatas/";   //!< マスタデータ
    public const string COMMON_PREFABS_ROOT         = "Prefabs/";       //!< プレハブ
    public const string COMMON_SOUND_ROOT           = "Sounds/";        //!< 音源系大本
    public const string COMMON_SCENES_ROOT          = "Scenes/";        //!< シーン大本
    public const string COMMON_TEXTURE_ROOT         = "Textures/";      //!< テクスチャ
    
    // 各種パス
    public const string SOUND_BGM_PATH = COMMON_SOUND_ROOT + "BGM/";    //!< BGM
    public const string SOUND_SE_PATH = COMMON_SOUND_ROOT + "SE/";      //!< SE
    public const string SCENE_RELECE_PATH   = COMMON_SCENES_ROOT + "Relese/";   //!< リリース用シーン
    public const string SCENE_MOCK_PATH     = COMMON_SCENES_ROOT + "Mock/";     //!< モック用シーン

	// シーン名
    public const string MOCK_COMPOSITION_TEST = "compositionText";  //!< 合成テストシーン
}
