using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
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
        if (GameMgr.Instance.uiMgr.uiTutorial != null &&
            GameMgr.Instance.uiMgr.uiTutorial.gameObject.activeSelf)
        {
            return false;
        }

        CurrSaveData.savePlay = new SavePlayData();
        if (GameMgr.Instance.sceneMgr.mainScene != null)
        {
            CurrSaveData.savePlay.tutorialID = GameMgr.Instance.uiMgr.uiTutorial.currentTutorialID;
            CurrSaveData.savePlay.tutorialIndex = GameMgr.Instance.uiMgr.uiTutorial.currentTutorialIndex;
            CurrSaveData.savePlay.isDungeonOpen = GameMgr.Instance.uiMgr.uiTutorial.isDungeonOpen;
            CurrSaveData.savePlay.stageId = GameMgr.Instance.sceneMgr.mainScene.stageId;
            CurrSaveData.savePlay.questID = GameMgr.Instance.rewardMgr.guideQuest.questID;
            CurrSaveData.savePlay.questValue = GameMgr.Instance.rewardMgr.guideQuest.currentTargetValue;
            CurrSaveData.savePlay.rewardID = GameMgr.Instance.sceneMgr.mainScene.appearBossMonster;
            CurrSaveData.savePlay.isStory = GameMgr.Instance.uiMgr.isStory;
            CurrSaveData.savePlay.userName = GameMgr.Instance.uiMgr.userNameText.text;
        }
        else
        {
            CurrSaveData.savePlay.tutorialID = GameMgr.Instance.sceneMgr.dungeonScene.tutorialId;
            CurrSaveData.savePlay.tutorialIndex = GameMgr.Instance.sceneMgr.dungeonScene.tutorialIndex;
            CurrSaveData.savePlay.isDungeonOpen = GameMgr.Instance.sceneMgr.dungeonScene.isDungeonOpen;
            CurrSaveData.savePlay.stageId = GameMgr.Instance.sceneMgr.dungeonScene.stageId;
            CurrSaveData.savePlay.questID = GameMgr.Instance.sceneMgr.dungeonScene.questId;
            CurrSaveData.savePlay.rewardID = GameMgr.Instance.sceneMgr.dungeonScene.rewardID;
            CurrSaveData.savePlay.questValue = GameMgr.Instance.sceneMgr.dungeonScene.questValue;
            CurrSaveData.savePlay.isStory = GameMgr.Instance.sceneMgr.dungeonScene.isStory;
            CurrSaveData.savePlay.userName = GameMgr.Instance.sceneMgr.dungeonScene.userName;
        }
        CurrSaveData.savePlay.saveCurrency = GameMgr.Instance.playerMgr.currency;
        CurrSaveData.savePlay.savePlayerEnhance = GameMgr.Instance.playerMgr.playerEnhance;
        CurrSaveData.savePlay.savePlayerInfomation = GameMgr.Instance.playerMgr.playerInfo;
        CurrSaveData.savePlay.savePlayerInventory = GameMgr.Instance.playerMgr.playerinventory;

        foreach (var data in GameMgr.Instance.playerMgr.skillBallControllers)
        {
            CurrSaveData.savePlay.saveSkillBallControllers.Add(data);
        }
        //foreach (var data in GameMgr.Instance.playerMgr.playerInfo.skillBookDatas)
        //{
        //    CurrSaveData.savePlay.savePlayerInfomation.skillBookDatas.Add(data.Key,data.Value);
        //}

        CurrSaveData.savePlay.bgm = GameMgr.Instance.soundMgr.bgmSlider.value;
        CurrSaveData.savePlay.sfx = GameMgr.Instance.soundMgr.sfxSlider.value;

        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, SaveFileName);
        // FileMode 분기

        //using (var writer = new JsonTextWriter(new StreamWriter(path)))
        //{
        //    var serializer = new JsonSerializer();
        //    serializer.Formatting = Formatting.Indented;
        //    serializer.TypeNameHandling = TypeNameHandling.All;
        //    serializer.Converters.Add(new EquipDataConverter());
        //    serializer.Converters.Add(new Vector3Converter());
        //    serializer.Converters.Add(new NormalItemDataConverter());
        //    serializer.Converters.Add(new SkillBallConverter());

        //    serializer.Serialize(writer, CurrSaveData);
        //}

        var serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All
        };
        serializerSettings.Converters.Add(new EquipDataConverter());
        serializerSettings.Converters.Add(new Vector3Converter());
        serializerSettings.Converters.Add(new NormalItemDataConverter());
        serializerSettings.Converters.Add(new SkillBallConverter());


        string jsonData = JsonConvert.SerializeObject(CurrSaveData, serializerSettings);
        byte[] encryptedData = EncryptStringToBytes_Aes(jsonData, Key, IV);
        File.WriteAllBytes(path, encryptedData);

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

        string jsonData = null;
        bool decryptionFailed = false;

        try
        {
            // 데이터를 복호화
            jsonData = DecryptStringFromBytes_Aes(encryptedData, Key, IV);
        }
        catch (CryptographicException)
        {
            decryptionFailed = true;
        }
        catch (Exception ex)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        if (decryptionFailed)
        {
            SaveData savedata = null;
            using (var reader = new JsonTextReader(new StreamReader(path)))
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.TypeNameHandling = TypeNameHandling.All;
                serializer.Converters.Add(new EquipDataConverter());
                serializer.Converters.Add(new Vector3Converter());
                serializer.Converters.Add(new NormalItemDataConverter());
                serializer.Converters.Add(new SkillBallConverter());
                savedata = serializer.Deserialize<SaveData>(reader);
            }

            return savedata as SaveDataV1;
        }


        var serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        serializerSettings.Converters.Add(new EquipDataConverter());
        serializerSettings.Converters.Add(new Vector3Converter());
        serializerSettings.Converters.Add(new NormalItemDataConverter());
        serializerSettings.Converters.Add(new SkillBallConverter());

        SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonData, serializerSettings);

        // JSON 데이터를 객체로 역직렬화하여 반환
        return data;
    }


    public static bool Load()
    {
        var path = Path.Combine(SaveDirectory, SaveFileName);
        if (!File.Exists(path))
        {
            return false;
        }

        CurrSaveData = (SaveDataV1)LoadData();

        //SaveData data = null;
        //using (var reader = new JsonTextReader(new StreamReader(path)))
        //{
        //    var serializer = new JsonSerializer();
        //    serializer.Formatting = Formatting.Indented;
        //    serializer.TypeNameHandling = TypeNameHandling.All;
        //    serializer.Converters.Add(new EquipDataConverter());
        //    serializer.Converters.Add(new Vector3Converter());
        //    serializer.Converters.Add(new NormalItemDataConverter());
        //    serializer.Converters.Add(new SkillBallConverter());
        //    data = serializer.Deserialize<SaveData>(reader);
        //}

        //while (data.Version < SaveDataVersion)
        //{
        //    data = data.VersionUp();
        //}

        //CurrSaveData = data as SaveDataV1;

        return true;
    }
    public static void DeleteSaveData()
    {
        File.Delete(Path.Combine(SaveDirectory, SaveFileName));
    }

}
