using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDefine {

    // itemCategoraize
    public static string[] CATEGORAIZE_ATLAS_NAME = new string[]{
        "NONE",
        "material",
        "tresure",
        "animal",
        "material",
        "animal",
    };

    // CommonRoot
    public const string COMMON_ANIMATORES_ROOT      = "Animatores/";    //!< アニメーションコントローラー
    public const string COMMON_ATLASES_ROOT         = "Atlases/";       //!< アトラス
    public const string COMMON_MASTER_DATAS_ROOT    = "MasterDatas/";   //!< マスタデータ
    public const string COMMON_PREFABS_ROOT         = "Prefabs/";       //!< プレハブ
    public const string COMMON_SOUND_ROOT           = "Sounds/";        //!< 音源系大本
    public const string COMMON_SCENES_ROOT          = "Scenes/";        //!< シーン大本
    public const string COMMON_TEXTURE_ROOT         = "Textures/";      //!< テクスチャ
    
    // 各種パス
    public const string COMMON_PREFABS_PATH = COMMON_PREFABS_ROOT + "common/";  //!< プレハブのcommonまでのパス
    public const string SOUND_BGM_PATH = COMMON_SOUND_ROOT + "BGM/";    //!< BGM
    public const string SOUND_SE_PATH = COMMON_SOUND_ROOT + "SE/";      //!< SE
    public const string SCENE_RELECE_PATH   = COMMON_SCENES_ROOT + "Relese/";   //!< リリース用シーン
    public const string SCENE_MOCK_PATH     = COMMON_SCENES_ROOT + "Mock/";     //!< モック用シーン

	// シーン名
    public const string MOCK_COMPOSITION_TEST = "compositionText";  //!< 合成テストシーン

    // 保存キー
    public const string SAVE_KEY_USER_COMPOSITION_COUNT = "save_key_user_composition_count";
    public const string SAVE_KEY_OPTION_BGM_VOLUME      = "save_key_option_bgm_volume";
    public const string SAVE_KEY_OPTION_SE_VOLUME       = "save_key_option_se_volume";
    public const string SAVE_KEY_COMPLETE_ITEM_NUM      = "save_key_complete_item_num";
}
