/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CommincationLogic
* 创建日期：2019-04-11 13:51:50
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using UnityEngine.UI;
using System.Collections.Generic;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class CommincationLogic : LogicBehaviour
    {
        /// <summary>
        /// 通讯手机1
        /// </summary>
        public Dropdown phone1;
        /// <summary>
        /// 通讯手机2
        /// </summary>
        public Dropdown phone2;
        /// <summary>
        /// 给下拉框赋值
        /// </summary>
        private List<string> showNames;
        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                case "btnOpen":
                    if (evt.OldValue != evt.NewValue)
                        CreateCommincation();
                    break;
                case "btnOff":
                    if (evt.OldValue != evt.NewValue)
                        OffCommincation();
                    break;
                case "phone1":
                    if (evt.OldValue != evt.NewValue)
                    {
                        if (string.IsNullOrEmpty(phone1.captionText.text)) return;
                        int objctID = System.Convert.ToInt32(phone1.captionText.text);
                        ChangPhoneState(objctID,Color.yellow);

                        //上一个放置为白色
                        int oldValue = (int)evt.OldValue;
                        string lateObjID = phone1.options[oldValue].text;
                        //如果上一个对象ID为空
                        if (string.IsNullOrEmpty(lateObjID))
                        {
                            return;
                        }
                        else {
                            objctID = System.Convert.ToInt32(lateObjID);
                            ChangPhoneState(objctID, Color.white);
                        }
                    }
                    break;
                case "phone2":
                    if (evt.OldValue != evt.NewValue)
                    {
                        if (string.IsNullOrEmpty(phone2.captionText.text)) return;
                        int objctID = System.Convert.ToInt32(phone2.captionText.text);
                        ChangPhoneState(objctID, Color.yellow);

                        //上一个放置为白色
                        int oldValue = (int)evt.OldValue;
                        string lateObjID = phone2.options[oldValue].text;
                        //如果上一个对象ID为空
                        if (string.IsNullOrEmpty(lateObjID))
                        {
                            return;
                        }
                        else
                        {
                            objctID = System.Convert.ToInt32(lateObjID);
                            ChangPhoneState(objctID, Color.white);
                        }
                    }
                    break;
                case "back":
                    if (evt.OldValue != evt.NewValue)
                        Back();
                    break;
                case "isShow":
                    bool isShow = (bool)evt.NewValue;
                    if (isShow)
                    {
                        UIActiveTrue();
                    }
                    else
                    {
                        UIActiveFalse();
                    }
                    break;
                case "recover":
                    if (evt.OldValue != evt.NewValue)
                    {
                        ObjectPool<PhoneModel> pool = InjectService.Get<ObjectPool<PhoneModel>>();
                        foreach (var value in pool.objectsDic.Values)
                        {
                            value.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        }
                    }
                    break;

            }
        }

        /// <summary>
        /// 创建通讯
        /// </summary>
        public void CreateCommincation()
        {
            //Debug.Log("创建通讯");
            //手机1和手机2是否准备就绪
            string str1 = phone1.captionText.text;
            string str2 = phone2.captionText.text;
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                ConnectDefeated("请确认通讯设备是否准备就绪");
                return;
            }
            else {
                if (str1.Equals(str2))
                {
                    ConnectDefeated("不能与自己通讯");
                    return;
                }
                else {
                    //判断通讯状态
                    int phoneID_1 = System.Convert.ToInt32(phone1.captionText.text);
                    int phoneID_2 = System.Convert.ToInt32(phone2.captionText.text);
                    ObjectPool<PhoneModel> poolPhone = InjectService.Get<ObjectPool<PhoneModel>>();
                    ObjectPool<TowerModel> poolTower = InjectService.Get<ObjectPool<TowerModel>>();
                    var phone1Entity = (PhoneEntity)poolPhone.objectsDic[phoneID_1].DataEntity;
                    var phone2Entity = (PhoneEntity)poolPhone.objectsDic[phoneID_2].DataEntity;

                    var tower1ID = phone1Entity.towerModelID;
                    var tower2ID = phone2Entity.towerModelID;

                    //1.两个手机是否都在服务器
                    if (tower1ID == -1 || tower2ID == -1)
                    {
                        ConnectDefeated("手机不在服务区");
                        return;
                    }
                    //2.两个手机所在的信号塔是否可以互相通信
                    else {
                        var tower1Entity = (TowerEntity)poolTower.objectsDic[tower1ID].DataEntity;
                        var tower2Entity = (TowerEntity)poolTower.objectsDic[tower2ID].DataEntity;
                        //phone1 呼叫 phone2
                        //Debug.Log(tower1ID+"__"+tower2ID);
                        //Debug.Log(tower1Entity.towerList.Count);
                        if (tower1ID == tower2ID || tower1Entity.towerList.Contains(tower2ID))
                        {
                            ConnectSuccess();
                        }
                        else {
                            ConnectDefeated("链路不通");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 断开通讯
        /// </summary>
        public void OffCommincation()
        {
            Debug.Log("关闭通讯");
            //当前所有信号塔的通讯手机都去掉当前通讯的手机
            //两个手机变成绿色
            if (string.IsNullOrEmpty(phone1.captionText.text) || string.IsNullOrEmpty(phone2.captionText.text)) return;
            int phoneID_1 = System.Convert.ToInt32(phone1.captionText.text);
            int phoneID_2 = System.Convert.ToInt32(phone2.captionText.text);
            ObjectPool<PhoneModel> poolPhone = InjectService.Get<ObjectPool<PhoneModel>>();

            var phoneEntity1 = (PhoneEntity)poolPhone.objectsDic[phoneID_1].DataEntity;
            var phoneEntity2 = (PhoneEntity)poolPhone.objectsDic[phoneID_2].DataEntity;

            //手机在通讯状态
            phoneEntity1.currentState = 0;
            phoneEntity2.currentState = 0;

            //互相绑定为通话状态的手机
            phoneEntity1.withPhoen = -1;
            phoneEntity2.withPhoen = -1;

            //poolPhone.objectsDic[phoneID_1].gameObject.GetComponent<Renderer>().material.color = Color.green;
            //poolPhone.objectsDic[phoneID_2].gameObject.GetComponent<Renderer>().material.color = Color.green;

            //手机1所在的信号塔广播信号
            ObjectPool<TowerModel> poolTower = InjectService.Get<ObjectPool<TowerModel>>();
            var phone1Entity = (PhoneEntity)poolPhone.objectsDic[phoneID_1].DataEntity;
            var tower1ID = phone1Entity.towerModelID;
            if (!poolTower.objectsDic.ContainsKey(tower1ID)) return;
            var tower1Entity = (TowerEntity)poolTower.objectsDic[tower1ID].DataEntity;
            tower1Entity.phoneUseList.Remove(phoneID_1);


            for (int i = 0; i < tower1Entity.towerList.Count; i++)
            {
                var _tower1ID = tower1Entity.towerList[i];
                var _towerModel = poolTower.objectsDic[_tower1ID];
                var _tower1Entity = (TowerEntity)_towerModel.DataEntity;
                _tower1Entity.phoneUseList.Remove(phoneID_1);
            }
        }

        /// <summary>
        /// 通讯建立成功
        /// </summary>
        public void ConnectSuccess() {
            //Debug.Log("通讯建立成功");
            //两个手机变成绿色
            int phoneID_1 = System.Convert.ToInt32(phone1.captionText.text);
            int phoneID_2 = System.Convert.ToInt32(phone2.captionText.text);
            ObjectPool<PhoneModel> poolPhone = InjectService.Get<ObjectPool<PhoneModel>>();

            var phoneEntity1 = (PhoneEntity)poolPhone.objectsDic[phoneID_1].DataEntity;
            var phoneEntity2 = (PhoneEntity)poolPhone.objectsDic[phoneID_2].DataEntity;

            //手机在通讯状态
            phoneEntity1.currentState = 1;
            phoneEntity2.currentState = 1;
            
            //互相绑定为通话状态的手机
            phoneEntity1.withPhoen = phoneID_2;
            phoneEntity2.withPhoen = phoneID_1;
                       
            //poolPhone.objectsDic[phoneID_1].gameObject.GetComponent<Renderer>().material.color = Color.green;
            //poolPhone.objectsDic[phoneID_2].gameObject.GetComponent<Renderer>().material.color = Color.green;
            
            //手机1所在的信号塔广播信号
            ObjectPool<TowerModel> poolTower = InjectService.Get<ObjectPool<TowerModel>>();
            var phone1Entity = (PhoneEntity)poolPhone.objectsDic[phoneID_1].DataEntity;
            var tower1ID = phone1Entity.towerModelID;
            var tower1Entity = (TowerEntity)poolTower.objectsDic[tower1ID].DataEntity;
            tower1Entity.phoneUseList.Add(phoneID_1);


            for (int i = 0; i < tower1Entity.towerList.Count; i++) {
                var _tower1ID = tower1Entity.towerList[i];
                var _towerModel = poolTower.objectsDic[_tower1ID];
                var _tower1Entity = (TowerEntity)_towerModel.DataEntity;
                _tower1Entity.phoneUseList.Add(phoneID_1);
            }

        }
        /// <summary>
        /// 通讯建立失败
        /// </summary>
        public void ConnectDefeated(string info)
        {
            Debug.Log(info);
        }

        /// <summary>
        /// 返回
        /// </summary>
        public void Back() {
            var comminicationEntity = (ComminicationEntity)FindObjectOfType<ComminicationViewModel>().DataEntity;
            comminicationEntity.isShow = false;

            var menEntity = (MenuEntity)FindObjectOfType<MenuViewModel>().DataEntity;
            menEntity.isShow = true;
        }
        /// <summary>
        /// UI关闭
        /// </summary>
        public void UIActiveFalse() {
            transform.localScale = Vector3.zero;
            phone1.options.Clear();
            phone2.options.Clear();
        }
        /// <summary>
        /// 激活UI时初始化
        /// </summary>
        public void UIActiveTrue() {
            transform.localScale = Vector3.one;
            //获取所有的手机列表放置到下拉框
            var commincationModel = GetComponent<ComminicationViewModel>();
            var comminationEntity = (ComminicationEntity)commincationModel.DataEntity;
            UpdateDropdownView(commincationModel, comminationEntity, phone1);
            UpdateDropdownView(commincationModel, comminationEntity, phone2);
        }

        /// <summary>
        /// 改变手机显示状态
        /// </summary>
        public void ChangPhoneState(int phoneObjectID, Color color) {
            ObjectPool<PhoneModel> pool = InjectService.Get<ObjectPool<PhoneModel>>();
            if(pool.objectsDic.ContainsKey(phoneObjectID))
                pool.objectsDic[phoneObjectID].gameObject.GetComponent<Renderer>().material.color = color;
        }

        /// <summary>
        /// 刷数据
        /// </summary>
        /// <param name="showNames"></param>
        private void UpdateDropdownView(ComminicationViewModel commincationModel, ComminicationEntity comminationEntity,Dropdown dropdownItem)
        {
            showNames = new List<string>();
            dropdownItem.options.Clear();
            Dropdown.OptionData tempData;
            ObjectPool<PhoneModel> pool = InjectService.Get<ObjectPool<PhoneModel>>();
            pool.Foreach(ForeachPool);

            #region 添加一个项
            tempData = new Dropdown.OptionData();
            tempData.text = "";
            dropdownItem.options.Add(tempData);
            #endregion

            for (int i = 0; i < showNames.Count; i++)
            {
                tempData = new Dropdown.OptionData();
                tempData.text = showNames[i];
                dropdownItem.options.Add(tempData);
            }
        }
        private void ForeachPool(int index, PhoneModel towerModel)
        {
            showNames.Add(towerModel.DataEntity.objectID.ToString());
        }
    }
}

