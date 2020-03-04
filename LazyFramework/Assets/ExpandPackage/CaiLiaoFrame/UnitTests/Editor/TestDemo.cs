/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：HowTo_Test
* 类 名 称：TestDemo
* 创建日期：2018-04-17 10:36:06
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 功能描述：简单的测试demo编写
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;


namespace HowTo_Test
{

    /// <summary></summary>
    [TestFixture]
    public class TestDemo
    {

        public int Number { get; set; }

        public int Sum(int num)
        {
            Number = 0;

            while (num > Number)
                Number += 1;
            return Number;
        }

        class StringMethod
        {
            
            public string ReverseString(string s)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int endIndex = s.Length - 1;
                for (int i = endIndex; i >= 0; i--)
                {
                    sb.Append(s[i]);
                }

                return sb.ToString();
            }

        }

        /// <summary>
        /// 在带有Test标签的方法中执行测试
        /// </summary>
        [Test]
        public void TestSum()
        {

            int value = Sum(3);
            //比对Sum方法的计算结果，与预期值是否相等
            Assert.AreEqual(value, 3);


        }
        [Test]
        public void TestString()
        {
            StringMethod stringMethod = new StringMethod();

            string value = stringMethod.ReverseString("Hello");
            Assert.AreEqual(value, "olleH");

        }

    }
}

