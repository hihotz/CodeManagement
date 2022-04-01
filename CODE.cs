using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeManagement
{
    class CODE
    {
        public int Uid { get; set; }
        public string KeyWord { get; set; }
        public string Url { get; set; }

        public CODE(int uid, string keyword, string url)
        {
            Uid = uid;
            KeyWord = keyword;
            Url = url;
        }

        public override string ToString()
        {
            return string.Format("[{0}], {1}, {2}", Uid, KeyWord, Url);
        }

    }
}
