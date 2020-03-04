/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CubeTweenLogic
* 创建日期：2019-01-14 17:13:31
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

namespace Buskit3D.Example_020_Tween
{
    /// <summary>
    /// 立方体行为逻辑
    /// </summary>
    public class CubeTweenLogic : TweenLogic
    {
        public override void OnComplete(TweenArgs args)
        {
            
            switch (args.tweenName)
            {  
                   case "1":
                    //当立方体做完第二段动画时，触发球体的第一段动画
                    if(observer!=null)
                    observer.GetComponent<SphereTweenDataModel>().ToNextTween();
                    break;
                default:
                    break;
            }
        }
    }
}

