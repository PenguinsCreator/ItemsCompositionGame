using UnityEngine;
using System;
using System.Collections;
using System.Security.Cryptography;

public class CommonUtil {

    /// <summary>
    /// プレハブインスタンス
    /// </summary>
    /// <param name="objName">オブジェクト名</param>
    /// <param name="objPath">オブジェクトパス</param>
    /// <param name="parent">親オブジェクト</param>
    /// <returns>GameObject</returns>
    public static GameObject PrefabInstance(string objName, string objPath, Transform parent)
    {
        GameObject obj = PrefabInstance(objPath, parent);
        obj.name = objName;
        return obj;
    }

    /// <summary>
    /// プレハブインスタンス
    /// </summary>
    /// <param name="objPath">オブジェクトパス</param>
    /// <param name="parent">親オブジェクト</param>
    /// <returns>GameObject</returns>
    public static GameObject PrefabInstance(string objPath, Transform parent = null)
    {
        if (objPath == null) {
            return null;
        }
        GameObject obj = GameObject.Instantiate(Resources.Load(objPath)) as GameObject;
        if (obj != null && parent != null) {
            obj.transform.parent = parent;
        }
        return obj;
    }

    /// <summary>
    /// オブジェクトを検索する
    /// </summary>
    /// <param name="objectName">探したいオブジェクト名</param>
    /// <param name="trans">探したいオブジェクトが存在するTransform</param>
    /// <param name="count">探索個数</param>
    /// <param name="countNum">何番目を引っ掛けるか</param>
    /// <returns></returns>
    public static GameObject SearchObject(string objectName, Transform trans, int count, int countNum){
        if (trans.gameObject.name.Equals(objectName)) {
            if (count >= countNum) {
                return trans.gameObject;
            }
            count++;
        }
        foreach (Transform child in trans) {
            GameObject obj = SearchObject(objectName, child, count, countNum);
            if (obj != null) {
                return obj;
            }
        }
        return null;
    }


    /// <summary>
    /// 子オブジェクトを探索する
    /// </summary>
    /// <param name="objectName">探したいオブジェクト名</param>
    /// <param name="parent">探索したいオブジェクトが存在するTransform</param>
    /// <param name="countNum">何番目を引っ掛けるか</param>
	public static GameObject SearchObjectChild(string objectName, Transform parent = null, int countNum = 0){
        int count = 0;
        if(parent == null){
            return GameObject.Find(objectName);
        }
        foreach (Transform child in parent) {
            GameObject obj = SearchObject(objectName, child, count, countNum);
            if (obj != null) {
                return obj;
            }
        }
        return null;
    }

    /// <summary>
    /// GameObjectにTextureをセット
    /// </summary>
    /// <param name="texture">Texture2D</param>
    /// <param name="objName">オブジェクト名</param>
    /// <returns></returns>
    public static GameObject SetTexture(Texture2D texture, string objName) {
        GameObject obj = SearchObjectChild(objName);
        if (obj == null) {
            obj = PrefabInstance(objName, "Prefabs/Common/" + objName, null);
            if (obj == null) {
                obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(0, 0, 0);
                obj.transform.localScale = new Vector3(1024, 768, 0);
            }
        }
        return obj;
    }

    /// <summary>
    /// GameObjectにTextureをセット
    /// </summary>
    /// <param name="texture">Texture2D</param>
    /// <param name="obj">オブジェクト名</param>
    public static void SetTexture(Texture2D texture, GameObject obj) {
        if (obj == null) { return; }
        obj.renderer.material.mainTexture = texture;
    }

    /// <summary>
    /// ランダムで抽選した値をひとつ返す
    /// </summary>
    /// <typeparam name="T">使用する型</typeparam>
    /// <param name="value">使用する配列</param>
    /// <returns>配列の中の値の一つ</returns>
    public static T RandPickOne<T>(T[] value) {
        return value[CreateRandomNumber(value.Length)];
    }

    /// <summary>
    /// ランダムで抽選した値の配列を返す
    /// </summary>
    /// <typeparam name="T">使用する型</typeparam>
    /// <param name="value">使用する配列</param>
    /// <param name="len">戻す長さ</param>
    /// <returns>指定長の配列</returns>
    public static T[] RandPickUp<T>(T[] value, int len) {
        // 型の配列を指定長で生成
        T[] array = new T[len];
        for (int ii = 0; ii < len; ii++) {
            array[ii] = RandPickOne<T>(value);
        }
        return array;
    }

    /// <summary>
    /// ランダム数値を作成
    /// </summary>
    /// <param name="max">ランダムの最大値</param>
    /// <returns>ランダム値</returns>
    public static int CreateRandomNumber(int max) {
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        byte[] rand = new byte[max];
        rng.GetBytes(rand);
        // 配列の最大長で乱数を生成
        return 1 + (int)((max - 1) * (BitConverter.ToUInt32(rand, 0) / ((double)uint.MaxValue + 1.0)));
    }

    /// <summary>
    /// 最低値と最大値の間からランダム数値を作成
    /// </summary>
    /// <param name="min">ランダムの最低値</param>
    /// <param name="max">ランダムの最大値</param>
    /// <returns>ランダム値</returns>
    public static int CreateRandomNumber(int min, int max) {
        int value = 0;
        // 同値だったら最低値を返す
        if (min == max) { return min; }
        if (min > max)  { return max; }
        do {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] rand = new byte[max];
            rng.GetBytes(rand);
            // 配列の最大長で乱数を生成
            value = 1 + (int)((max - 1) * (BitConverter.ToUInt32(rand, 0) / ((double)uint.MaxValue + 1.0)));
        } while (value < min);
        return value;
    }
}
