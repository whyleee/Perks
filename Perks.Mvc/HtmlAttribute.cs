using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Perks.Mvc
{
    public class HtmlAttribute : IHtmlString
    {
        private readonly string _name;
        private string _internalValue = "";

        public HtmlAttribute(string name)
        {
            _name = name;
        }

        public HtmlAttribute Add(string value, bool condition = true)
        {
            if (value.IsNotNullOrEmpty() && condition)
            {
                _internalValue += value + " ";
            }

            return this;
        }

        public string ToHtmlString()
        {
            if (_internalValue.IsNotNullOrEmpty())
            {
                _internalValue = string.Format("{0}=\"{1}\"",
                    _name, _internalValue.Substring(0, _internalValue.Length - 1));
            }

            return _internalValue;
        }
    }
}
