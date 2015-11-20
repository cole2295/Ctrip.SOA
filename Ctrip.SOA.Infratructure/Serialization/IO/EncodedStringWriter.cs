using System;
using System.IO;
using System.Text;

namespace Ctrip.SOA.Infratructure.IO
{
	/// <summary>
	/// 实现一个用于将信息写入字符串的 <see cref="System.IO.TextWriter"/>。
	/// 该信息存储在基础 <see cref="System.Text.StringBuilder"/> 中。
	/// </summary>
	public class EncodedStringWriter : StringWriter
	{
		private Encoding m_Encoding;

		/// <summary>
		/// 初始化 <b>StringWriter</b> 类的新实例。 
		/// </summary>
		public EncodedStringWriter()
			: this(new StringBuilder(), Encoding.UTF8)
		{
		}

		/// <summary>
		/// 使用指定的格式控制初始化 <b>StringWriter</b> 类的新实例。 
		/// </summary>
		/// <param name="formatProvider">控制格式设置的 <see cref="System.IFormatProvider"/> 对象。</param>
		public EncodedStringWriter(IFormatProvider formatProvider)
			: this(new StringBuilder(), formatProvider, Encoding.UTF8)
		{
		}

		/// <summary>
		/// 初始化写入指定 <b>StringBuilder</b> 的 <b>StringWriter</b> 类的新实例。  
		/// </summary>
		/// <param name="sb">要写入的 <b>StringBuilder</b>。</param>
		public EncodedStringWriter(StringBuilder sb)
			: this(sb, Encoding.UTF8)
		{
		}

		/// <summary>
		/// 初始化写入指定 <b>StringBuilder</b> 并具有指定格式提供程序的 <b>StringWriter</b> 类的新实例。 
		/// </summary>
		/// <param name="sb">要写入的 <b>StringBuilder</b>。</param>
		/// <param name="formatProvider">控制格式设置的 <see cref="System.IFormatProvider"/> 对象。</param>
		public EncodedStringWriter(StringBuilder sb, IFormatProvider formatProvider)
			: this(sb, formatProvider, Encoding.UTF8)
		{
		}

		/// <summary>
		/// 初始化具有指定编码的 <b>StringWriter</b> 新实例。 
		/// </summary>
		/// <param name="encoding">要使用的字符编码。</param>
		public EncodedStringWriter(Encoding encoding)
			: this(new StringBuilder(), encoding)
		{
		}

		/// <summary>
		/// 初始化写入指定 <b>StringBuilder</b> 并具有指定编码的 <b>StringWriter</b> 新实例。 
		/// </summary>
		/// <param name="sb">要写入的 <b>StringBuilder</b>。</param>
		/// <param name="encoding">要使用的字符编码。</param>
		public EncodedStringWriter(StringBuilder sb, Encoding encoding)
			: base(sb)
		{
			m_Encoding = encoding;
		}

		/// <summary>
		/// 初始化写入指定 <b>StringBuilder</b> 并具有指定格式提供程序和指定编码的 <b>StringWriter</b> 新实例。 
		/// </summary>
		/// <param name="sb">要写入的 <b>StringBuilder</b>。</param>
		/// <param name="formatProvider">控制格式设置的 <see cref="System.IFormatProvider"/> 对象。</param>
		/// <param name="encoding">要使用的字符编码。</param>
		public EncodedStringWriter(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding)
			: base(sb, formatProvider)
		{
			m_Encoding = encoding;
		}

		/// <summary>
		/// 获取将输出写入到其中的 <see cref="System.Text.Encoding"/>。 
		/// </summary>
		/// <value>在当前实例的构造函数中指定的 <b>Encoding</b>；或者如果未指定编码，则为 <see cref="System.Text.UTF8Encoding"/>。</value>
		public override Encoding Encoding
		{
			get
			{
				if (m_Encoding != null)
				{
					return m_Encoding;
				}

				return Encoding.UTF8;
			}
		}
	}
}