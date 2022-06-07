using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    public abstract class moSymbol
    {
        /// <summary>
        /// 抽象属性
        /// </summary>
        public abstract moSymbolTypeConstant SymbolType { get; }

        /// <summary>
        /// 抽象方法：复制
        /// </summary>
        /// <returns></returns>
        public abstract moSymbol Clone();           // 抽象类中定义的抽象属性和抽象方法，在派生类中必须予以实现
    }
}
