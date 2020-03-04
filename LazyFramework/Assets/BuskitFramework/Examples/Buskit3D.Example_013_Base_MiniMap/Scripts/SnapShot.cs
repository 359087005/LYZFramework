
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestMono
* 创建日期：2018-12-18 13:56:48
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Com.Rainier.Buskit3D.Example_013_MiniMap
{
    /// <summary>
    /// 屏幕快照
    /// </summary>
    [ExecuteInEditMode, RequireComponent(typeof(UnityEngine.Camera))]
    public class SnapShot : MonoBehaviour
    {

        /// <summary>
        /// The width of the output snapshot in pixels (1 - 4096 px)
        /// </summary>
        public int resWidth = 2048;
        /// <summary>
        /// The height of the output snapshot in pixels  (1 - 4096 px)
        /// </summary>
        public int resHeight = 2048;
        /// <summary>
        /// The applied MSAA, possible values are 1,2,4 nad 8
        /// </summary>
        public int msaa = 1;
        [Space(7)]
        public string m_TakeSnapShot = "";

        private static string _folderPath = "/Scripts/Source/Buskit3D.Common.UI/MiniMap/Art/SnapShots/";

        public static string FolderPath { get { return SnapShot._folderPath; } }

        private string SnapshotName(int width, int height)
        {
            string levelName = Application.loadedLevelName;

            //if in editor, we have to get the name through editor
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                string[] path = EditorApplication.currentScene.Split(char.Parse("/"));
                string[] fileName = path[path.Length - 1].Split(char.Parse("."));
                levelName = fileName[0];
            }
#endif

            return string.Format(GetFullFolderPath() + "MapSnapshot_{0}_{1}x{2}_{3}.png", levelName, width, height, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetFullFolderPath()
        {
            return Application.dataPath + _folderPath;
        }

        /// <summary>
        /// Takes a map snapshot and saves it
        /// </summary>
        [ContextMenu("Take Snap Shot")]
        public void TakeSnapshot()
        {
            //TODO fix 
#if UNITY_EDITOR && !UNITY_WEBPLAYER
            //setup rendertexture
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            rt.antiAliasing = msaa;
            rt.filterMode = FilterMode.Trilinear;
            GetComponent<UnityEngine.Camera>().targetTexture = rt;

            //render the texture
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            GetComponent<UnityEngine.Camera>().Render();
            RenderTexture.active = rt;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            GetComponent<UnityEngine.Camera>().targetTexture = null;
            RenderTexture.active = null;
            snapshot.alphaIsTransparency = true;
            byte[] bytes = snapshot.EncodeToPNG();
            DestroyImmediate(rt);
            DestroyImmediate(snapshot);

            //save to the file
            if (!System.IO.Directory.Exists(GetFullFolderPath()))
            {
                Debug.LogError("File path: " + GetFullFolderPath() + " doesn't exist! Create it.");
                return;
            }

            string _fileName = SnapshotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(_fileName, bytes);
            Debug.Log(string.Format("Saved snapshot to: {0}", _fileName), this);
            _fileName = "";
            AssetDatabase.Refresh();
#endif
        }
    }
}