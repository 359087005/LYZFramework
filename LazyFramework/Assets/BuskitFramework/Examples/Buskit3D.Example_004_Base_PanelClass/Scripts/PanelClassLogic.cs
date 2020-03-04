/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：容器类功能的逻辑处理类
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_004_PanelClass
{
    /// <summary>
    /// 容器功能的业务逻辑类
    /// </summary>
    public class PanelClassLogic : LogicBehaviour
    {
        //记录打印的显示文本
        //public Text text;

        /// <summary>
        /// 业务逻辑方法
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            #region WatchableDictionary
            if (evt.PropertyName.Equals("dataDic#Add"))
            {
                KeyValuePair<int, string> value = (KeyValuePair<int, string>)evt.NewValue;
                Debug.Log("当前dataDic中添加了一组数据" + value);
                DebugText("dataDic#Add");
                DebugText("当前dataDic中添加了一组数据" + value);
            }
            
            if (evt.PropertyName.Equals("dataDic#[]"))
            {
                KeyValuePair<int, string> temp = (KeyValuePair<int, string>)evt.NewValue;
                Debug.Log("当前修改了dataDic的一条数据" + temp.Value);
                DebugText("dataDic#[]");
                DebugText("当前修改了dataDic中key值为 [" + temp.Key + "] 的数据,当前值为" + temp.Value);
            }

            if (evt.PropertyName.Equals("dataDic#Clear"))
            {
                Debug.Log("dataDic#Clear");
                DebugText("dataDic#Clear");
            }
            if (evt.PropertyName.Equals("dataDic#Remove"))
            {
                Debug.Log("dataDic#Remove");
                Debug.Log("当前删除了Key值为[" + evt.NewValue +"]的元素");
                DebugText("dataDic#Remove");
                DebugText("当前删除了Key值为[" + evt.NewValue + "]的元素");
            }
            #endregion

            #region WatchableArrayList
            if (evt.PropertyName.Equals("dataArrayList#Add"))
            {

                int value = System.Convert.ToInt32(evt.NewValue);
                Debug.Log("当前朝dataArrayList中添加了一组数据" + value);
                DebugText("dataArrayList#Add");
                DebugText("当前朝dataArrayList中添加了一组数据" + value);
            }
            if (evt.PropertyName.Equals("dataArrayList#Insert"))
            {
                KeyValuePair<int, object> value = (KeyValuePair<int, object>)evt.NewValue;
                Debug.Log("dataArrayList#Insert");
                Debug.Log("当前插入了" + value.Value);
                DebugText("dataList#Insert");
                DebugText("当前插入了dataList中下标为 [" + value.Key + "] 数据,当前值为" + value.Value);
            }
            if (evt.PropertyName.Equals("dataArrayList#[]"))
            {
                KeyValuePair<int, object> temp = (KeyValuePair<int, object>)evt.NewValue;
                Debug.Log("当前修改了dataArrayList的一条数据" + evt.NewValue);
                DebugText("dataArrayList#[]");
                DebugText("当前修改了dataArrayList中下标为 [" + temp.Key + "] 数据,当前值为" + temp.Value);
            }
            if (evt.PropertyName.Equals("dataArrayList#RemoveAt"))
            {

                Debug.Log("dataArrayList#RemoveAt");
                Debug.Log("当前删除元素的下标为" + evt.NewValue);
                DebugText("dataArrayList#RemoveAt");
                DebugText("当前删除元素的下标为" + evt.NewValue);
            }
            if (evt.PropertyName.Equals("dataArrayList#Remove"))
            {
                Debug.Log("dataArrayList#Remove");
                Debug.Log("当前删除了" + evt.NewValue);
                DebugText("dataArrayList#Remove");
                DebugText("当前删除了" + evt.NewValue);
            }
            if (evt.PropertyName.Equals("dataArrayList#Clear"))
            {
                Debug.Log("dataArrayList#Clear");
                DebugText("dataArrayList#Clear");
            }
            #endregion

            #region WatchableList
            if (evt.PropertyName.Equals("dataList#Add"))
            {
                int value = (int)evt.NewValue;
                Debug.Log("当前朝dataList中添加了一组数据" + value);
                DebugText("dataList#Add");
                DebugText("当前朝dataList中添加了一组数据" + value);
            }
            if (evt.PropertyName.Equals("dataList#[]"))
            {
                KeyValuePair<int, int> temp = (KeyValuePair<int, int>)evt.NewValue;
                Debug.Log("当前修改了dataList的一条数据" + temp.Value);
                DebugText("dataList#[]");
                DebugText("当前修改了dataList中下标为 [" + temp.Key + "] 数据,当前值为" + temp.Value);
            }
            if (evt.PropertyName.Equals("dataList#Remove"))
            {
                Debug.Log("dataList#Remove");
                Debug.Log("当前删除了" + evt.NewValue);
                DebugText("dataList#Remove");
                DebugText("当前删除了"+evt.NewValue);
            }


            if (evt.PropertyName.Equals("dataList#Insert"))
            {
                KeyValuePair<int, int> value = (KeyValuePair<int, int>)evt.NewValue;
                Debug.Log("dataList#Insert");
                Debug.Log("当前插入了" + value.Value);
                DebugText("dataList#Remove");
                DebugText("当前插入了dataList中下标为 [" + value.Key + "] 数据,当前值为" + value.Value);
            }


            if (evt.PropertyName.Equals("dataList#RemoveAt"))
            {
                
                Debug.Log("dataList#RemoveAt");
                Debug.Log("当前删除元素的下标为" + evt.NewValue);
                DebugText("dataList#RemoveAt");
                DebugText("当前删除元素的下标为" + evt.NewValue);
            }
            if (evt.PropertyName.Equals("dataList#Clear"))
            {
                Debug.Log("dataList#Clear");
                DebugText("dataList#Clear");
            }
            #endregion
        }

        /// <summary>
        /// 输出当前的打印信息
        /// </summary>
        /// <param name="text"></param>
        private void DebugText(string text)
        {
            //this.text.text += text;
            //this.text.text += "\n";
        }
    }
}