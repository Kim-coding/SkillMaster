using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using static SavePlayDataConverter;

public class SaveLoadSystem
{
    private static readonly byte[] Key = Convert.FromBase64String("ZgjtMN69XqjbfyS22uDnrlohWL9O6rwQSM/ykpSsVko="); // 32 bytes for AES-256
    private static readonly byte[] IV = Convert.FromBase64String("7Tyf5sE7xYNCPBRY3A82Cg==");   // 16 bytes for AES
    public enum Mode
    {
        Json,
        Binary,
        EncryptedBinary,
    }


    public static Mode FileMode { get; set; } = Mode.Json;

    public static int SaveDataVersion { get; private set; } = 1;

    // 0 (자동), 1, 2, 3 ...
    private static readonly string SaveFileName = "SaveAuto.sav";

    static SaveLoadSystem()
    {
        if (!Load())
        {
            CurrSaveData = new SaveDataV1();
        }
    }

    public static SaveDataV1 CurrSaveData { get; set; }

    private static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}/Save";
        }
    }

    public static bool Save()
    {

        CurrSaveData.savePlay = new SavePlayData();
        if(GameMgr.Instance.sceneMgr.mainScene != null)
        {
            CurrSaveData.savePlay.tutorialID = GameMgr.Instance.uiMgr.uiTutorial.currentTutorialID;
            CurrSaveData.savePlay.tutorialIndex = GameMgr.Instance.uiMgr.uiTutorial.currentTutorialIndex;
            CurrSaveData.savePlay.stageId = GameMgr.Instance.sceneMgr.mainScene.stageId;
            CurrSaveData.savePlay.questID = GameMgr.Instance.rewardMgr.guideQuest.questID;
            CurrSaveData.savePlay.questValue = GameMgr.Instance.rewardMgr.guideQuest.currentTargetValue;
        }
        else
        {
            CurrSaveData.savePlay.tutorialID = GameMgr.Instance.sceneMgr.dungeonScene.tutorialId;
            CurrSaveData.savePlay.tutorialIndex = GameMgr.Instance.sceneMgr.dungeonScene.tutorialIndex;
            CurrSaveData.savePlay.stageId = GameMgr.Instance.sceneMgr.dungeonScene.stageId;
            CurrSaveData.savePlay.questID = GameMgr.Instance.sceneMgr.dungeonScene.questId;
            CurrSaveData.savePlay.questValue = GameMgr.Instance.sceneMgr.dungeonScene.questValue;
        }
        CurrSaveData.savePlay.saveCurrency = GameMgr.Instance.playerMgr.currency;
        CurrSaveData.savePlay.savePlayerEnhance = GameMgr.Instance.playerMgr.playerEnhance;
        CurrSaveData.savePlay.savePlayerInfomation = GameMgr.Instance.playerMgr.playerInfo;
        CurrSaveData.savePlay.savePlayerInventory = GameMgr.Instance.playerMgr.playerinventory;

        foreach (var data in GameMgr.Instance.playerMgr.skillBallControllers)
        {
            CurrSaveData.savePlay.saveSkillBallControllers.Add(data);
        }

        CurrSaveData.savePlay.rewardID = GameMgr.Instance.rewardMgr.stageID;

        //foreach (var data in GameMgr.Instance.playerMgr.playerInfo.skillBookDatas)
        //{
        //    CurrSaveData.savePlay.savePlayerInfomation.skillBookDatas.Add(data.Key,data.Value);
        //}


        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, SaveFileName);
        // FileMode 분기

        using (var writer = new JsonTextWriter(new StreamWriter(path)))
        {
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Converters.Add(new EquipDataConverter());
            serializer.Converters.Add(new Vector3Converter());
            serializer.Converters.Add(new NormalItemDataConverter());
            serializer.Converters.Add(new SkillBallConverter());

            serializer.Serialize(writer, CurrSaveData);
        }

        //string jsonData = JsonConvert.SerializeObject(CurrSaveData, new JsonSerializerSettings
        //{
        //    Formatting = Formatting.Indented,
        //    TypeNameHandling = TypeNameHandling.All
        //});
        //byte[] encryptedData = EncryptStringToBytes_Aes(jsonData, Key, IV);
        //File.WriteAllBytes(path, encryptedData);

        return true;
    }

    private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException(nameof(plainText));
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException(nameof(IV));

        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        return encrypted;
    }

    private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException(nameof(cipherText));
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException(nameof(IV));

        string plaintext;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }

    public static object LoadData()
    {
        var path = Path.Combine(SaveDirectory, SaveFileName);
        if (!File.Exists(path))
        {
            return null;
        }

        // 파일에서 암호화된 데이터를 읽음
        byte[] encryptedData = File.ReadAllBytes(path);

        // 데이터를 복호화
        string jsonData = DecryptStringFromBytes_Aes(encryptedData, Key, IV);

        // JSON 데이터를 객체로 역직렬화하여 반환
        return JsonConvert.DeserializeObject(jsonData, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
    }


    public static bool Load()
    {
        var path = Path.Combine(SaveDirectory, SaveFileName);
        if (!File.Exists(path))
        {
            return false;
        }

        //CurrSaveData = (SaveDataV1)LoadData();

        SaveData data = null;
        using (var reader = new JsonTextReader(new StreamReader(path)))
        {
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Converters.Add(new EquipDataConverter());
            serializer.Converters.Add(new Vector3Converter());
            serializer.Converters.Add(new NormalItemDataConverter());
            serializer.Converters.Add(new SkillBallConverter());
            data = serializer.Deserialize<SaveData>(reader);
        }

        while (data.Version < SaveDataVersion)
        {
            data = data.VersionUp();
        }

        CurrSaveData = data as SaveDataV1;

        return true;
    }
    public static void DeleteSaveData()
    {
        File.Delete(Path.Combine(SaveDirectory, SaveFileName));
    }

}
