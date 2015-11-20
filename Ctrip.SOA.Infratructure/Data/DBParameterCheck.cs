using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Data {
    /// <summary>
    /// 数据库参数验证枚举(后续维护)
    /// </summary>
    public enum ParameterEnum {
        /// <summary>
        /// 不能为空
        /// </summary>
        NoEmpty = 2,
        /// <summary>
        /// 必须是数字
        /// </summary>
        Number = 4,
        /// <summary>
        /// 必须是日期
        /// </summary>
        Date = 8,
        /// <summary>
        /// 如果是数字必须大于0
        /// </summary>
        NotDefaultInt = 16
    }

    /// <summary>
    /// 数据库参数验证类
    /// </summary>
    public class DBParameterCheck {
        /// <summary>
        /// 参数验证
        /// </summary>
        /// <param name="parameter">当前参数</param>
        /// <param name="type">验证方式</param>
        /// <returns>True:验证失败</returns>
        public static bool CheckParameter(string parameter, ParameterEnum type) {
            bool returnValue = false;
            //不能为空
            if ((type & ParameterEnum.NoEmpty) == ParameterEnum.NoEmpty) {
                if (string.IsNullOrEmpty(parameter.Replace(" ", ""))) {
                    returnValue = true;
                }
            }
            //必须是数字
            if ((type & ParameterEnum.Number) == ParameterEnum.Number) {
                if (!StringHelper.IsNumeric(parameter)) {
                    returnValue = true;
                }
                else {
                    //如果是数字必须大于0
                    if ((type & ParameterEnum.NotDefaultInt) == ParameterEnum.NotDefaultInt) {
                        if (Convert.ToInt32(parameter) <= 0) {
                            returnValue = true;
                        }
                    }
                }
            }

            //必须是日期
            if ((type & ParameterEnum.Date) == ParameterEnum.Date) {

            }
            return returnValue;
        }
    }
}
