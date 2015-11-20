using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Enums {
    /// <summary>
    /// prd_productdesc – DescType
    ///1 – 产品描述信息
    ///2 – 预订描述信息
    ///3 – 费用包含描述信息
    ///4 – 签证信息
    ///5 – 费用不包含描述信息
    ///
    /// 为了避免错误，新添加的描述类型直接从10开始
    /// 
    /// </summary>
    public enum ProductDescType {
        ProductDescription = 1,
        ReservationDescription = 2,
        FeeIncludeDescription = 3,
        VisaDescription = 4,
        FeeExcludeDescription = 5,

        ProductHighLightDescription = 10
    }
}
