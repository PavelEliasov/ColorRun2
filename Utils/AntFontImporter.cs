#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Простой конвертер растровых шрифтов в шрифт Unity
/// -------------------------------------------------
///
/// AntFontImporter — это скрипт позволяющий быстро конвертировать растровые шрифты
/// в формате *.png + *.fnt в шрифт Unity.
///
/// Растровый шрифт можно сгенерировать любым доступным генератором шрифтов, например:
/// * ShoeBox - (ссылка)
/// * Littera - (ссылка)
///
/// Автор: AntKarlov (ant-karlov.ru)
/// Дата: 08.08.2016
///
/// Установка и использование скрипта:
/// 1. Скопировать скрипт AntFontImporter.cs в папку вашего проекта (например, в Scripts).
/// 2. Скопировать сгенерированный шрифт в папку вашего проекта.
/// 3. В окне проекта выбрать файл вашего шрифта с расширением *.fnt и вызвать контекстное меню к нему.
/// 4. В контекстном меню выбрать пункт "Convert FNT to Font".
/// 5. После успешного завершения работы шрифта появятся два новых файла: материал шрифта и сам шрифт.
/// 6. Установите полученный материал шрифта и сам шрифт для любого текстового поля Unity GUI.
/// 7. Успешной разработки!
///
/// Подробнее о растровых шрифтах в Unity: (ссылка)
/// </summary>
public static class AntFontImporter
{
	[MenuItem("Assets/Convert FNT to Font")]
	public static void ImportBitmapFont()
	{
		TextAsset selected = Selection.activeObject as TextAsset;
		if (selected != null)
		{
			string rootDir = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selected));
			string fileName = Path.GetFileNameWithoutExtension(selected.name);

			// Чтение данных из текстового файла шрифта.
			Debug.Log("AntFontImporter: Reading FNT data...");
			AntFont fontData = new AntFont(selected.text);

			// Создание текстуры.
			string texFileName = rootDir + "/" + fontData["page"]["file"].Value;
			Debug.Log("AntFontImporter: Creating Texture2D from \"" + texFileName + "\" image...");
			Texture2D texture = CreateTexture(texFileName);

			// Создание символов.
			Debug.Log("AntFontImporter: Initializing font chars...");
			CharacterInfo[] chars = CreateCharacters(fontData, new Vector2(texture.width, texture.height));

			// Создание материала.
			string matFileName = rootDir + "/" + fileName + ".mat";
			Debug.Log("AntFontImporter: Creating Material and saving it to \"" + matFileName + "\"...");
			Material material = CreateMaterial(texture);
			AssetDatabase.CreateAsset(material, matFileName);

			// Создание шрифта.
			string fontFileName = rootDir + "/" + fileName + ".fontsettings";
			Debug.Log("AntFontImporter: Creating Font Settings and saving it to \"" + fontFileName + "\"...");
			Font font = CreateFont(fontData, material, chars);
			AssetDatabase.CreateAsset(font, fontFileName);

			Debug.Log("AntFontImporter: BitmapFont is done!");
		}
	}

	private static Font CreateFont(AntFont aFontData, Material aMaterial, CharacterInfo[] aCharacters)
	{
		Font font = new Font();
		font.material = aMaterial;
		font.name = aFontData["info"]["face"].Value;
		font.characterInfo = aCharacters;

		// Изменение значений доступных только для чтения... ;)
		SerializedObject objFont = new SerializedObject(font);
		objFont.FindProperty("m_FontSize").floatValue = 0.0f;
		objFont.FindProperty("m_LineSpacing").floatValue = aFontData["common"]["lineHeight"].AsFloat;

		List<AntFontItem> kerningList = aFontData.GetAllBy("kerning");
		if (kerningList.Count > 0)
		{
			SerializedProperty kerningProp = objFont.FindProperty("m_KerningValues");
			for (int i = 0; i < kerningList.Count; i++)
			{
				kerningProp.InsertArrayElementAtIndex(i);
				SerializedProperty kern = kerningProp.GetArrayElementAtIndex(i);
				kern.FindPropertyRelative("second").floatValue = kerningList[i]["amount"].AsFloat;
			}
		}

		objFont.ApplyModifiedProperties();
		return font;
	}

	private static Material CreateMaterial(Texture2D aTexture)
	{
		Shader shader = Shader.Find ("UI/Default");
		Material material = new Material (shader);
		material.mainTexture = aTexture;
		return material;
	}

	private static CharacterInfo[] CreateCharacters(AntFont aFontData, Vector2 aTextureSize)
	{
		var charsList = aFontData.GetAllBy("char");
		Rect rect = new Rect();
		AntFontItem curChar;
		CharacterInfo info;
		CharacterInfo[] result = new CharacterInfo[charsList.Count];
		for (int i = 0; i < charsList.Count; i++)
		{
			curChar = charsList[i];

			// Создание информации о символе.
			info = new CharacterInfo();
			info.index = curChar["id"].AsInt;
			info.advance = curChar["xadvance"].AsInt;

			// Чтение данных и рассчет UV координат для символа.
			rect.x = curChar["x"].AsFloat / aTextureSize.x;
			rect.y = curChar["y"].AsFloat / aTextureSize.y;
			rect.width = curChar["width"].AsFloat / aTextureSize.x;
			rect.height = curChar["height"].AsFloat / aTextureSize.y;
			rect.y = 1f - rect.y - rect.height;

			// Применение расчитанных UV координат к символу.
			info.uvBottomLeft = new Vector2(rect.xMin, rect.yMin);
			info.uvBottomRight = new Vector2(rect.xMax, rect.yMin);
			info.uvTopLeft = new Vector2(rect.xMin, rect.yMax);
			info.uvTopRight = new Vector2(rect.xMax, rect.yMax);

			// Чтение данных и рассчет смещений для символа.
			rect.x = curChar["xoffset"].AsFloat;
			rect.y = curChar["yoffset"].AsFloat;
			rect.width = curChar["width"].AsFloat;
			rect.height = curChar["height"].AsFloat;
			rect.y = -rect.y;
			rect.height = -rect.height;

			// Применение расчитанных смещений к символу.
			info.minX = Mathf.RoundToInt(rect.xMin);
			info.maxX = Mathf.RoundToInt(rect.xMax);
			info.minY = Mathf.RoundToInt(rect.yMax);
			info.maxY = Mathf.RoundToInt(rect.yMin);

			result[i] = info;
		}

		return result;
	}

	private static Texture2D CreateTexture(string aFileName)
	{
		Texture2D texture = AssetDatabase.LoadAssetAtPath(aFileName, typeof(Texture2D)) as Texture2D;
		if (texture == null)
		{
			throw new UnityException("AntFontImporter: Bitmap \"" + aFileName + "\" not found.");
		}

		return texture;
	}

	#region Helpers for parsing plain text format
	public class AntFont
	{
		private List<AntFontItem> _items;

		public AntFont(string aData)
		{
			_items = new List<AntFontItem>();
			AntFontItem item;
			string[] lines = Regex.Split(aData, "\n|\r|\r\n");
			if (lines.Length > 0)
			{
				string[] header = Regex.Split(lines[0], " ");
				if (header.Length > 0 && header[0] == "info")
				{
					for (int i = 0; i < lines.Length; i++)
					{
						item = new AntFontItem(lines[i]);
						_items.Add(item);
					}
				}
				else
				{
					throw new UnityException("AntFontImporter: Selected file is not FNT data.");
				}
			}
			else
			{
				throw new UnityException("AntFontImporter: Selected file is empty.");
			}
		}

		public bool Contains(string aKey)
		{
			return (Get(aKey) != null);
		}

		public List<AntFontItem> GetAllBy(string aKey)
		{
			var result = new List<AntFontItem>();
			for (int i = 0; i < _items.Count; i++)
			{
				if (_items[i].Key == aKey)
				{
					result.Add(_items[i]);
				}
			}

			return result;
		}

		public AntFontItem Get(string aKey)
		{
			for (int i = 0; i < _items.Count; i++)
			{
				if (_items[i].Key == aKey)
				{
					return _items[i];
				}
			}

			return null;
		}

		public AntFontItem this[string aKey]
		{
			get { return Get(aKey); }
		}
	}

	public class AntFontItem
	{
		public string Key { get; private set; }
		private Dictionary<string, AntFontData> _values;

		public AntFontItem(string aData)
		{
			Key = null;
			_values = new Dictionary<string, AntFontData>();
			string token = "";
			bool quoteMode = false;
			for (int i = 0; i < aData.Length; i++)
			{
				switch (aData[i])
				{
					case ' ' :
						if (quoteMode)
						{
							token += aData[i];
							break;
						}

						if (Key == null)
						{
							Key = token;
							token = "";
						}
						else
						{
							AddData(token);
							token = "";
						}
					break;

					case '"' :
						token += aData[i];
						quoteMode = !quoteMode;
					break;

					default :
						token += aData[i];
					break;
				}
			}

			if (token != "")
			{
				AddData(token);
			}
		}

		private void AddData(string aData)
		{
			var data = new AntFontData(aData);
			if (data.Key != null)
			{
				_values.Add (data.Key, data);
			}
		}

		public AntFontData this[string aKey]
		{
			get
			{
				if (!_values.ContainsKey(aKey))
				{
					Debug.LogWarning("AntFontImporter: Property with key \"" + aKey + "\" not found!");
					return new AntFontData(aKey + "=");
				}

				return _values[aKey];
			}
		}
	}

	public class AntFontData
	{
		public string Key { get; private set; }
		public string Value { get; private set; }

		public AntFontData(string aData)
		{
			string token = "";
			bool quoteMode = false;
			for (int i = 0; i < aData.Length; i++)
			{
				switch (aData[i])
				{
					case '=' :
						if (quoteMode)
						{
							token += aData[i];
							break;
						}

						Key = token;
						token = "";
					break;

					case '/' :
					case '\\' :
						if (quoteMode)
						{
							token = "";
						}
					break;

					case '"' :
						quoteMode = !quoteMode;
					break;

					default:
						token += aData[i];
					break;
				}
			}

			Value = token;
		}

		public int AsInt
		{
			get
			{
				int v = 0;
				return (int.TryParse(Value, out v)) ? v : 0;
			}
		}

		public float AsFloat
		{
			get
			{
				float v = 0.0f;
				return (float.TryParse(Value, out v)) ? v : 0.0f;
			}
		}
	}
	#endregion Helpers for parsing plain text format
}
#endif