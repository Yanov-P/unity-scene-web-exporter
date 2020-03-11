using Assets.Kanau.Utils;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Kanau {
    [Serializable]
    public class ExportSettings {
        public static ExportSettings Instance;

        [Header("A-Frame")]
        public AFrameSettings aframe;

        public SkySettings sky;
        public CameraSettings camera;
        public CursorSettings cursor;

        // 파일 저장 좌표, 포맷등등
        public DestinationSettings destination;

        public LogSettings log;

        public AnimationSettings animation;
    }

    public class DestinationSettings {
        public string extension;
        public SceneFormat format;

        public string rootPath;
        public string imageDirectory = "images";
        public string modelDirectory = "models";
    }

    [Serializable]
    public class LogSettings {
        public ReportLevel level = ReportLevel.All;
        public bool useConsole = false;
    }

    [Serializable]
    public class AnimationSettings
    {
        public Animation animation;
        public float timeStep = 0.1f;
    }

    [Serializable]
    public class AFrameSettings {
        public string title = "Hello world!";

        // https://aframe.io/releases/latest/aframe.min.js
		// fix a-frame version.
        public string libraryAddress = "https://aframe.io/releases/1.0.3/aframe.min.js";
        public bool enablePerformanceStatistics = false;

        public string exporterPath = "Assets/Kanau";

        public string templateHeadFilename = "template_head.txt";
        public string templateAppendFilename = "template_append.txt";
        public string templateEndFilename = "template_end.txt";

        public string TemplateHead { get { return GetTemplateContent(templateHeadFilename); } }
        public string TemplateAppend { get { return GetTemplateContent(templateAppendFilename); } }
        public string TemplateEnd { get { return GetTemplateContent(templateEndFilename); } }

        public enum IndentStyle {
            Space,
            Tab,
        }
        public IndentStyle indentStyle = IndentStyle.Space;
        public int indentSize = 2;

        string GetTemplateContent(string templateFile) {
#if UNITY_EDITOR
            TextAsset content = AssetDatabase.LoadAssetAtPath<TextAsset>(exporterPath + "/" + templateFile);
            return content.text;
#else
            return "";
#endif
        }
    }

    [Serializable]
    public class CursorSettings {
        public bool enabled = false;

        public bool fuse = false;
        public float maxDistance = 1000f;
        public float timeout = 1500f;
    }

    [Serializable]
    public class CameraSettings {
        public bool wasdControlsEnabled = true;
        public bool lookControlsEnabled = true;
    }

    [Serializable]
    public class SkySettings {
        public enum SkyMode {
            None,
            MainCameraBackground,
            Color,
            Texture,
        }

        public SkyMode mode = SkyMode.MainCameraBackground;

        public Color skyColor = Color.white;
        public Texture skyTexture;
        public float radius = 800;
    }
}
