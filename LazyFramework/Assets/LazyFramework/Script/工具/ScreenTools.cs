/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ScreenTools
* 创建日期：2019-07-10 13:34:31
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace Lazy
{
    public static class ScreenTools
    {
        public static void ChangeScreenRatio(int x, int y, bool isFull=false)
        {
            Screen.SetResolution(x, y, isFull);
        }
    }
}

