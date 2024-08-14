using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;


public class SavePlayDataConverter : JsonConverter<SavePlayData>
{
    public override SavePlayData ReadJson(JsonReader reader, Type objectType, SavePlayData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var data = new SavePlayData();
        JObject jObj = JObject.Load(reader);
        data.instanceId = (int)jObj["instanceId"];
        return data;
    }


    public override void WriteJson(JsonWriter writer, SavePlayData value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("instanceId");
        writer.WriteValue(value.instanceId);
        writer.WriteEndObject();
    }

    public class Vector3Converter : JsonConverter<Vector3>
    {

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Vector3 v = Vector3.zero;
            JObject jObj = JObject.Load(reader);
            v.x = (float)jObj["X"];
            v.y = (float)jObj["Y"];
            v.z = (float)jObj["Z"];
            return v;
        }

        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("X");
            writer.WriteValue(value.x);
            writer.WritePropertyName("Y");
            writer.WriteValue(value.y);
            writer.WritePropertyName("Z");
            writer.WriteValue(value.z);
            writer.WriteEndObject();
        }
    }


    public class QuaternionConverter : JsonConverter<Quaternion>
    {
        public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Quaternion rot = Quaternion.identity;
            JObject jObj = JObject.Load(reader);
            rot.x = (float)jObj["X"];
            rot.y = (float)jObj["Y"];
            rot.z = (float)jObj["Z"];
            rot.w = (float)jObj["W"];
            return rot;
        }

        public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("X");
            writer.WriteValue(value.x);
            writer.WritePropertyName("Y");
            writer.WriteValue(value.y);
            writer.WritePropertyName("Z");
            writer.WriteValue(value.z);
            writer.WritePropertyName("W");
            writer.WriteValue(value.w);
            writer.WriteEndObject();
        }
    }

    public class ColorConverter : JsonConverter<Color>
    {
        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Color color;
            JObject jObj = JObject.Load(reader);
            color.r = (float)jObj["R"];
            color.g = (float)jObj["G"];
            color.b = (float)jObj["B"];
            color.a = (float)jObj["A"];
            return color;
        }

        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("R");
            writer.WriteValue(value.r);
            writer.WritePropertyName("G");
            writer.WriteValue(value.g);
            writer.WritePropertyName("B");
            writer.WriteValue(value.b);
            writer.WritePropertyName("A");
            writer.WriteValue(value.a);
            writer.WriteEndObject();
        }
    }


    public class BigIntegerConverter : JsonConverter<BigInteger>
    {

        public override BigInteger ReadJson(JsonReader reader, Type objectType, BigInteger existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObj = JObject.Load(reader);

            BigInteger b = new BigInteger
            {
                factor = (int)jObj["factor"],
                numberList = jObj["numberList"].ToObject<List<int>>()
            };

            return b;
        }

        public override void WriteJson(JsonWriter writer, BigInteger value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("factor");
            writer.WriteValue(value.factor);
            writer.WritePropertyName("numberList");
            writer.WriteValue(value.numberList);
            writer.WriteEndObject();
        }
    }

    public class EquipDataConverter : JsonConverter<EquipData>
    {

        public override EquipData ReadJson(JsonReader reader, Type objectType, EquipData existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            EquipData data = new EquipData();
            JObject jObj = JObject.Load(reader);
            data.equipmentID = (int)jObj["equipmentID"];
            data.equipmenttype = (int)jObj["equipmenttype"];
            data.equipment_rating = (string)jObj["equipment_rating"];
            data.equipment_esset = (string)jObj["equipment_esset"];
            data.reinforcement_value = (int)jObj["reinforcement_value"];
            data.item_name_id = (int)jObj["item_name_id"];
            data.option_value = (int)jObj["option_value"];
            data.option_id = (int)jObj["option_id"];
            data.item_icon = (string)jObj["item_icon"];

            return data;
        }

        public override void WriteJson(JsonWriter writer, EquipData value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("equipmentID");
            writer.WriteValue(value.equipmentID);
            writer.WritePropertyName("equipmenttype");
            writer.WriteValue(value.equipmenttype);
            writer.WritePropertyName("equipment_rating");
            writer.WriteValue(value.equipment_rating);
            writer.WritePropertyName("equipment_esset");
            writer.WriteValue(value.equipment_esset);
            writer.WritePropertyName("reinforcement_value");
            writer.WriteValue(value.reinforcement_value);
            writer.WritePropertyName("item_name_id");
            writer.WriteValue(value.item_name_id);
            writer.WritePropertyName("option_value");
            writer.WriteValue(value.option_value);
            writer.WritePropertyName("option_id");
            writer.WriteValue(value.option_id);
            writer.WritePropertyName("item_icon");
            writer.WriteValue(value.item_icon);
            writer.WriteEndObject();
        }
    }


    public class SkillBallConverter : JsonConverter<SkillBallController>
    {

        public override SkillBallController ReadJson(JsonReader reader, Type objectType, SkillBallController existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            SkillBallController data = new SkillBallController();
            JObject jObj = JObject.Load(reader);
            data.tier = (int)jObj["tier"];
            //JArray areaRectArray = (JArray)jObj["areaRect"];
            //data.anchoredPos = new Vector2(areaRectArray[0].Value<float>(), areaRectArray[1].Value<float>());
            data.anchoredPos = new Vector2((float)jObj["areaRectX"], (float)jObj["areaRectY"]) ;
            data.skill_ID = (int)jObj["skill_ID"];

            return data;
        }

        public override void WriteJson(JsonWriter writer, SkillBallController value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("tier");
            writer.WriteValue(value.tier);
            value.anchoredPos = new Vector2(value.areaRect.anchoredPosition.x, value.areaRect.anchoredPosition.y);

            writer.WritePropertyName("areaRectX");
            writer.WriteValue(value.anchoredPos.x);
            writer.WritePropertyName("areaRectY");
            writer.WriteValue(value.anchoredPos.y);
            //writer.WriteStartArray();
            //writer.WriteValue(value.anchoredPos.x);
            //writer.WriteValue(value.anchoredPos.y);
            //writer.WriteEndArray();
            writer.WritePropertyName("skill_ID");
            writer.WriteValue(value.skill_ID);
            writer.WriteEndObject();
        }
    }
}

