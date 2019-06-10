using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    /// <summary>
    /// 注册表使用示例
    /// </summary>
    public class RegistryKeyUse
    {
        private readonly RegistryKey R_Local;
        private readonly RegistryKey R_Run;

        /// <summary>
        /// 根据给定的值，新建一个子项
        /// </summary>
        /// <param name="subKey">可为空，为空时有默认值</param>
        public RegistryKeyUse(string subKey = null)
        {
            R_Local = Registry.LocalMachine;
            if (subKey!=null)
            {
                R_Run = R_Local.CreateSubKey(subKey);
            }
            else
            {
                R_Run = R_Local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            }
        }

        /// <summary>
        /// 根据标识名称获取注册的值
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string GetRegistryValueName(string keyName)
        {
            object value = R_Run.GetValue(keyName);

            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置注册表的值
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="valueStr"></param>
        public void SetRegistryValue(string keyName, string valueStr)
        {
            R_Run.SetValue(keyName, valueStr);
        }

        /// <summary>
        /// 删除指定的注册值，不引发异常
        /// </summary>
        /// <param name="keyName"></param>
        public void DeleteRegistryValue(string keyName)
        {
            object value = R_Run.GetValue(keyName);
            if (value != null)
            {
                R_Run.DeleteSubKey(keyName, false);
            }
        }
    }
}
