/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：TestEntity
* 创建日期：2019-03-14 11:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_0xx_Igcs
{
    public class TestEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 1 测试选项断言
        /// </summary>
        [IgcsAssertOption(
            Meaning = "信号窗函数类型",
            Score = 0.4f,
            Options = new object[] { "汉明窗", "矩形窗", "三角窗", "高斯窗" },
            AssertValue = "汉明窗"
        )]
        public int SignalWindowType = -1;

        /// <summary>
        /// 2 测试等于断言
        /// </summary>
        [IgcsAssertEquals(
            Meaning ="信号源频率",
            Score = 0.4f,
            AssertValue =0.8f
        )]
        public float frequency = 0.0f;

        /// <summary>
        /// 3 测试为假断言
        /// </summary>
        [IgcsAssertFalse(Meaning ="开关是否打开",Score =5.0f)]
        public bool opened = false;

        /// <summary>
        /// 4 测试大于断言
        /// </summary>
        [IgcsAssertGreater(AssertValue = 1.0f, Meaning = "最低频率1", Score = 5.0f)]
        public float y = 0.2f;

        /// <summary>
        /// 5 测试大于等于断言
        /// </summary>
        [IgcsAssertGreaterOrEquals(AssertValue = 3.0f, Meaning = "最低频率2", Score = 5.0f)]
        public float z = 0.2f;

        /// <summary>
        /// 6 测试区间断言
        /// </summary>
        [IgcsAssertInterval(
            Meaning ="X坐标范围",
            Max = 122.3f,
            Min =10.0f,
            Score = 3.0f)]
        public float x = 0.4f;

        /// <summary>
        /// 7 测试小于断言
        /// </summary>
        [IgcsAssertLess(AssertValue = 1.0f, Meaning = "最高频率1", Score = 5.0f)]
        public float w = 0.2f;

        /// <summary>
        /// 8 测试小于等于断言
        /// </summary>
        [IgcsAssertLessOrEquals(AssertValue = 3.0f, Meaning = "最高频率2", Score = 5.0f)]
        public float o = 0.2f;

        /// <summary>
        /// 9 测试不等于断言
        /// </summary>
        [IgcsAssertNotEquals(AssertValue =30,Meaning ="元素数量",Score =3.0f)]
        public int number = 20;

        /// <summary>
        /// 10 测试为真断言
        /// </summary>
        [IgcsAssertTrue(Meaning = "是否启动", Score = 5.0f)]
        public bool started = false;

    }
}
